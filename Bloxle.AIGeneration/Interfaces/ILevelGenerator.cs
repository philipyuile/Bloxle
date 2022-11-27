using Bloxle.Common.Levels;

namespace Bloxle.AIGeneration.Interfaces
{
    interface ILevelGenerator
    {
        Level Level { get; }
        void InitialiseLevel();
        Level GenerateLevel();
        double CalculateDifficultyIndex();
    }
}
