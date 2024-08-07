﻿//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:39)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx
{
    /// <inheritdoc />
    /// <summary>
    ///     Basically breaks down into two distinct operations for mutating data/retrieving data
    /// </summary>
    public partial interface ICommander<TRepository>
    {
        /// <summary>
        ///     Executes an arbitrary command against the underlying data store.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>        
        /// <param name="method">The method.</param>
        /// <returns></returns>
        bool Execute<TResult>([CallerMemberName] string method = null);

        /// <summary>
        ///     Executes a potentially state changing operation against the underlying data store (create/update/delete)
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="model">The model.</param>        
        /// <param name="method">The method.</param>
        /// <returns></returns>
        bool Execute<TResult>(TResult model, [CallerMemberName] string method = null);

        /// <summary>
        ///     Executes any number of potentially state changing operations against an underlying data store.
        ///     This method will escalate to a distributed transaction if the operations spans more than one
        ///     database.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The map.</param>
        /// <param name="scopeOption">The <see cref="TransactionScopeOption"/> applied to the function.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        TResult Execute<TResult>(
            Func<TResult> map,
            TransactionScopeOption scopeOption = TransactionScopeOption.Suppress,
            [CallerMemberName] string method = null);

    }
}