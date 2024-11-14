//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================
namespace Syrx.Commanders.Databases
{
    public partial class DatabaseCommander<TRepository> : ICommander<TRepository>
    {
        private readonly IDatabaseConnector _connector;
        private readonly IDatabaseCommandReader _reader;
        private readonly Type _type;

        public void Dispose() { }

        public DatabaseCommander(IDatabaseCommandReader reader, IDatabaseConnector connector)
        {
            _reader = reader;
            _connector = connector;
            _type = typeof(TRepository);
        }

        [Browsable(false)]
        private CommandDefinition GetCommandDefinition(
            CommandSetting setting,
            object parameters = null,
            IDbTransaction transaction = null,
            CancellationToken cancellationToken = default)
        {
            return new CommandDefinition(
                commandText: setting.CommandText,
                parameters: parameters,
                transaction: transaction,
                commandTimeout: setting.CommandTimeout,
                commandType: setting.CommandType,
                flags: (CommandFlags) setting.Flags,
                cancellationToken: cancellationToken);
        }
    }
}