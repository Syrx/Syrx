namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public class CommanderOptionsBuilder
    {
        private Dictionary<string, ConnectionStringSetting> _connectionStrings;
        private Dictionary<string, NamespaceSetting> _options;

        public CommanderOptionsBuilder()
        {
            _connectionStrings = [];
            _options = [];
        }

        public CommanderOptionsBuilder AddConnectionString(string alias, string connectionString)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(alias), nameof(alias));
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(connectionString), nameof(connectionString));
            Action<ConnectionStringOptionsBuilder> builder = (a) => a.UseAlias(alias).UseConnectionString(connectionString);
            return AddConnectionString(builder);
        }

        public CommanderOptionsBuilder AddConnectionString(Action<ConnectionStringOptionsBuilder> builder)
        {
            var options = ConnectionStringOptionsBuilderExtensions.Build(builder);

            if (_connectionStrings.TryGetValue(options.Alias, out var setting))
            {
                if (setting.ConnectionString != options.ConnectionString)
                {
                    Throw<ArgumentException>(
                    setting.ConnectionString == options.ConnectionString,
                    @$"The alias '{options.Alias}' is already assigned to a different connection string. 
Current connection string: {setting.ConnectionString}
New connection string: {options.ConnectionString}");
                }

                _connectionStrings[options.Alias] = options;
                return this;
            }

            _connectionStrings.Add(options.Alias, options);

            return this;
        }

        public CommanderOptionsBuilder AddCommand(Action<NamespaceSettingOptionsBuilder> builder)
        {
            var options = NamespaceSettingOptionsBuilderExtensions.Build(builder);
            return AddCommand(options);
        }

        public CommanderOptionsBuilder AddCommand(NamespaceSetting options)
        {
            Throw<ArgumentNullException>(options != null, nameof(options));
            Evaluate(options!);
            return this;
        }

        public void Evaluate(NamespaceSetting option)
        {
            // pretty sure this can be done more elegantly. 
            if (_options.TryGetValue(option.Namespace, out var ns))
            {
                // for this namespace, we need to check that the 
                // types collection doesn't already contain the 
                // type being passed by 'option'. 

                // for the current approach we need to 
                // evaluate each of the types in 'option.Types'
                // to see if they should be assigned to that type. 

                foreach (var type in option.Types)
                {
                    // evaluate whether we are already hosting this type. 
                    var entry = ns.Types.SingleOrDefault(x => x.Name == type.Name);

                    // if that's true, add these type.Commands to that that type. 
                    if (entry != null)
                    {
                        foreach (var (method, command) in type.Commands)
                        {
                            entry.Commands.TryAdd(method, command);
                        }
                        return;
                    }

                    // as we're already in the same namespace, add
                    // to that one instead. 
                    ns.Types.AddRange(option.Types);
                    return;
                }
            }

            _options.Add(option.Namespace, option);
        }

        private IEnumerable<NamespaceSetting> ValidateNamespaceCollections(IEnumerable<NamespaceSetting> collection)
        {
            Throw<ArgumentException>(collection.Any(), "Collection should have at least 1 namespace setting. Use the AddCommand method to add a new entry to the collection.");
            return collection;
        }

        protected internal CommanderSettings Build()
        {
            // the connections collections can be empty as we 
            // might want tp pull these from a different source/secrets. 
            var connections = _connectionStrings.Values.ToList();
            var namespaces = ValidateNamespaceCollections(_options.Values);


            return new CommanderSettings { Namespaces = namespaces.ToList(), Connections = connections };
        }
    }

}
