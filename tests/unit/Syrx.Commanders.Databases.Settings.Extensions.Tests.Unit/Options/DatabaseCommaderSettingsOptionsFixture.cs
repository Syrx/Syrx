// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.12.17 (19:35)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options
{
    public class DatabaseCommaderSettingsOptionsFixture
    {
        public FileInfo JsonFile { get; }

        public FileInfo CommandJsonFile { get; }

        public DatabaseCommaderSettingsOptionsFixture()
        {
            var path = $"{nameof(DatabaseCommaderSettingsOptionsFixture)}.json";
            var settings = BuildTestSettings();
            JsonFile = PersistToFile(path, settings);
        }


        private DatabaseCommanderSettings BuildTestSettings()
        {
            return DatabaseCommanderSettingsOptionsBuilder
                .Build(options => options
                    .AddConnectionString("test-alias", "test-connection-string")
                    .AddCommand(o => o
                        .ForRepositoryType<DatabaseCommaderSettingsOptionsFixture>()
                        .ForMethodNamed("MethodA")
                        .UseCommandText("select 'MethodA'")
                        .AgainstConnectionAlias("test-alias"))
                );
        }

        //public SyrxConfigurationSection BuildConfigurationSection()
        //{
        //    var settings = BuildTestSettings();
        //    return new SyrxConfigurationSection { Syrx = settings };
        //}

        public FileInfo PersistToFile<TSettings>(string path, TSettings settings, bool delete = true)
        {
            var file = new FileInfo(path);
            if (file.Exists && delete)
            {
                file.Delete();
            }

            var serialized = JsonConvert.SerializeObject(settings, Formatting.Indented);
            using var stream = file.OpenWrite();
            using var writer = new StreamWriter(stream);
            writer.Write(serialized);
            writer.Close();

            return file;
        }

        public FileInfo PersistToFile(string path, DatabaseCommanderSettings settings)
        {
            return PersistToFile(path, settings, true);
        }
    }
}