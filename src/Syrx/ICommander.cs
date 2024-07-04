//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:39)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx
{
    /// <inheritdoc />
    /// <summary>
    ///     Basically breaks down into two distinct operations for writes (Execute) and reads (Query).
    /// </summary>
    public partial interface ICommander<TRepository> : IDisposable
    {
    }
}