using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class RingLevelGenerator : LevelGenerator
    {

        const int MIN_SQUARE_GRID_SIZE = 5;
        const int MAX_SQUARE_GRID_SIZE = 7;

        protected override void SetWidthAndHeight()
        {
            Random r = new Random();
            int width = 0;
            int height = 0;

            while (width % 2 == 0)
            {
                width = r.Next(MIN_SQUARE_GRID_SIZE, MAX_SQUARE_GRID_SIZE + 1);
                height = width;
            }

            _width = width;
            _height = height;

            _minMovesGridAreaRatio = 0.3;
            _maxMovesGridAreaRatio = 0.6;
        }

        protected override void InitialiseBlankGrid()
        {
            Level.InitBlankRectangle();

            foreach (var tile in Level.TileGrid)
            { 
                if ((_width <= 5 && tile.Position.X == _width / 2  && tile.Position.Y ==_width / 2)
                    ||
                    (_width > 5 && tile.Position.X - 1 <= _width / 2 && tile.Position.X + 1 >= _width / 2
                               && tile.Position.Y - 1 <= _width / 2 && tile.Position.Y + 1 >= _width / 2))
                {
                    tile.IsActive = false;
                }
            }
        }
    }
}
