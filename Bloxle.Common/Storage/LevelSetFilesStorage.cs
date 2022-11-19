using Bloxle.Common.Interfaces;
using Newtonsoft.Json;
using System.IO;

using Bloxle.Common.Levels;

namespace Bloxle.Common.Storage
{
    public class LevelSetFilesStorage : ILevelSetStorage
    {
        private readonly string _levelsFolder;

        public LevelSetFilesStorage(string levelsFolder)
        {
            _levelsFolder = levelsFolder;
        }

        public Level LoadGameFile(int levelNumber)
        {
            string path = _levelsFolder + $"level{levelNumber}.json";
            return JsonConvert.DeserializeObject<Level>(File.ReadAllText(path));
        }

        public void SaveGameFile(int levelNumber, Level tileGrid)
        {
            if (!Directory.Exists(_levelsFolder))
            {
                Directory.CreateDirectory(_levelsFolder);
            }

            string path = _levelsFolder + $"level{levelNumber}.json";
            File.WriteAllText(path, JsonConvert.SerializeObject(tileGrid));
        }
    }
}
