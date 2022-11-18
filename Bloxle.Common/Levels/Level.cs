using Microsoft.Xna.Framework;
using System;

using Bloxle.Common.Enums;

namespace Bloxle.Common.Levels
{
    public class Level
    {
        public Tile[,] _tileGrid;

        public int _width;
        public int _height;
        public int _targetScore;

        public readonly int _numberOfColours;

        public Level(int width, int height)
        {
            _width = width;
            _height = height;
            _tileGrid = new Tile[_width, _height];
            _numberOfColours = Enum.GetNames(typeof(TileColour)).Length;
            _targetScore = 0;
        }

        public bool AllTilesAreThisColour(TileColour colour)
        {
            foreach (var tile in _tileGrid)
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
            if (tilePosition.X >= 0 && tilePosition.Y >= 0 && tilePosition.X < _width && tilePosition.Y < _height)
            {
                return true;
            }
            return false;
        }

        public bool CycleTilesFromPosition(Vector2 tilePosition, int[,] inputMask)
        {
            if (tilePosition.X < 0 || tilePosition.Y < 0 || tilePosition.X >= _width || tilePosition.Y >= _height)
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

                    if (absoluteXPos >= 0 && absoluteXPos < _width && absoluteYPos >= 0 && absoluteYPos < _height)
                    {
                        _tileGrid[absoluteXPos,absoluteYPos].colour = (TileColour)((int)((_tileGrid[absoluteXPos, absoluteYPos].colour) + inputMask[i,j] + _numberOfColours) % _numberOfColours);
                    }
                }
            }

            return true;
        }

        public void InitBlank(TileColour tileColour)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _tileGrid[i, j] = new Tile { colour = tileColour, position = new Vector2(i, j) };
                }
            }
        }

        public int GetTargetScore()
        {
            return _targetScore;
        }

        public void SetTargetScore(int targetScore)
        {
            _targetScore = targetScore;
        }
    }
}
