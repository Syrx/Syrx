//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:39)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx
{

    /// <summary>
    /// Provides read operations.
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public partial interface ICommander<TRepository>
    {
        /// <summary>
        ///     Query the data source.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<TResult>(
            object parameters = null,
            [CallerMemberName] string method = null);

        /* not supported (yet). 
        IEnumerable<TResult> Query<T1, TResult>(
            Func<T1, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);
        */

        /// <summary>
        ///     Use a multimap query with two generic inputs.
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="map">Mapping predicate used to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, TResult>(
            Func<T1, T2, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with three generic inputs.
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with four generic inputs.
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, TResult>(
            Func<T1, T2, T3, T4, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with five generic inputs.
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with six generic inputs.
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with seven generic inputs.
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with eight generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with nine generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with ten generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="T10">The tenth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with eleven generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="T10">The tenth type.</typeparam>
        /// <typeparam name="T11">The eleventh type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with twelve generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="T10">The tenth type.</typeparam>
        /// <typeparam name="T11">The eleventh type.</typeparam>
        /// <typeparam name="T12">The twelfth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with thirteen generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="T10">The tenth type.</typeparam>
        /// <typeparam name="T11">The eleventh type.</typeparam>
        /// <typeparam name="T12">The twelfth type.</typeparam>
        /// <typeparam name="T13">The thirteenth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with fourteen generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="T10">The tenth type.</typeparam>
        /// <typeparam name="T11">The eleventh type.</typeparam>
        /// <typeparam name="T12">The twelfth type.</typeparam>
        /// <typeparam name="T13">The thirteenth type.</typeparam>
        /// <typeparam name="T14">The fourteenth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with fifteen generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="T10">The tenth type.</typeparam>
        /// <typeparam name="T11">The eleventh type.</typeparam>
        /// <typeparam name="T12">The twelfth type.</typeparam>
        /// <typeparam name="T13">The thirteenth type.</typeparam>
        /// <typeparam name="T14">The fourteenth type.</typeparam>
        /// <typeparam name="T15">The fifteenth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);

        /// <summary>
        ///     Use a multimap query with sixteen generic inputs.
        ///     This is a shortcut over the {{Func<object[], TResult>}} offered
        ///     by Dapper. Use with caution. 
        ///     
        ///     If you're down here, what the hell are you doing, dude?! I mean, these
        ///     are offered as a convenience but c'mon - if you're composing 16 types 
        ///     here there's probably something wrong with your design.
        /// </summary>
        /// <typeparam name="T1">The first type.</typeparam>
        /// <typeparam name="T2">The second type.</typeparam>
        /// <typeparam name="T3">The third type.</typeparam>
        /// <typeparam name="T4">The fourth type.</typeparam>
        /// <typeparam name="T5">The fifth type.</typeparam>
        /// <typeparam name="T6">The sixth type.</typeparam>
        /// <typeparam name="T7">The seventh type.</typeparam>
        /// <typeparam name="T8">The eigth type.</typeparam>
        /// <typeparam name="T9">The ninth type.</typeparam>
        /// <typeparam name="T10">The tenth type.</typeparam>
        /// <typeparam name="T11">The eleventh type.</typeparam>
        /// <typeparam name="T12">The twelfth type.</typeparam>
        /// <typeparam name="T13">The thirteenth type.</typeparam>
        /// <typeparam name="T14">The fourteenth type.</typeparam>
        /// <typeparam name="T15">The fifteenth type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The mapping predicate to use to compose the result type.</param>
        /// <param name="parameters">Optional parameters to pass to the query.</param>        
        /// <param name="method">Optionally pass a method name to use as the key to finding a command setting.</param>
        /// <returns></returns>
        IEnumerable<TResult> Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> map,
            object parameters = null,
            [CallerMemberName] string method = null);
    }
}