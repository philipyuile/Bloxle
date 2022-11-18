using Bloxle.Common.Levels;

namespace Bloxle.Common.Interfaces
{
    public interface IGameStorage
    {
        Level LoadGameFile();
        void SaveGameFile(Level tileGrid);
    }
}
