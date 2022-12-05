using System;

using Microsoft.Xna.Framework;

using Serilog;

using Bloxle.Game.Shared.Display;
using Bloxle.Game.Shared.Game;

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

                string levelFolder = "Content/Levels/";
                string progressFolder = "Content/Progress/";

                var displayParams = new DisplayParameters {
                    GameOrigin = new Vector2(280, 40),
                    MenuOrigin = new Vector2(100, 70),
                    ScreenWidth = 800,
                    ScreenHeight = 600,
                    GameTileSize = 48,
                    MenuTileSize = 80,
                    MenuTileMargin = 20,
                    ArrowTileSize = 32,
                    TileScale = 1.0
        };

                using (var menu = new GameScreen(NUMBER_OF_LEVELS_SHOWN, levelFolder, progressFolder, displayParams))
                {
                    menu.Run();
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
                Log.Logger.Error(e.StackTrace);

                throw;
            }
        }
    }
}
