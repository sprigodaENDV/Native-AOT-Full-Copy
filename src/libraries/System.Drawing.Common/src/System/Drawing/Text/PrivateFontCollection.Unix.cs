// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Drawing.Text
{
    public partial class PrivateFontCollection
    {
        private void GdiAddFontFile(string filename)
        {
             // There is no GDI on Unix, only libgdiplus, so this is a no-op.
        }
    }
}
