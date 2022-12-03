using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class CrossLevelGenerator : LevelGenerator
    {

        const int MIN_SQUARE_GRID_SIZE = 5;
        const int MAX_SQUARE_GRID_SIZE = 7;

        protected override void SetWidthAndHeight()
        {
            Random r = new Random();
            int width = 0;
            int height = 0;

             width = r.Next(MIN_SQUARE_GRID_SIZE, MAX_SQUARE_GRID_SIZE + 1);
             height = width;

            _width = width;
            _height = height;

            _minMovesGridAreaRatio = 0.3;
            _maxMovesGridAreaRatio = 0.6;
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
    }
}
