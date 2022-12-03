using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class RectangleLevelGenerator : LevelGenerator
    {
        const int MIN_GRID_SIZE = 2;
        const int MAX_GRID_SIZE = 6;
        const int MIN_SUM_GRID_LENGTH_WIDTH = 6;

        protected override void SetWidthAndHeight()
        {
            Random r = new Random();
            int width = 0;
            int height = 0;

            while (height + width < MIN_SUM_GRID_LENGTH_WIDTH || height == width)
            {
                width = r.Next(MIN_GRID_SIZE, MAX_GRID_SIZE + 1);
                height = r.Next(MIN_GRID_SIZE, MAX_GRID_SIZE + 1);
            }

            _width = width;
            _height = height;
            _minMovesGridAreaRatio = 0.25;
            _maxMovesGridAreaRatio = 0.55;
        }

        protected override void InitialiseBlankGrid()
        {
            Level.InitBlankRectangle();
        }
    }
}
