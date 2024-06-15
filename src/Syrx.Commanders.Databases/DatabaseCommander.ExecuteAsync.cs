//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================
namespace Syrx.Commanders.Databases
{
    public sealed partial class DatabaseCommander<TRepository>
    {
        public async Task<bool> ExecuteAsync<TResult>(CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            using (var connection = _connector.CreateConnection(setting))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction(setting.IsolationLevel))
                {
                    try
                    {
                        var command = GetCommandDefinition(setting, transaction: transaction,
                            cancellationToken: cancellationToken);
                        var result = (await connection.ExecuteAsync(command) > 0);
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

        public async Task<bool> ExecuteAsync<TResult>(TResult model,
            CancellationToken cancellationToken = default, [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            using (var connection = _connector.CreateConnection(setting))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction(setting.IsolationLevel))
                {
                    try
                    {
                        var command = GetCommandDefinition(setting, model, transaction, cancellationToken);
                        var result = (await connection.ExecuteAsync(command) > 0);
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

        public async Task<TResult> ExecuteAsync<TResult>(Func<TResult> map,
            TransactionScopeOption scopeOption = TransactionScopeOption.Suppress,
            TransactionScopeAsyncFlowOption asyncFlowOption = TransactionScopeAsyncFlowOption.Enabled,
            CancellationToken cancellationToken = default, [CallerMemberName] string method = null)
        {
            // i'm honestly not lovin this method. looks like it could cause
            // too many holes and could lead to some seriously, serious
            // nasty, nasty.             
            using (var scope = new TransactionScope(scopeOption, TransactionScopeAsyncFlowOption.Enabled))
            {
                // could also be abused as sync over async.
                // todo: write tests to prove abuse and write about 
                // why it's bad, bad, bad. 
                var result = await Task.FromResult(map());
                scope.Complete();
                return result;
            }
        }
    }
}