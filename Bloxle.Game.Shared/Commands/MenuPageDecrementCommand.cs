using Bloxle.Common.Interfaces;

using Bloxle.Game.Shared.Menu;

namespace Bloxle.Game.Shared.Commands
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