
using Bloxle.Common.Input;
using Bloxle.Common.Levels;
using System.Numerics;

namespace Bloxle.Common.Commands
{
    public class UndoCommand : CycleCommand
    {
        public UndoCommand(Level tileGrid, Vector2 tilePosition) : base (tileGrid, tilePosition, InputMask.UndoMask)
        {
        }
    }
}