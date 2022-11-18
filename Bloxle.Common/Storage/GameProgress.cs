using System.Collections.Generic;
using System.Linq;

namespace Bloxle.Common.Storage
{
    public class GameProgress
    {
        public List<CompletedLevel> LevelsCompleted;

        public int GetMinimumIncompleteLevel()
        {
            int minimumIncompleteLevel = 1;

            if (LevelsCompleted.Any())
            {
                minimumIncompleteLevel = LevelsCompleted.Max(x => x.LevelNumber) + 1;
            }

            return minimumIncompleteLevel;
        }
    }
}
