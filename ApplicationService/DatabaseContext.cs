using ApplicationService.PersistentStorage.NHibernate;
using NHibernate;
using System;
using System.Data;

namespace ApplicationService
{
    public class DatabaseContext : IDisposable
    {
        private ISession _session;
        private ITransaction _transaction;
        bool _disposed;

        public DatabaseContext(IsolationLevel isolationLevel)
        {
            _session = SessionManager.OpenSession();
            _transaction = isolationLevel != IsolationLevel.Unspecified ?
                _session.BeginTransaction(isolationLevel) :
                _session.BeginTransaction();
        }

        //~DatabaseContext()
        //{
        //    Dispose(false);
        //}

        public ISession Session
        {
            get { return _session; }
        }

        public void Commit()
        {
            if (_session.Transaction.IsActive)
                _transaction.Commit();
            else
                throw new InvalidOperationException("No active transaction.");
        }

        public void Dispose()
        {
            if (_session.Transaction.IsActive && !_transaction.WasCommitted)
                _transaction.Rollback();

            //_session.Dispose();
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }
        
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_session.Transaction.IsActive && !_transaction.WasCommitted)
                    _transaction.Rollback();

                _session.Dispose();
            }

            //_transaction = null;
            //_session = null;

            _disposed = true;
        }
    }
}