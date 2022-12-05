using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;

using System.IO;

using Bloxle.Game.Shared.Display;
using Bloxle.Game.Shared.Game;
using Android.Content.Res;
using System.Linq;

namespace Bloxle.Game.Android
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        const int NUMBER_OF_LEVELS_SHOWN = 100;

        private GameScreen _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);



            string levelFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Content/Levels/");
            string progressFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Content/Progress/");

            if (!Directory.Exists(levelFolder))
            {
                Directory.CreateDirectory(levelFolder);

                string content;
                AssetManager assets = this.Assets;
                var levelAssetPaths = assets.List("").Where(x => x.StartsWith("level"));
                foreach (var levelAssetPath in levelAssetPaths)
                {
                    using (StreamReader sr = new StreamReader(assets.Open(levelAssetPath)))
                    {
                        var levelPath = Path.Combine(levelFolder, levelAssetPath);
                        content = sr.ReadToEnd();
                        File.WriteAllText(levelPath, content);
                    }
                }
            }

            var displayParams = new DisplayParameters
            {
                GameOrigin = new Vector2(560, 80),
                MenuOrigin = new Vector2(200, 140),
                ScreenWidth = 1600,
                ScreenHeight = 1200,
                GameTileSize = 48,
                MenuTileSize = 80,
                MenuTileMargin = 20,
                ArrowTileSize = 32,
                TileScale = 2.0
            };

            _game = new GameScreen(NUMBER_OF_LEVELS_SHOWN, levelFolder, progressFolder, displayParams);
            _view = _game.Services.GetService(typeof(View)) as View;

            SetContentView(_view);
            _game.Run();
        }
    }
}
