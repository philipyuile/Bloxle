﻿using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class SquareLevelGenerator : LevelGenerator
    {
        const int MIN_SQUARE_GRID_SIZE = 3;
        const int MAX_SQUARE_GRID_SIZE = 6;
        const int MIN_SUM_GRID_LENGTH_WIDTH = 6;
        const int MAX_SUM_GRID_LENGTH_WIDTH = 10;
        const double MIN_MOVES_GRID_AREA_RATIO = 0.2;
        const double MAX_MOVES_GRID_AREA_RATIO = 0.5;

        protected override void SetWidthAndHeight()
        {
            Random r = new Random();
            int width = 0;
            int height = 0;

            while (height + width < MIN_SUM_GRID_LENGTH_WIDTH || height + width > MAX_SUM_GRID_LENGTH_WIDTH)
            {
                width = r.Next(MIN_SQUARE_GRID_SIZE, MAX_SQUARE_GRID_SIZE);
                height = width;
            }

            _width = width;
            _height = height;
        }

        protected override void SetNumberOfMoves()
        {
            Random r = new Random();

            _numberOfMoves = r.Next((int)(_width * _height * MIN_MOVES_GRID_AREA_RATIO + 1), (int)(_width * _height * MAX_MOVES_GRID_AREA_RATIO + 1));
        }

        protected override void InitialiseBlankGrid()
        {
            Level.InitBlankRectangle();
        }
    }
}
