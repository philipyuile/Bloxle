using System;

namespace Bloxle.AIGeneration.LevelGenerators
{
    public class RingLevelGenerator : SquareLevelGenerator
    {

        protected override void InitialiseBlankGrid()
        {
            Level.InitBlankRectangle();

            foreach (var tile in Level.TileGrid)
            { 
                if (_width % 2 == 1
                    && tile.Position.X == _width / 2 && tile.Position.Y == _width / 2)
                {
                    tile.IsActive = false;
                }
                else if (_width % 2 == 0
                    && (tile.Position.X == _width / 2 || tile.Position.X == _width / 2 - 1)
                    && (tile.Position.Y == _width / 2 || tile.Position.Y == _width / 2 - 1))
                {
                    tile.IsActive = false;
                }
            }
        }

        public override double CalculateDifficultyIndex()
        {
            return (2 * _width - 1) * _level.TargetScore;
        }
    }
}
