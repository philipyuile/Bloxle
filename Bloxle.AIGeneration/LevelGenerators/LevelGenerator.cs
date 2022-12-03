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

        protected double _minMovesGridAreaRatio = 0.2;
        protected double _maxMovesGridAreaRatio = 0.45;

        public void InitialiseLevel()
        {
            SetWidthAndHeight();
            _level = new Level(_width, _height);
            InitialiseBlankGrid();
            SetNumberOfMoves();
            _level.TargetScore = _numberOfMoves;
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
            return _level.TargetScore;
        }

        protected virtual void SetNumberOfMoves()
        {
            int gridArea = _level.TileGrid.Length;
            Random r = new Random();

            _numberOfMoves = r.Next((int)(gridArea * _minMovesGridAreaRatio + 1), (int)(gridArea * _maxMovesGridAreaRatio + 1));
        }

        protected abstract void SetWidthAndHeight();

        protected abstract void InitialiseBlankGrid();
    }
}
