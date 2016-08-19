using ApplicationService.PersistentStorage.NHibernate;
using NHibernate;
using System;
using System.Data;

namespace ApplicationService
{
    public class DatabaseContext : IDisposable
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public DatabaseContext(IsolationLevel isolationLevel)
        {
            _session = SessionManager.OpenSession();
            _transaction = isolationLevel != IsolationLevel.Unspecified ?
                _session.BeginTransaction(isolationLevel) :
                _session.BeginTransaction();
        }

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

            _session.Dispose();
        }
    }
}