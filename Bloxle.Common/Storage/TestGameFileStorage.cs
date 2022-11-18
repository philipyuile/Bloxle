using Bloxle.Common.Interfaces;
using Newtonsoft.Json;
using System.IO;

using Bloxle.Common.Levels;

namespace Bloxle.Common.Storage
{
    public class TestGameFileStorage : IGameStorage
    {
        private readonly string _levelFolder;

        public TestGameFileStorage(string levelFolder)
        {
            _levelFolder = levelFolder;
        }

        public Level LoadGameFile()
        {
            string path = _levelFolder + "testgameblank.json";
            return JsonConvert.DeserializeObject<Level>(File.ReadAllText(path));
        }

        public void SaveGameFile(Level tileGrid)
        {
            if (!Directory.Exists(_levelFolder))
            {
                Directory.CreateDirectory(_levelFolder);
            }

            string path = _levelFolder + "testgameblank.json";
            File.WriteAllText(path, JsonConvert.SerializeObject(tileGrid));
        }

    }
}
