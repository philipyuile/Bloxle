using Bloxle.Common.Interfaces;

using Bloxle.Game.Shared.Menu;

namespace Bloxle.Game.Shared.Commands
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