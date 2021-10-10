// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Xunit;

namespace System.Text.Tests
{
    public class UTF7EncodingGetEncoder
    {
        [Fact]
        public void GetEncoder()
        {
            Encoder encoder = new UTF7Encoding().GetEncoder();
            Assert.NotNull(encoder);
        }
    }
}
