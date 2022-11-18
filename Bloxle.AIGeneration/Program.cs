using Bloxle.AIGeneration.AIGeneration;

namespace Bloxle.AIGeneration
{
    public static class Program
    {
        static void Main()
        {
            const int NUMBER_OF_LEVELS = 100;

            var engine = new AIGenerationEngine();
            engine.Generate(NUMBER_OF_LEVELS);
        }
    }
}
