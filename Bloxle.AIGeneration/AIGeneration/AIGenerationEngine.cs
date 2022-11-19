using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;
using Bloxle.Common.Enums;
using Bloxle.AIGeneration.Input;
using System;

using Bloxle.Common.Levels;

namespace Bloxle.AIGeneration.AIGeneration
{
    public class AIGenerationEngine
    {
        public string _levelFolder = "Content/Levels/";

        Level _levelGrid;

        const int INITIAL_GRID_SIZE = 3;

        const int GRID_SIZE_STEP = 5;

        IGameInput _input;
        IGameStorage _storage;

        public void Generate(int numberOfLevels)
        {

            for (int levelNumber = 1; levelNumber <= numberOfLevels; levelNumber++)
            {
                int gridSize = INITIAL_GRID_SIZE + (levelNumber - 1) / GRID_SIZE_STEP;

                _storage = new GenerationFileStorage(_levelFolder, levelNumber);

                Random r = new Random();
                //int numberOfMoves = r.Next(gridSize - 1, gridSize * gridSize / 2 + 2 * (levelNumber % GRID_SIZE_STEP));

                int numberOfMoves = gridSize * gridSize / 3 + (levelNumber - 1 % GRID_SIZE_STEP) - 1;
                int targetScore = numberOfMoves + (levelNumber - 1) / GRID_SIZE_STEP;

                _levelGrid = new Level(gridSize, gridSize, targetScore);
                _levelGrid.InitBlank(TileColour.Green);

                _input = new AIGenerationInput(_levelGrid);

                for (var moveNumber = 0; moveNumber < numberOfMoves; moveNumber++)
                {
                    ICommand inputCommand = _input.GetInputCommand();
                    inputCommand.Execute();
                }

                _storage.SaveGameFile(_levelGrid);
            }
        }
    }
}
