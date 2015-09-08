// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using Framework.Misc;

namespace Framework.Cryptography.WoW
{
    public class RsaCrypt : IDisposable
    {
        BigInteger e, n, p, q, dp, dq, iq;
        bool isEncryptionInitialized;
        bool isDecryptionInitialized;

        public void InitializeEncryption(RsaData rsaData)
        {
            InitializeEncryption(rsaData.RsaParams.D, rsaData.RsaParams.P, rsaData.RsaParams.Q, rsaData.RsaParams.DP, rsaData.RsaParams.DQ, rsaData.RsaParams.InverseQ);
        }

        public void InitializeEncryption<T>(T d, T p, T q, T dp, T dq, T iq, bool isBigEndian = false)
        {
            this.p  = p.ToBigInteger(isBigEndian);
            this.q  = q.ToBigInteger(isBigEndian);
            this.dp = dp.ToBigInteger(isBigEndian);
            this.dq = dq.ToBigInteger(isBigEndian);
            this.iq = iq.ToBigInteger(isBigEndian);

            if (this.p.IsZero && this.q.IsZero)
                throw new InvalidOperationException("'0' isn't allowed for p or q");
            else
                isEncryptionInitialized = true;
        }

        public void InitializeDecryption(RsaData rsaData)
        {
            InitializeDecryption(rsaData.RsaParams.Exponent, rsaData.RsaParams.Modulus);
        }

        public void InitializeDecryption<T>(T e, T n, bool reverseBytes = false)
        {
            this.e = e.ToBigInteger(reverseBytes);
            this.n = n.ToBigInteger(reverseBytes);

            isDecryptionInitialized = true;
        }

        public byte[] Encrypt<T>(T data, bool isBigEndian = false)
        {
            if (!isEncryptionInitialized)
                throw new InvalidOperationException("Encryption not initialized");

            var bData = data.ToBigInteger(isBigEndian);

            var m1 = BigInteger.ModPow(bData % p, dp, p);
            var m2 = BigInteger.ModPow(bData % q, dq, q);

            var h = (iq * (m1 - m2)) % p;

            // Be sure to use the positive remainder
            if (h.Sign == -1)
                h = p + h;

            var m = m2 + h * q;

            return m.ToByteArray();
        }

        public byte[] Decrypt<T>(T data, bool isBigEndian = false)
        {
            if (!isDecryptionInitialized)
                throw new InvalidOperationException("Encryption not initialized");

            var c = data.ToBigInteger(isBigEndian);

            return BigInteger.ModPow(c, e, n).ToByteArray();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    e = 0;
                    n = 0;
                    p = 0;
                    q = 0;
                    dp = 0;
                    dq = 0;
                    iq = 0;

                    isEncryptionInitialized = false;
                    isDecryptionInitialized = false;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
