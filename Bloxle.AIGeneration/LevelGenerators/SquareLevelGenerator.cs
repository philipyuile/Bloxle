using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class SquareLevelGenerator : LevelGenerator
    {
        const int MIN_SQUARE_GRID_SIZE = 3;
        const int MAX_SQUARE_GRID_SIZE = 6;
        const int MIN_SUM_GRID_LENGTH_WIDTH = 6;

        protected override void SetWidthAndHeight()
        {
            Random r = new Random();
            int width = 0;
            int height = 0;

            while (height + width < MIN_SUM_GRID_LENGTH_WIDTH)
            {
                width = r.Next(MIN_SQUARE_GRID_SIZE, MAX_SQUARE_GRID_SIZE + 1);
                height = width;
            }

            _width = width;
            _height = height;
            _minMovesGridAreaRatio = 0.2;
            _maxMovesGridAreaRatio = 0.5;

        }

        protected override void InitialiseBlankGrid()
        {
            Level.InitBlankRectangle();
        }
    }
}
