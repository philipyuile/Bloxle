using Microsoft.Xna.Framework;

using Bloxle.Common.Interfaces;
using Bloxle.Common.Commands;
using Bloxle.Common.Input;
using Bloxle.Common.Levels;

using System;
using System.Collections.Generic;

namespace Bloxle.AIGeneration.Input
{
    public class AIGenerationInput : IGameInput
    {
        private Level _tileGrid;
        private int[,] _inputMask;
        private Dictionary<string, int> _alreadyMovedHereLookup;

        public AIGenerationInput(Level tileGrid) {
            _tileGrid = tileGrid;
            _inputMask = InputMask.UndoMask;
            _alreadyMovedHereLookup = new Dictionary<string, int>();
        }

        public ICommand GetInputCommand()
        {
            Random r = new Random();

            int x;
            int y;
            string lookupKey;
            int numberOfMovesOnAlreadySquare;

            do
            {
                do
                {
                    x = r.Next(_tileGrid.Width);
                    y = r.Next(_tileGrid.Height);
                }
                while (!_tileGrid.IsWithinBounds(new Vector2(x, y)));

                lookupKey = $"{x},{y}";

                if (!_alreadyMovedHereLookup.TryGetValue(lookupKey, out numberOfMovesOnAlreadySquare))
                {
                    _alreadyMovedHereLookup[lookupKey] = 0;
                    numberOfMovesOnAlreadySquare = 0;
                }

            } while (numberOfMovesOnAlreadySquare >= 3);

            _alreadyMovedHereLookup[lookupKey]++;

            Vector2 tilePosition = new Vector2(x,y);

            ICommand command = new CycleCommand(_tileGrid, tilePosition, _inputMask);

            return command;
        }
    }
}