// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Xunit;

namespace System.Reflection.Emit.Tests
{
    public class LocalBuilderIsPinned
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPinned(bool pinned)
        {
            TypeBuilder type = Helpers.DynamicType(TypeAttributes.NotPublic);
            MethodBuilder method = type.DefineMethod("Method", MethodAttributes.Public);
            ILGenerator ilGenerator = method.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ret);
            LocalBuilder localBuilder = ilGenerator.DeclareLocal(typeof(string), pinned);
            Assert.Equal(pinned, localBuilder.IsPinned);
        }
    }
}
