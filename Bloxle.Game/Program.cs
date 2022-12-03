using System;
using Bloxle.Game.Game;
using Serilog;

namespace Bloxle
{
    public static class Program
    {
        const int NUMBER_OF_LEVELS_SHOWN = 100;

        static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        [STAThread]
        static void Main()
        {
            try
            {
                ConfigureLogger();

                using (var menu = new GameScreen(NUMBER_OF_LEVELS_SHOWN))
                {
                    menu.Run();
                }
            }
            catch (Exception e) {
                Log.Logger.Error(e.Message);
                Log.Logger.Error(e.StackTrace);

                throw;
            }
        }
    }
}
