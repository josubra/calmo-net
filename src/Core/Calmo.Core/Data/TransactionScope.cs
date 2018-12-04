using System;
using System.Data;
using Calmo.Core.Threading;

namespace Calmo.Core.Data
{
	/// <summary>
	/// Class used to control the cross-database transaction scope
	/// </summary>
    public class TransactionScope : IDisposable
    {
        public const string ActiveScopeKey = "activeScope";
        public const string ScopeTransactionKey = "scopeTransaction";
        private bool _complete;

        public TransactionScope()
        {
            ThreadStorage.SetData(ActiveScopeKey, true);
        }

		/// <summary>
		/// Mark the current transaction as complete successfully
		/// </summary>
        public void Complete()
        {
            _complete = true;
        }

		/// <summary>
		/// Dispose the transaction object and commit (if the transaction was marked as completed) or rollback otherwise.
		/// </summary>
        public void Dispose()
        {
            var transaction = ThreadStorage.GetData<IDbTransaction>(ScopeTransactionKey);
            if (transaction == null || !ThreadStorage.GetData<bool>(ActiveScopeKey)) return;

            var connection = transaction.Connection;

            if (_complete)
                transaction.Commit();
            else
                transaction.Rollback();

            if (connection != null && connection.State != ConnectionState.Closed)
                connection.Dispose();

            ThreadStorage.ClearData(ActiveScopeKey);
            ThreadStorage.ClearData(ScopeTransactionKey);
        }
    }
}