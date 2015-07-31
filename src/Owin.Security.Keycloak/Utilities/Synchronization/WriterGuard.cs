﻿using System;
using System.Threading;

namespace Owin.Security.Keycloak.Utilities.Synchronization
{
    internal class WriterGuard : IDisposable
    {
        private ReaderWriterLockSlim _readerWriterLock;

        public WriterGuard(ReaderWriterLockSlim readerWriterLock)
        {
            _readerWriterLock = readerWriterLock;
            _readerWriterLock.EnterWriteLock();
        }

        private bool IsDisposed => _readerWriterLock == null;

        public void Dispose()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString());
            _readerWriterLock.ExitWriteLock();
            _readerWriterLock = null;
        }
    }
}