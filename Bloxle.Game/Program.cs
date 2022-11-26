using System;
using Bloxle.Game.Game;
using Bloxle.Game.Menu;

namespace Bloxle
{
    public static class Program
    {
        const int NUMBER_OF_LEVELS_SHOWN = 50;

        [STAThread]
        static void Main()
        {
            using (var menu = new GameScreen(NUMBER_OF_LEVELS_SHOWN))
            {
                menu.Run();
                   
            }
        }
    }
}
