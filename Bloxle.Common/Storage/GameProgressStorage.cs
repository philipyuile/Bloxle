using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bloxle.Common.Storage
{
    public class GameProgressStorage
    {
        private readonly string _progressFolder;

        public GameProgressStorage(string progressFolder)
        {
            _progressFolder = progressFolder;
        }

        public GameProgress LoadGameProgressFile()
        {
            string path = _progressFolder + $"progress.json";
            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(path));
            }
            else
            {
                return new GameProgress { LevelsCompleted = new List<CompletedLevel>()};
            }
        }

        public void SaveGameProgressFile(GameProgress gameProgress)
        {
            if (!Directory.Exists(_progressFolder))
            {
                Directory.CreateDirectory(_progressFolder);
            }

            string path = _progressFolder + "progress.json";
            File.WriteAllText(path, JsonConvert.SerializeObject(gameProgress));
        }
    }
}
