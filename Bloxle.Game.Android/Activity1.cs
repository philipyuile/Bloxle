using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;

using System.IO;

using Bloxle.Game.Shared.Game;

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

            _game = new GameScreen(NUMBER_OF_LEVELS_SHOWN, levelFolder, progressFolder);
            _view = _game.Services.GetService(typeof(View)) as View;

            SetContentView(_view);
            _game.Run();
        }
    }
}
