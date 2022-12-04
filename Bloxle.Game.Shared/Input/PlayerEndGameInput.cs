using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Bloxle.Common.Interfaces;
using Bloxle.Game.Shared.Commands;
using Microsoft.Xna.Framework.Input.Touch;

namespace Bloxle.Game.Shared.Input
{
    class PlayerEndGameInput : IGameInput
    {
        private MouseState _lastMouseState;

        public PlayerEndGameInput() {
            _lastMouseState = Mouse.GetState();            
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
                command = new ExitCommand();
            }


            var touchState = TouchPanel.GetState();

            foreach (var touch in touchState)
            {
                if (touch.State == TouchLocationState.Released)
                {
                    command = new ExitCommand();
                }
            }

            _lastMouseState = mouseState;

            return command;
        }
    }
}