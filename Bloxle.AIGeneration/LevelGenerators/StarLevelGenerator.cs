using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class StarLevelGenerator : LevelGenerator
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

            _minMovesGridAreaRatio = 0.2;
            _maxMovesGridAreaRatio = 0.55;
        }

        protected override void InitialiseBlankGrid()
        {
            Level.InitBlankRectangle();

            int extraCutOut = _width >= 7 ? 1 : 0;

            foreach (var tile in Level.TileGrid)
            {
                
                if ((tile.Position.X <= extraCutOut && tile.Position.Y <= _width / 2 + extraCutOut && tile.Position.Y >= _width / 2 - extraCutOut)
                    || (tile.Position.Y <= extraCutOut && tile.Position.X <= _width / 2 + extraCutOut && tile.Position.X >= _width / 2 - extraCutOut)
                    || (tile.Position.X + 1 >= _width - extraCutOut && tile.Position.Y <= _width / 2 + extraCutOut && tile.Position.Y >= _width / 2 - extraCutOut)
                    || (tile.Position.Y + 1 >= _width - extraCutOut && tile.Position.X <= _width / 2 + extraCutOut && tile.Position.X >= _width / 2 - extraCutOut)
                    )
                {
                    tile.IsActive = false;
                }
            }
        }
    }
}
