
namespace Syrx.Commanders.Databases.Extensions.Configuration
{
    [Flags]
    public enum CommandFlagSetting
    {
        None = 0,
        Buffered = 1,
        Pipelined = 2,
        NoCache = 4
    }
}
