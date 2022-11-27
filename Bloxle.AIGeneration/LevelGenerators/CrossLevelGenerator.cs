using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class CrossLevelGenerator : LevelGenerator
    {

        const int MIN_SQUARE_GRID_SIZE = 3;
        const int MAX_SQUARE_GRID_SIZE = 7;
        const double MIN_MOVES_GRID_AREA_RATIO = 0.2;
        const double MAX_MOVES_GRID_AREA_RATIO = 0.4;

        protected override void SetWidthAndHeight()
        {
            Random r = new Random();
            int width = 0;
            int height = 0;

             width = r.Next(MIN_SQUARE_GRID_SIZE, MAX_SQUARE_GRID_SIZE + 1);
             height = width;

            _width = width;
            _height = height;
        }

        protected override void SetNumberOfMoves()
        {
            Random r = new Random();

            _numberOfMoves = r.Next((int)(_width * _width * MIN_MOVES_GRID_AREA_RATIO + 1), (int)(_width * _width * MAX_MOVES_GRID_AREA_RATIO + 1));
        }


        protected override void InitialiseBlankGrid()
        {
            Level.InitBlankRectangle();

            int cutOutSquareWidth = _width >= 6 ? 2 : 1;

            foreach (var tile in Level.TileGrid)
            {
                
                if ((tile.Position.X + 1 <= cutOutSquareWidth && tile.Position.Y + 1 <= cutOutSquareWidth)
                    || (tile.Position.X + 1 > _width - cutOutSquareWidth && tile.Position.Y + 1 <= cutOutSquareWidth)
                    || (tile.Position.X + 1 <= cutOutSquareWidth && tile.Position.Y + 1 > _width - cutOutSquareWidth)
                    || (tile.Position.X + 1 > _width - cutOutSquareWidth && tile.Position.Y + 1 > _width - cutOutSquareWidth)
                    )
                {
                    tile.IsActive = false;
                }
            }
        }

        public override double CalculateDifficultyIndex()
        {
            return (2 * _width - 1) * _level.TargetScore;
        }
    }
}
