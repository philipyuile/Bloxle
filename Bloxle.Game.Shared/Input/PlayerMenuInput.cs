using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;
using Bloxle.Game.Shared.Commands;
using Bloxle.Game.Shared.Menu;
using Microsoft.Xna.Framework.Input.Touch;

namespace Bloxle.Game.Shared.Input
{
    class PlayerMenuInput : IGameInput
    {
        private MouseState _lastMouseState;
        private MenuGrid _menuGrid;
        private Vector2 _gridOrigin;
        private GameProgress _gameProgress;

        public PlayerMenuInput(MenuGrid menuGrid, Vector2 gridOrigin, GameProgress gameProgress) {
            _lastMouseState = Mouse.GetState();
            _menuGrid = menuGrid;
            _gridOrigin = gridOrigin;
            _gameProgress = gameProgress;
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
                command = ProcessCommands(command, mousePosition);
            }

            var touchState = TouchPanel.GetState();

            foreach (var touch in touchState)
            {
                if (touch.State == TouchLocationState.Released)
                {
                    command = ProcessCommands(command, touch.Position);
                }
            }

            _lastMouseState = mouseState;

            return command;
        }

        private ICommand ProcessCommands(ICommand command, Vector2 screenPosition)
        {
            var tilePosition = ConvertToTilePosition(screenPosition);
            if (tilePosition.X >= 0 && tilePosition.X < _menuGrid.GetWidth() && tilePosition.Y >= 0 && tilePosition.Y < _menuGrid.GetHeight())
            {
                command = new MenuSelectionCommand(_menuGrid, tilePosition, _gameProgress);
            }
            else if (tilePosition.X == 5 && tilePosition.Y == 3)
            {
                command = new MenuPageDecrementCommand(_menuGrid);
            }
            else if (tilePosition.X == 6 && tilePosition.Y == 3)
            {
                command = new MenuPageIncrementCommand(_menuGrid);
            }

            return command;
        }

        private Vector2 ConvertToTilePosition(Vector2 position)
        {
            var tilePosition = new Vector2((int)((position.X - _gridOrigin.X) / 100), (int)((position.Y - _gridOrigin.Y) / 100));
            return tilePosition;
        }
    }
}