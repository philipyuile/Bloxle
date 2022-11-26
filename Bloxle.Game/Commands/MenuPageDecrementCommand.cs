using Microsoft.Xna.Framework;
using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;

using Bloxle.Game.Menu;

namespace Bloxle.Game.Commands
{
    class MenuPageDecrementCommand : ICommand
    {
        private MenuGrid _tileGrid;

        public MenuPageDecrementCommand(MenuGrid tileGrid)
        {
            _tileGrid = tileGrid;
        }

        public bool Execute()
        {
            _tileGrid.DecreaseCurrentPageNumber();
            return true;
        }
    }
}