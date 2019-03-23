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
    /// Represents a type which will be used for persistence and retrieval operations. 
    /// This is typically a repository.
    /// </summary>
    /// <typeparam name="TCommandSetting"></typeparam>
    public interface ITypeSetting<TCommandSetting> where TCommandSetting : ICommandSetting
    {
        /// <summary>
        /// The fully qualified name of the type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The collection of commands used by this type for peristence and retrieval operations. 
        /// </summary>
        IDictionary<string, TCommandSetting> Commands { get; }
    }
}