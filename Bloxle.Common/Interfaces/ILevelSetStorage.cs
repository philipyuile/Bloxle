using Bloxle.Common.Levels;

namespace Bloxle.Common.Interfaces
{
    public interface ILevelSetStorage
    {
        Level LoadGameFile(int levelNumber);
        void SaveGameFile(int levelNumber, Level tileGrid);
    }
}
