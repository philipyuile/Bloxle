using Microsoft.Xna.Framework;
using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;

using Bloxle.Game.Menu;

namespace Bloxle.Game.Commands
{
    class MenuPageIncrementCommand : ICommand
    {
        private MenuGrid _tileGrid;

        public MenuPageIncrementCommand(MenuGrid tileGrid)
        {
            _tileGrid = tileGrid;
        }

        public bool Execute()
        {
            _tileGrid.IncreaseCurrentPageNumber();
            return true;
        }
    }
}