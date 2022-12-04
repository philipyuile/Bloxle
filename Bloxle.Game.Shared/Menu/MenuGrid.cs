using Microsoft.Xna.Framework;
using System.Linq;

namespace Bloxle.Game.Shared.Menu
{
    public class MenuGrid
    {
        private MenuTile[] _levels;

        private int _width;
        private int _height;
        private int _numberOfLevels;
        private const int _maxNumberOfLevelsPerPage = 20;

        public int SelectedLevel { get; set; }
        public int CurrentPageNumber { get; set; }


        public MenuGrid(int width, int numberOfLevels)
        {
            int levelsPerPage = _maxNumberOfLevelsPerPage > numberOfLevels ? _maxNumberOfLevelsPerPage : numberOfLevels;

            _width = width;
            _height = levelsPerPage % _width == 0 ? levelsPerPage / _width : levelsPerPage / _width + 1;
            _numberOfLevels = numberOfLevels;
            _levels = new MenuTile[_numberOfLevels];
            CurrentPageNumber = 0;
        }

        public void PopulateMenuItems(MenuTile[] items)
        {
            for (var i = 0; i < items.Length; i++)
            {
                int pageNumber = i / _maxNumberOfLevelsPerPage;
                int numberOnPage = i % _maxNumberOfLevelsPerPage;
                var xpos = numberOnPage % _width;
                var ypos = numberOnPage / _width;
                items[i].Position = new Vector2(xpos, ypos);
                items[i].Page = pageNumber;
            }
            _levels = items;
        }

        public int GetLevelFromPosition(Vector2 position)
        {
            var level = _levels.FirstOrDefault(l => l.Position.X == position.X && l.Position.Y == position.Y && l.Page == CurrentPageNumber);
            if (level == null)
            {
                return 0;
            }
            return level.LevelNumber;
        }

        public void SetSelectedLevelFromPosition(Vector2 position)
        {
            var level = _levels.FirstOrDefault(l => l.Position.X == position.X && l.Position.Y == position.Y && l.Page == CurrentPageNumber);
            if (level == null)
            {
                SelectedLevel = 0;
                return;
            }

            SelectedLevel = level.LevelNumber;
        }

        public int GetHeight()
        {
            return _height;
        }

        public int GetWidth()
        {
            return _width;
        }

        public MenuTile[] GetLevelsForCurrentPage()
        {
            return _levels.Where(x => x.Page == CurrentPageNumber).ToArray();
        }

        public int GetCurrentPageNumber()
        {
            return CurrentPageNumber;
        }

        public void IncreaseCurrentPageNumber()
        {
            int potentialNewPageNumber = CurrentPageNumber + 1;
            if (_levels.Any(x => x.Page == potentialNewPageNumber))
            {
                CurrentPageNumber = potentialNewPageNumber;
            }
        }

        public void DecreaseCurrentPageNumber()
        {
            if (CurrentPageNumber > 0)
            {
                CurrentPageNumber--;
            }
        }

        public int PageOfLevel(int level)
        {
            if (!_levels.Any(l => l.LevelNumber == level))
            {
                return _levels.Select(x => x.Page).Max();
            }

            return _levels.First(l => l.LevelNumber == level).Page;
        }
    }
}
