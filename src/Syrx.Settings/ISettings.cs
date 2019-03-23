//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:40)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System.Collections.Generic;

namespace Syrx.Settings
{
    /// <summary>
    /// This is the root settings element for all Syrx commanders.
    /// </summary>
    /// <typeparam name="TCommandSetting"></typeparam>
    public interface ISettings<TCommandSetting> where TCommandSetting : ICommandSetting
    {
        /// <summary>
        /// The collection of all types, grouped into namespaces. 
        /// </summary>
        IEnumerable<INamespaceSetting<TCommandSetting>> Namespaces { get; }        
    }
}