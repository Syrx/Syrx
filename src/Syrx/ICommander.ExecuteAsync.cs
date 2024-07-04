//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:39)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx
{
    /// <summary>
    /// Provides asychronous write operations. 
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public partial interface ICommander<TRepository> : IDisposable
    {
        /// <summary>
        ///     Executes an arbitrary command against the underlying datastore asynchronously.
        /// </summary>        
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        Task<bool> ExecuteAsync<TResult>(CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Executes a potentially state changing operation asynchronously.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="model">The model.</param>        
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        Task<bool> ExecuteAsync<TResult>(TResult model, CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Wraps a Func with a TransactionScope object. Doesn't handle async very well at all. Use at your own peril.
        ///     The "passing" test for this method is entirely synchronous in the map Func.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The map.</param>
        /// <param name="scopeOption"></param>
        /// <param name="asyncFlowOption"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        Task<TResult> ExecuteAsync<TResult>(Func<TResult> map,
            TransactionScopeOption scopeOption = TransactionScopeOption.Suppress,
            TransactionScopeAsyncFlowOption asyncFlowOption = TransactionScopeAsyncFlowOption.Enabled,
            CancellationToken cancellationToken = default, [CallerMemberName] string method = null);
    }
}