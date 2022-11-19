using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;
using Bloxle.Common.Enums;
using Bloxle.AIGeneration.Input;
using System;

using Bloxle.Common.Levels;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Bloxle.AIGeneration.AIGeneration
{
    public class RandomDifficultyIndexGenerationEngine
    {
        public const string LEVEL_FOLDER = "Content/Levels/";

        IList<Level> _levelGrids;

        const int MIN_GRID_SIZE = 2;
        const int MIN_SQUARE_GRID_SIZE = 3;
        const int MAX_GRID_SIZE = 6;
        const int MIN_SUM_GRID_LENGTH_WIDTH = 6;
        const int MAX_SUM_GRID_LENGTH_WIDTH = 10;

        const double MIN_MOVES_GRID_AREA_RATIO = 0.2;
        const double MAX_MOVES_GRID_AREA_RATIO = 0.5;

        IGameInput _input;
        ILevelSetStorage _storage;

        public void Generate(int numberOfLevels)
        {
            _levelGrids = new List<Level>();

            for (int levelCount = 1; levelCount <= numberOfLevels; levelCount++)
            {
                Random r = new Random();
                int width = 0;
                int height = 0;
                bool isSquare = false;
                
                while (height + width < MIN_SUM_GRID_LENGTH_WIDTH || height + width > MAX_SUM_GRID_LENGTH_WIDTH)
                {
                    isSquare = r.NextDouble() > 0.2;
                    if (isSquare)
                    {
                        width = r.Next(MIN_SQUARE_GRID_SIZE, MAX_GRID_SIZE);
                        height = width;
                    }
                    else
                    {
                        width = r.Next(MIN_GRID_SIZE, MAX_GRID_SIZE);
                        height = r.Next(MIN_GRID_SIZE, MAX_GRID_SIZE);
                    }
                }

                int numberOfMoves = r.Next((int)(width * height * MIN_MOVES_GRID_AREA_RATIO + 1), (int) (width * height * MAX_MOVES_GRID_AREA_RATIO + 1));

                Level levelGrid = new Level(width, height, numberOfMoves);               
                levelGrid.InitBlank(TileColour.Green);

                _input = new AIGenerationInput(levelGrid);

                for (var moveNumber = 0; moveNumber < numberOfMoves; moveNumber++)
                {
                    ICommand inputCommand = _input.GetInputCommand();
                    inputCommand.Execute();
                }

                _levelGrids.Add(levelGrid);
            }

            _levelGrids = _levelGrids.OrderBy(x => CalculateDifficultyIndex(x)).ToList();

            _storage = new LevelSetFilesStorage(LEVEL_FOLDER);

            int levelNumber = 1;
            foreach (var levelGrid in _levelGrids)
            {
                _storage.SaveGameFile(levelNumber, levelGrid);
                levelNumber++;
            }
        }

        private double CalculateDifficultyIndex(Level levelGrid)
        {
            return (levelGrid.Width + levelGrid.Height) * levelGrid.TargetScore;
        }
    }
}
