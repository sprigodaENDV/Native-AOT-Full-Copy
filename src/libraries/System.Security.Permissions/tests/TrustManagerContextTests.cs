// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Policy;
using Xunit;

namespace System.Security.Permissions.Tests
{
    public class TrustManagerContextTests
    {
        [Fact]
        public static void TrustManagerContextCallMethods()
        {
            TrustManagerContext tmc = new TrustManagerContext();
            tmc = new TrustManagerContext(new TrustManagerUIContext());
        }
    }
}
