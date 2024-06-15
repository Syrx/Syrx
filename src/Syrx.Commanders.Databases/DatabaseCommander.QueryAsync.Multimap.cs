//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================
namespace Syrx.Commanders.Databases
{
    public sealed partial class DatabaseCommander<TRepository> //: ICommander
    {
        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(object parameters = null,
            CancellationToken cancellationToken = default, [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters, cancellationToken: cancellationToken);
            var connection = _connector.CreateConnection(setting);
            return await connection.QueryAsync<TResult>(command);
        }

        public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, TResult>(Func<T1, T2, TResult> map,
            object parameters = null, CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters, cancellationToken: cancellationToken);
            var connection = _connector.CreateConnection(setting);
            return await connection.QueryAsync(
                command.CommandText,
                map,
                buffered: command.Buffered,
                param: command.Parameters,
                splitOn: setting.Split,
                commandTimeout: setting.CommandTimeout,
                commandType: setting.CommandType);
        }

        public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> map,
            object parameters = null, CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters, cancellationToken: cancellationToken);
            var connection = _connector.CreateConnection(setting);
            return await connection.QueryAsync(
                command.CommandText,
                map,
                buffered: command.Buffered,
                param: command.Parameters,
                splitOn: setting.Split,
                commandTimeout: setting.CommandTimeout,
                commandType: setting.CommandType);
        }

        public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> map,
            object parameters = null, CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters, cancellationToken: cancellationToken);
            var connection = _connector.CreateConnection(setting);
            return await connection.QueryAsync(
                command.CommandText,
                map,
                buffered: command.Buffered,
                param: command.Parameters,
                splitOn: setting.Split,
                commandTimeout: setting.CommandTimeout,
                commandType: setting.CommandType);
        }

        public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, TResult>(
            Func<T1, T2, T3, T4, T5, TResult> map, object parameters = null,
            CancellationToken cancellationToken = default, [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters, cancellationToken: cancellationToken);
            var connection = _connector.CreateConnection(setting);
            return await connection.QueryAsync(
                command.CommandText,
                map,
                buffered: command.Buffered,
                param: command.Parameters,
                splitOn: setting.Split,
                commandTimeout: setting.CommandTimeout,
                commandType: setting.CommandType);
        }

        public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, TResult>(
            Func<T1, T2, T3, T4, T5, T6, TResult> map, object parameters = null,
            CancellationToken cancellationToken = default, [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters, cancellationToken: cancellationToken);
            var connection = _connector.CreateConnection(setting);
            return await connection.QueryAsync(
                command.CommandText,
                map,
                buffered: command.Buffered,
                param: command.Parameters,
                splitOn: setting.Split,
                commandTimeout: setting.CommandTimeout,
                commandType: setting.CommandType);
        }

        public async Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> map, object parameters = null,
            CancellationToken cancellationToken = default, [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters, cancellationToken: cancellationToken);
            var connection = _connector.CreateConnection(setting);
            return await connection.QueryAsync(
                command.CommandText,
                map,
                buffered: command.Buffered,
                param: command.Parameters,
                splitOn: setting.Split,
                commandTimeout: setting.CommandTimeout,
                commandType: setting.CommandType);
        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8)
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];

                return map(one, two, three, four, five, six, seven, eight);
            };


            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);

        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9)
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];

                return map(one, two, three, four, five, six, seven, eight, nine);
            };


            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10)
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];
                var ten = (T10) a[9];

                return map(one, two, three, four, five, six, seven, eight, nine, ten);
            };


            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T11),
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];
                var ten = (T10) a[9];
                var eleven = (T11) a[10];

                return map(one, two, three, four, five, six, seven, eight, nine, ten, eleven);
            };

            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T11),
                typeof(T12)
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];
                var ten = (T10) a[9];
                var eleven = (T11) a[10];
                var twelve = (T12) a[11];

                return map(one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve);
            };

            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T11),
                typeof(T12),
                typeof(T13),
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];
                var ten = (T10) a[9];
                var eleven = (T11) a[10];
                var twelve = (T12) a[11];
                var thirteen = (T13) a[12];

                return map(
                    one,
                    two,
                    three,
                    four,
                    five,
                    six,
                    seven,
                    eight,
                    nine,
                    ten,
                    eleven,
                    twelve,
                    thirteen);
            };

            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T11),
                typeof(T12),
                typeof(T13),
                typeof(T14)
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];
                var ten = (T10) a[9];
                var eleven = (T11) a[10];
                var twelve = (T12) a[11];
                var thirteen = (T13) a[12];
                var fourteen = (T14) a[13];

                return map(
                    one,
                    two,
                    three,
                    four,
                    five,
                    six,
                    seven,
                    eight,
                    nine,
                    ten,
                    eleven,
                    twelve,
                    thirteen,
                    fourteen);
            };

            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }

        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T11),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15)
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];
                var ten = (T10) a[9];
                var eleven = (T11) a[10];
                var twelve = (T12) a[11];
                var thirteen = (T13) a[12];
                var fourteen = (T14) a[13];
                var fifteen = (T15) a[14];

                return map(
                    one,
                    two,
                    three,
                    four,
                    five,
                    six,
                    seven,
                    eight,
                    nine,
                    ten,
                    eleven,
                    twelve,
                    thirteen,
                    fourteen,
                    fifteen);
            };

            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }


        public Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> map,
            object parameters = null,
            CancellationToken cancellationToken = default,
            [CallerMemberName] string method = null)
        {
            var setting = _reader.GetCommand(_type, method);
            var command = GetCommandDefinition(setting, parameters);

            var types = new Type[]
            {
                typeof(T1),
                typeof(T2),
                typeof(T3),
                typeof(T4),
                typeof(T5),
                typeof(T6),
                typeof(T7),
                typeof(T8),
                typeof(T9),
                typeof(T10),
                typeof(T11),
                typeof(T12),
                typeof(T13),
                typeof(T14),
                typeof(T15),
                typeof(T16)
            };

            Func<object[], TResult> internalMapper = (a) =>
            {
                var one = (T1) a[0];
                var two = (T2) a[1];
                var three = (T3) a[2];
                var four = (T4) a[3];
                var five = (T5) a[4];
                var six = (T6) a[5];
                var seven = (T7) a[6];
                var eight = (T8) a[7];
                var nine = (T9) a[8];
                var ten = (T10) a[9];
                var eleven = (T11) a[10];
                var twelve = (T12) a[11];
                var thirteen = (T13) a[12];
                var fourteen = (T14) a[13];
                var fifteen = (T15) a[14];
                var sixteen = (T16) a[15];

                return map(
                    one,
                    two,
                    three,
                    four,
                    five,
                    six,
                    seven,
                    eight,
                    nine,
                    ten,
                    eleven,
                    twelve,
                    thirteen,
                    fourteen,
                    fifteen,
                    sixteen);
            };

            var connection = _connector.CreateConnection(setting);
            return connection.QueryAsync(
                    sql: command.CommandText,
                    types: types,
                    map: internalMapper,
                    param: parameters,
                    buffered: command.Buffered,
                    splitOn: setting.Split,
                    commandTimeout:
                    command.CommandTimeout,
                    commandType: setting.CommandType);
        }

    }
}