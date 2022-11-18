using Bloxle.Common.Interfaces;
using Newtonsoft.Json;
using System.IO;

using Bloxle.Common.Levels;

namespace Bloxle.Common.Storage
{
    public class GenerationFileStorage : IGameStorage
    {
        private readonly string _levelFolder;

        private int _levelNumber;

        public GenerationFileStorage(string levelFolder, int levelNumber)
        {
            _levelFolder = levelFolder;
            _levelNumber = levelNumber;
        }

        public Level LoadGameFile()
        {
            string path = _levelFolder + $"level{_levelNumber}.json";
            return JsonConvert.DeserializeObject<Level>(File.ReadAllText(path));
        }

        public void SaveGameFile(Level tileGrid)
        {
            if (!Directory.Exists(_levelFolder))
            {
                Directory.CreateDirectory(_levelFolder);
            }

            string path = _levelFolder + $"level{_levelNumber}.json";
            File.WriteAllText(path, JsonConvert.SerializeObject(tileGrid));
        }
    }
}
