using Bloxle.Game.Shared.Menu;
using System.IO;

namespace Bloxle.Game.Shared.Storage
{
    public class MenuCollectionStorage
    {
        private int _numberOfLevels;
        private readonly string _levelFolder;

        public MenuCollectionStorage(string levelFolder, int numberOfLevels)
        {
            _levelFolder = levelFolder;
            _numberOfLevels = numberOfLevels;
        }

        public MenuTile[] LoadMenuCollection()
        {
            MenuTile[] menuItems = new MenuTile[_numberOfLevels];

            for (var currentLevel = 1; currentLevel <= _numberOfLevels; currentLevel++)
            {
                var currentFilePath = $"{_levelFolder}level{currentLevel}.json";
                if (File.Exists(currentFilePath)) {
                    menuItems[currentLevel - 1] = new MenuTile { LevelNumber = currentLevel };
                }
            }

            return menuItems;
        }
    }
}
