// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Security.Cryptography;
using Internal.NativeCrypto;

namespace Internal.Cryptography
{
    internal sealed partial class DesImplementation
    {
        private static UniversalCryptoTransform CreateTransformCore(
            CipherMode cipherMode,
            PaddingMode paddingMode,
            byte[] key,
            byte[]? iv,
            int blockSize,
            int feedbackSize,
            int paddingSize,
            bool encrypting)
        {
            SafeAlgorithmHandle algorithm = DesBCryptModes.GetSharedHandle(cipherMode, feedbackSize);

            BasicSymmetricCipher cipher = new BasicSymmetricCipherBCrypt(algorithm, cipherMode, blockSize, paddingSize, key, false, iv, encrypting);
            return UniversalCryptoTransform.Create(paddingMode, cipher, encrypting);
        }

        private static ILiteSymmetricCipher CreateLiteCipher(
            CipherMode cipherMode,
            PaddingMode paddingMode,
            ReadOnlySpan<byte> key,
            ReadOnlySpan<byte> iv,
            int blockSize,
            int feedbackSize,
            int paddingSize,
            bool encrypting)
        {
            SafeAlgorithmHandle algorithm = DesBCryptModes.GetSharedHandle(cipherMode, feedbackSize);

            return new BasicSymmetricCipherLiteBCrypt(
                algorithm,
                cipherMode,
                blockSize,
                paddingSize,
                key,
                ownsParentHandle: false,
                iv,
                encrypting);
        }
    }
}
