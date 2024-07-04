//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================
namespace Syrx.Commanders.Databases
{
    public sealed partial class DatabaseCommander<TRepository>
    {
        public bool Execute<TResult>([CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            using (var connection = _connector.CreateConnection(setting))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction(setting.IsolationLevel))
                {
                    try
                    {
                        var command = GetCommandDefinition(setting, transaction: transaction);
                        var result = (connection.Execute(command) > 0);
                        transaction.Commit();
                        return result;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool Execute<TResult>(TResult model, [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            using (var connection = _connector.CreateConnection(setting))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction(setting.IsolationLevel))
                {
                    try
                    {
                        var command = GetCommandDefinition(setting, model, transaction);
                        var result = (connection.Execute(command) > 0);
                        transaction.Commit();
                        return result;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public TResult Execute<TResult>(Func<TResult> map,
            TransactionScopeOption scopeOption = TransactionScopeOption.Suppress,
            [CallerMemberName] string method = null)
        {
            // thought: should we support passing in the transaction scope option?
            //          i.e. let it be overridden by query definition?
            using (var scope = new TransactionScope(scopeOption))
            {
                var result = map();
                scope.Complete();
                return result;
            }
        }
    }
}