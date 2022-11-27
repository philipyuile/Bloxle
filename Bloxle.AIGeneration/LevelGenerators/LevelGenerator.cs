using Bloxle.AIGeneration.Input;
using Bloxle.AIGeneration.Interfaces;
using Bloxle.Common.Interfaces;
using Bloxle.Common.Levels;
using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public abstract class LevelGenerator : ILevelGenerator
    {
        protected int _width;
        protected int _height;
        protected int _numberOfMoves;
        protected Level _level;

        public void InitialiseLevel()
        {
            SetWidthAndHeight();
            SetNumberOfMoves();
            _level = new Level(_width, _height, _numberOfMoves);
            InitialiseBlankGrid();
        }

        public Level Level{ get { return _level; } }

        public virtual Level GenerateLevel()
        {
            AIGenerationInput input = new AIGenerationInput(_level);

            for (var moveNumber = 0; moveNumber < _numberOfMoves; moveNumber++)
            {
                ICommand inputCommand = input.GetInputCommand();
                inputCommand.Execute();
            }
            return _level;
        }

        public virtual double CalculateDifficultyIndex()
        {
            return (_width + _height) * _level.TargetScore;
        }

        protected abstract void SetWidthAndHeight();

        protected abstract void SetNumberOfMoves();

        protected abstract void InitialiseBlankGrid();
    }
}
