using Microsoft.Xna.Framework;

namespace Bloxle.Game.Shared.Display
{
    public class DisplayParameters
    {

        public Vector2 GameOrigin { get; set; }

        public Vector2 MenuOrigin { get; set; }

        public int ScreenWidth { get; set; }

        public int ScreenHeight { get; set; }

        public int GameTileSize { get; set; }

        public int MenuTileSize { get; set; }

        public int MenuTileMargin { get; set; }

        public int ArrowTileSize { get; set; }

        public double TileScale { get; set; }

    }
}
