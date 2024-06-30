using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Syrx.Commanders.Databases.Oracle
{
    /// <summary>
    /// Provides multiple result set support to Syrx (via Dapper) from Oracle. 
    /// This is necessary so that all implementations of the Syrx database commander
    /// provide support for multiple result sets. 
    /// </summary>
    /// <notes>
    /// Lifted entirely from https://stackoverflow.com/a/41110515
    /// Thank you to nw. and greyseal96
    /// </notes>

    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters dynamicParameters;

        private readonly List<OracleParameter> oracleParameters = new List<OracleParameter>();

        public OracleDynamicParameters(params string[] refCursorNames)
        {
            dynamicParameters = new DynamicParameters();
            AddRefCursorParameters(refCursorNames);
        }

        public OracleDynamicParameters(object template, params string[] refCursorNames)
        {
            dynamicParameters = new DynamicParameters(template);
            AddRefCursorParameters(refCursorNames);
        }

        private void AddRefCursorParameters(params string[] refCursorNames)
        {
            foreach (string refCursorName in refCursorNames)
            {
                var oracleParameter = new OracleParameter(refCursorName, OracleDbType.RefCursor, ParameterDirection.Output);
                oracleParameters.Add(oracleParameter);
            }
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters) dynamicParameters).AddParameters(command, identity);
            var oracleCommand = command as OracleCommand;
            if (oracleCommand != null)
            {
                oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }

        /// <summary>
        /// Returns an array of cursor names using an incrementing number. 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static OracleDynamicParameters Cursors(int size = 16) => 
            new OracleDynamicParameters(Enumerable.Range(1, size).Select(i => i.ToString()).ToArray());
    }
}
