using Bloxle.AIGeneration.AIGeneration;

namespace Bloxle.AIGeneration
{
    public static class Program
    {
        static void Main()
        {
            const int NUMBER_OF_LEVELS = 50;

            var engine = new RandomDifficultyIndexGenerationEngine();
            engine.Generate(NUMBER_OF_LEVELS);
        }
    }
}
