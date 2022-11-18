using Microsoft.Xna.Framework;
using Bloxle.Common.Interfaces;
using Bloxle.Common.Levels;

namespace Bloxle.Common.Commands
{
    public class CycleCommand : ICommand
    {
        public readonly Vector2 TilePosition;

        protected Level _tileGrid;

        protected int[,] _inputMask;

        public CycleCommand(Level tileGrid, Vector2 tilePosition, int[,] inputMask)
        {
            TilePosition = tilePosition;
            _tileGrid = tileGrid;
            _inputMask = inputMask;
        }

        public bool Execute()
        {
            return _tileGrid.CycleTilesFromPosition(TilePosition, _inputMask);
        }
    }
}