using Microsoft.Xna.Framework;
using System.Linq;

namespace Bloxle.Game.Menu
{
    public class MenuGrid
    {
        private MenuTile[] _levels;

        private int _width;
        private int _height;
        private int _numberOfLevels;

        public int SelectedLevel { get; set; }

        public MenuGrid(int width, int numberOfLevels)
        {
            _width = width;
            _height = numberOfLevels % _width == 0 ? numberOfLevels / _width : numberOfLevels / _width + 1;
            _numberOfLevels = numberOfLevels;
            _levels = new MenuTile[_numberOfLevels];
        }

        public void PopulateMenuItems(MenuTile[] items)
        {
            for (var i = 0; i < items.Length; i++)
            {
                var xpos = i % _width;
                var ypos = i / _width;
                items[i].Position = new Vector2(xpos, ypos);
            }
            _levels = items;
        }

        public int GetLevelFromPosition(Vector2 position)
        {
            return _levels.First(l => l.Position.X == position.X && l.Position.Y == position.Y).LevelNumber;
        }

        public void SetSelectedLevelFromPosition(Vector2 position)
        {
            SelectedLevel = _levels.First(l => l.Position.X == position.X && l.Position.Y == position.Y).LevelNumber;
        }

        public int GetHeight()
        {
            return _height;
        }

        public int GetWidth()
        {
            return _width;
        }

        public MenuTile[] GetLevels()
        {
            return _levels;
        }
    }
}
