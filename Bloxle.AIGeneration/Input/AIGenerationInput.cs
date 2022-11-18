using Microsoft.Xna.Framework;

using Bloxle.Common.Interfaces;
using Bloxle.Common.Commands;
using Bloxle.Common.Input;
using Bloxle.Common.Levels;

using System;

namespace Bloxle.AIGeneration.Input
{
    class AIGenerationInput : IGameInput
    {
        private Level _tileGrid;
        private int[,] _inputMask;

        public AIGenerationInput(Level tileGrid) {
            _tileGrid = tileGrid;
            _inputMask = InputMask.UndoMask;
            
        }

        public ICommand GetInputCommand()
        {
            Random r = new Random();

            var x = r.Next(_tileGrid._width);
            var y = r.Next(_tileGrid._height);

            Vector2 tilePosition = new Vector2(x,y);

            ICommand command = new CycleCommand(_tileGrid, tilePosition, _inputMask);

            return command;
        }
    }
}