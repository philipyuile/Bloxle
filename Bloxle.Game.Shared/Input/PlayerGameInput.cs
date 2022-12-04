using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Bloxle.Common.Interfaces;
using Bloxle.Common.Commands;
using Bloxle.Common.Levels;
using Bloxle.Game.Shared.Commands;
using System.Collections.Generic;
using System.Linq;

namespace Bloxle.Game.Shared.Input
{
    class PlayerGameInput : IGameInput
    {
        private MouseState _lastMouseState;
        private Level _tileGrid;
        private Vector2 _gridOrigin;
        private Stack<Vector2> _moveStack;

        public PlayerGameInput(Level tileGrid, Vector2 gridOrigin) {
            _lastMouseState = Mouse.GetState();
            _tileGrid = tileGrid;
            _gridOrigin = gridOrigin;
            _moveStack = new Stack<Vector2>();
            
        }

        public ICommand GetInputCommand()
        {
            ICommand command = null;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                command = new ExitCommand();
            }

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Released && _lastMouseState.LeftButton == ButtonState.Pressed)
            {
                var mousePosition = new Vector2(mouseState.X, mouseState.Y);
                var tilePosition = ConvertToTilePosition(mousePosition).ToNumerics();

                if (_tileGrid.IsWithinBounds(tilePosition))
                {
                    _moveStack.Push(tilePosition);
                    command = new MoveCommand(_tileGrid, tilePosition);
                }
                else if (tilePosition.X == 9 && tilePosition.Y == 0)
                {
                    if (_moveStack.Any()) {
                        Vector2 lastMovePosition = _moveStack.Pop();
                        command = new UndoCommand(_tileGrid, lastMovePosition.ToNumerics());
                    }
                }
            }

            _lastMouseState = mouseState;

            return command;
        }

        private Vector2 ConvertToTilePosition(Vector2 position)
        {
            var tilePosition = new Vector2((int)((position.X - _gridOrigin.X) / 48), (int)((position.Y - _gridOrigin.Y) / 48));
            return tilePosition;
        }
    }
}