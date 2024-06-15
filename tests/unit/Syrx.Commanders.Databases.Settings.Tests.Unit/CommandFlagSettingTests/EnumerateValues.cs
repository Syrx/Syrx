//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.CommandFlagSettingTests
{
    public class EnumerateValues
    {
        [Theory]
        //[InlineData(CommandFlagSetting.None, 0)]
        //[InlineData(CommandFlagSetting.Buffered, 1)]
        //[InlineData(CommandFlagSetting.Pipelined, 2)]
        //[InlineData(CommandFlagSetting.NoCache, 4)]        
        //[InlineData(CommandFlagSetting.Buffered | CommandFlagSetting.Pipelined, 3)]
        //[InlineData(CommandFlagSetting.Buffered | CommandFlagSetting.NoCache, 5)]
        //[InlineData(CommandFlagSetting.Pipelined | CommandFlagSetting.NoCache, 6)]
        [MemberData(nameof(CommandFlagExtensions.CommandFlagSettings), MemberType = typeof(CommandFlagExtensions))]
        public void Successfully(CommandFlagSetting setting, int value)
        {
            Console.WriteLine($"Setting: {setting} Value: {value}");
            Equal(value, (int) setting);
        }
    }


    public class CommandFlagExtensions
    {
        public static IEnumerable<object[]> CommandFlagSettings {
            get {
                return EnumExtensions
            .GetFlags<CommandFlagSetting>()
            .Select(x => new object[] { x, (int) x });
            }
        }
    }


    public static class EnumExtensions
    {
        public static IEnumerable<TResult> GetFlags<TResult>(Func<TResult, bool> filter = null) where TResult : Enum
        {
            return filter == null
                ? Enum.GetValues(typeof(TResult)).Cast<TResult>()
                : Enum.GetValues(typeof(TResult)).Cast<TResult>().Where(filter);
        }

    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> Filter<TResult>(this IEnumerable<TResult> input, Func<TResult, bool> filter)
        {
            return filter != null ? input.Where(filter) : input;
        }
    }



}