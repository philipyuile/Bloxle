using Microsoft.Xna.Framework;
using System;

using Bloxle.Common.Enums;

namespace Bloxle.Common.Levels
{
    public class Level
    {
        public Tile[,] TileGrid;

        public int Width { get; set; }
        public int Height { get; set; }
        public int TargetScore { get; set; }

        public readonly int _numberOfColours;

        public Level(int width, int height, int targetScore)
        {
            Width = width;
            Height = height;
            TileGrid = new Tile[Width, Height];
            _numberOfColours = Enum.GetNames(typeof(TileColour)).Length;
            TargetScore = targetScore;
        }

        public bool AllTilesAreThisColour(TileColour colour)
        {
            foreach (var tile in TileGrid)
            {
                if (tile.colour != colour)
                {
                    return false;
                }
            }

            return true;
        }

        public bool WithinBounds(Vector2 tilePosition)
        {
            if (tilePosition.X >= 0 && tilePosition.Y >= 0 && tilePosition.X < Width && tilePosition.Y < Height)
            {
                return true;
            }
            return false;
        }

        public bool CycleTilesFromPosition(Vector2 tilePosition, int[,] inputMask)
        {
            if (tilePosition.X < 0 || tilePosition.Y < 0 || tilePosition.X >= Width || tilePosition.Y >= Height)
            {
                return false;
            }

            var x = (int)tilePosition.X;
            var y = (int)tilePosition.Y;
            var maskOffset = (inputMask.GetLength(0) - 1) / 2;

            for (int i = 0; i < inputMask.GetLength(0); i++)
            {
                for (int j = 0; j < inputMask.GetLength(1); j++)
                {
                    var absoluteXPos = x + j - maskOffset;
                    var absoluteYPos = y + i - maskOffset;

                    if (absoluteXPos >= 0 && absoluteXPos < Width && absoluteYPos >= 0 && absoluteYPos < Height)
                    {
                        TileGrid[absoluteXPos,absoluteYPos].colour = (TileColour)((int)((TileGrid[absoluteXPos, absoluteYPos].colour) + inputMask[i,j] + _numberOfColours) % _numberOfColours);
                    }
                }
            }

            return true;
        }

        public void InitBlank(TileColour tileColour)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    TileGrid[i, j] = new Tile { colour = tileColour, position = new Vector2(i, j) };
                }
            }
        }
    }
}
