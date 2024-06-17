namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public class TypeSettingOptionsBuilder<TType>
    {
        private string _name;
        private Dictionary<string, CommandSetting> _commands;

        public TypeSettingOptionsBuilder()
        {
            var type = typeof(TType);
            _name = type!.FullName!;
            _commands = [];
        }

        public TypeSettingOptionsBuilder<TType> ForMethod(string method, Action<CommandSettingOptionsBuilder> builder)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(method), nameof(method));
            Throw<ArgumentNullException>(builder != null, nameof(builder));

            var options = CommandSettingOptionsBuilderExtensions.Build(builder!);

            if (_commands.TryGetValue(method, out var setting))
            {
                return this;
            }

            _commands.Add(method, options!);
            return this;
        }

        protected internal TypeSetting Build()
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_name), nameof(_name));
            Throw<ArgumentException>(_commands.Count != 0, $"At least 1 command is expected to be set for type '{_name}'");

            return new TypeSetting { Commands = _commands, Name = _name };
        }
    }
}
