// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;
using Xunit;

public partial class SafeHandleTests
{
    private class MySafeHandle : SafeHandle
    {
        public MySafeHandle()
            : base(IntPtr.Zero, true)
        {
        }

        public MySafeHandle(IntPtr handle)
            : this()
        {
            SetHandle(handle);
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }

        public bool IsReleased { get; private set; }

        protected override bool ReleaseHandle()
        {
            return this.IsReleased = true;
        }
    }

    [Fact]
    public static void SafeHandle_invalid()
    {
        MySafeHandle mch = new MySafeHandle();
        Assert.False(mch.IsClosed);
        Assert.True(mch.IsInvalid);
        Assert.False(mch.IsReleased);
        Assert.Equal(IntPtr.Zero, mch.DangerousGetHandle());

        mch.Dispose();
        Assert.True(mch.IsClosed);
        Assert.True(mch.IsInvalid);
        Assert.False(mch.IsReleased);

        // Make sure we can dispose multiple times
        mch.Dispose();
        Assert.True(mch.IsClosed);
        Assert.True(mch.IsInvalid);
        Assert.False(mch.IsReleased);
    }

    [Fact]
    public static void SafeHandle_valid()
    {
        IntPtr ptr = new IntPtr(1);
        MySafeHandle mch = new MySafeHandle(ptr);
        Assert.False(mch.IsClosed);
        Assert.False(mch.IsInvalid);
        Assert.False(mch.IsReleased);
        Assert.Equal(ptr, mch.DangerousGetHandle());

        mch.Dispose();
        Assert.True(mch.IsClosed);
        Assert.False(mch.IsInvalid);
        Assert.True(mch.IsReleased);

        // Make sure we can dispose multiple times
        mch.Dispose();
        Assert.True(mch.IsClosed);
        Assert.False(mch.IsInvalid);
        Assert.True(mch.IsReleased);
    }

    [Fact]
    public static void SafeHandle_invalid_close()
    {
        MySafeHandle mch = new MySafeHandle();
        mch.Close();
        Assert.True(mch.IsClosed);
        Assert.True(mch.IsInvalid);
        Assert.False(mch.IsReleased);
    }

    [Fact]
    public static void SafeHandle_valid_close()
    {
        MySafeHandle mch = new MySafeHandle(new IntPtr(1));
        mch.Close();
        Assert.True(mch.IsClosed);
        Assert.False(mch.IsInvalid);
        Assert.True(mch.IsReleased);
    }

    [Fact]
    public static void SafeHandle_SetHandleAsInvalid()
    {
        IntPtr ptr = new IntPtr(1);
        MySafeHandle handle = new MySafeHandle(ptr);
        handle.SetHandleAsInvalid();
        Assert.True(handle.IsClosed);
        Assert.False(handle.IsInvalid);
        Assert.False(handle.IsReleased);
        Assert.Equal(ptr, handle.DangerousGetHandle());
    }

    [DllImport("Kernel32", SetLastError = true)]
    private static extern void SetLastError(int error);

    private class LastErrorSafeHandle : SafeHandle
    {
        internal LastErrorSafeHandle(IntPtr h)
            : base(h, true)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            SetLastError(-1);
            return true;
        }
    }

    [Fact]
    [PlatformSpecific(TestPlatforms.Windows)]
    public static void SafeHandle_DangerousReleasePreservesLastError()
    {
        LastErrorSafeHandle handle = new LastErrorSafeHandle((IntPtr)1);

        bool success = false;
        handle.DangerousAddRef(ref success);
        handle.Dispose();

        SetLastError(42);
        handle.DangerousRelease();

        int error = Marshal.GetLastPInvokeError();
        Assert.Equal(42, error);
    }
}
