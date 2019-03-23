//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:40)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Settings
{
    /// <summary>
    /// Represents the configuration of a command to be 
    /// executed against a data source.
    /// </summary>
    public interface ICommandSetting
    {
        /// <summary>
        /// Bridges this command setting to its connection. 
        /// </summary>
        string ConnectionAlias { get; } 
    }
}