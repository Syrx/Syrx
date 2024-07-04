//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Connectors
{
    /// <summary>
    ///     Used to create database connections against an underlying data store.
    /// </summary>
    public interface IDatabaseConnector 
    {
        IDbConnection CreateConnection(CommandSetting setting);
    }
}

