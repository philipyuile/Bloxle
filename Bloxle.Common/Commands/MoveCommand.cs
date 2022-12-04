
using Bloxle.Common.Input;
using Bloxle.Common.Levels;
using System.Numerics;

namespace Bloxle.Common.Commands
{
    public class MoveCommand : CycleCommand
    {
        public MoveCommand(Level tileGrid, Vector2 tilePosition) : base (tileGrid, tilePosition, InputMask.PlayerMoveMask)
        {
        }
    }
}