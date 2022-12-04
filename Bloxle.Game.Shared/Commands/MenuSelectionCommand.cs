using Microsoft.Xna.Framework;
using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;

using Bloxle.Game.Shared.Menu;

namespace Bloxle.Game.Shared.Commands
{
    class MenuSelectionCommand : ICommand
    {
        private readonly Vector2 _tilePosition;

        private MenuGrid _tileGrid;

        private GameProgress _gameProgress;

        public MenuSelectionCommand(MenuGrid tileGrid, Vector2 tilePosition, GameProgress gameProgress)
        {
            _tilePosition = tilePosition;
            _tileGrid = tileGrid;
            _gameProgress = gameProgress;
        }

        public bool Execute()
        {
            if (_gameProgress.GetMinimumIncompleteLevel() >= _tileGrid.GetLevelFromPosition(_tilePosition))
            {
                _tileGrid.SetSelectedLevelFromPosition(_tilePosition);
                return true;
            }

            return false;
        }
    }
}