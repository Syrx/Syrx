﻿namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public class NamespaceSettingOptionsBuilder
    {
        private string _namespace;
        //private List<TypeSetting> _types;
        private Dictionary<string, TypeSetting> _types;

        public NamespaceSettingOptionsBuilder()
        {
            _namespace = string.Empty;
            _types = [];
        }

        public NamespaceSettingOptionsBuilder ForType<TType>(Action<TypeSettingOptionsBuilder<TType>> builder)
        {
            var type = typeof(TType);
            var @namespace = type.Namespace;
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(@namespace), nameof(type.Namespace));
            Throw<ArgumentNullException>(builder != null, nameof(builder));

            var options = TypeSettingOptionsBuilderExtensions.Build(builder!);
            Throw<ArgumentNullException>(options != null, $"Error in adding command using ForType<> on '{type.FullName}'");

            _namespace = @namespace!;
            Evaluate(options!);
            return this;
        }

        public void Evaluate(TypeSetting option)
        {
            // pretty sure this can be done more elegantly. 
            if (_types.TryGetValue(option.Name, out var type))
            {
                foreach (var (method, command) in option.Commands)
                {
                    type.Commands.TryAdd(method, command);
                }
                return;
            }

            _types.Add(option.Name, option);
        }


        protected internal NamespaceSetting Build()
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_namespace), nameof(_namespace));
            Throw<ArgumentException>(_types.Any(), $"At least 1 type was expected to be added to {_namespace}");

            return new NamespaceSetting
            {
                Namespace = _namespace,
                Types = _types.Values.ToList()
            };
        }
    }
}
