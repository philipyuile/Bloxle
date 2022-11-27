using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;
using Bloxle.AIGeneration.Input;
using System;

using Bloxle.Common.Levels;
using System.Collections.Generic;
using System.Linq;
using Bloxle.AIGeneration.Interfaces;
using Bloxle.AIGeneration.LevelGenerators;

namespace Bloxle.AIGeneration.AIGeneration
{
    public class RandomDifficultyIndexGenerationEngine
    {
        public const string LEVEL_FOLDER = "Content/Levels/";

        public void Generate(int numberOfLevels)
        {
            IList<ILevelGenerator> levelGenerators = new List<ILevelGenerator>();

            for (int levelCount = 1; levelCount <= numberOfLevels; levelCount++)
            {
                ILevelGenerator levelGenerator;
                Random r = new Random();
                var randomDouble = r.NextDouble();
                if (randomDouble < 0.2)
                {
                    levelGenerator = new RingLevelGenerator();
                }
                else if (randomDouble < 0.8)
                {
                    levelGenerator = new SquareLevelGenerator();
                }
                else
                {
                    levelGenerator = new RectangleLevelGenerator();
                }

                levelGenerator.InitialiseLevel();

                levelGenerator.GenerateLevel();

                levelGenerators.Add(levelGenerator);
            }

            IList<Level> levelGrids = levelGenerators.OrderBy(x => x.CalculateDifficultyIndex()).Select(y => y.Level).ToList();

            ILevelSetStorage _storage = new LevelSetFilesStorage(LEVEL_FOLDER);

            int levelNumber = 1;
            foreach (var levelGrid in levelGrids)
            {
                _storage.SaveGameFile(levelNumber, levelGrid);
                levelNumber++;
            }
        }
    }
}
