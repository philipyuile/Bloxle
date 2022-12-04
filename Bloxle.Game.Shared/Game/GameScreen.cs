using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using Bloxle.Common.Commands;
using Bloxle.Common.Interfaces;
using Bloxle.Common.Storage;
using Bloxle.Common.Enums;
using Bloxle.Common.Levels;

using Bloxle.Game.Shared.Commands;
using Bloxle.Game.Shared.Input;
using Bloxle.Game.Shared.Enums;
using Bloxle.Game.Shared.Storage;
using Bloxle.Game.Shared.Menu;
using System;

namespace Bloxle.Game.Shared.Game
{
    public class GameScreen : Microsoft.Xna.Framework.Game
    {
        public string _levelFolder = "Content/Levels/";
        public string _progressFolder = "Content/Progress/";

        Texture2D _tilesTexture;
        Texture2D _arrowsTexture;
        SpriteFont _headerFont;
        SpriteFont _textFont;
        static Vector2 _gridOrigin;
        IDictionary<TileColour, Rectangle> _tilesTextureDictionary;
        IDictionary<ArrowDirection, Rectangle> _arrowsTextureDictionary;
        Texture2D _overlayRectangle;
        Texture2D _menuTileRectangle;
        Texture2D _onePixel;

        GameProgress _gameProgress;
        GameProgressStorage _gameProgressStorage;

        IGameInput _menuInput;
        IGameInput _gameInput;
        IGameInput _endGameInput;

        IGameStorage _gameStorage;
        Level _levelGrid;

        MenuCollectionStorage _menuStorage;
        MenuGrid _menuGrid;

        int _playerScore = 0;
        int _targetScore = 0;

        int _numberOfLevels;
        const int _menuGridWidth = 5;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public GameStatus Status { get; set; }

        public int SelectedLevel { get { return _menuGrid.SelectedLevel; } }

        public GameScreen(int numberofLevels)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _numberOfLevels = numberofLevels;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            _gameProgressStorage = new GameProgressStorage(_progressFolder);
            _gameProgress = _gameProgressStorage.LoadGameProgressFile();

            Status = GameStatus.Menu;

            InitialiseMenu();

            base.Initialize();
        }

        protected void InitialiseGame()
        {
            _playerScore = 0;
            _gridOrigin = new Vector2(280, 40);

            _gameStorage = new GenerationFileStorage(_levelFolder, SelectedLevel);

            _levelGrid = _gameStorage.LoadGameFile();
            _targetScore = _levelGrid.TargetScore;

            _gameInput = new PlayerGameInput(_levelGrid, _gridOrigin);
        }

        protected void InitialiseMenu()
        {
            _gridOrigin = new Vector2(100, 70);

            _menuStorage = new MenuCollectionStorage(_levelFolder, _numberOfLevels);
            MenuTile[] menuItems = _menuStorage.LoadMenuCollection();
            _menuGrid = new MenuGrid(_menuGridWidth, _numberOfLevels);
            _menuGrid.PopulateMenuItems(menuItems);
            _menuGrid.SelectedLevel = 0;
            _menuGrid.CurrentPageNumber = _menuGrid.PageOfLevel(_gameProgress.GetMinimumIncompleteLevel());

            _menuInput = new PlayerMenuInput(_menuGrid, _gridOrigin, _gameProgress);
            _endGameInput = new PlayerEndGameInput();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tilesTexture = Content.Load<Texture2D>("Tiles/BasicTiles");
            _tilesTextureDictionary = new Dictionary<TileColour, Rectangle>();
            _tilesTextureDictionary.Add(TileColour.Red, new Rectangle(0, 0, 48, 48));
            _tilesTextureDictionary.Add(TileColour.Yellow, new Rectangle(48, 0, 48, 48));
            _tilesTextureDictionary.Add(TileColour.Green, new Rectangle(96, 0, 48, 48));
            _tilesTextureDictionary.Add(TileColour.Blue, new Rectangle(144, 0, 48, 48));

            _arrowsTexture = Content.Load<Texture2D>("Arrows/arrows");
            _arrowsTextureDictionary = new Dictionary<ArrowDirection, Rectangle>();
            _arrowsTextureDictionary.Add(ArrowDirection.Up, new Rectangle(32, 0, 32, 32));
            _arrowsTextureDictionary.Add(ArrowDirection.Left, new Rectangle(0, 32, 32, 32));
            _arrowsTextureDictionary.Add(ArrowDirection.Down, new Rectangle(32, 32, 32, 32));
            _arrowsTextureDictionary.Add(ArrowDirection.Right, new Rectangle(64, 32, 32, 32));

            _onePixel = Content.Load<Texture2D>("1Pixel");

            _headerFont = Content.Load<SpriteFont>("Fonts/Standard");
            _textFont = Content.Load<SpriteFont>("Fonts/Small");

            _overlayRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _overlayRectangle.SetData(new[] { new Color (100,100,100,100) });

            _menuTileRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _menuTileRectangle.SetData(new[] { new Color(100, 100, 100, 100) });

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            _spriteBatch.Dispose();
            _overlayRectangle.Dispose();
            _menuTileRectangle.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            ICommand inputCommand = null;

            if (Status == GameStatus.InGame)
            {
                inputCommand = _gameInput.GetInputCommand();

                if (inputCommand != null)
                {
                    if (inputCommand is ExitCommand)
                    {
                        Status = GameStatus.Menu;
                        InitialiseMenu();
                        return;
                    }


                    if (inputCommand is MoveCommand && inputCommand.Execute())
                    {
                        _playerScore++;
                    }
                    else if (inputCommand is UndoCommand && inputCommand.Execute())
                    {
                        _playerScore--;
                    }
                }

                if (_levelGrid.AllTilesAreThisColour(TileColour.Green))
                {
                    Status = GameStatus.Success;
                    _gameProgress.LevelsCompleted.Add(new CompletedLevel { LevelNumber = SelectedLevel, Moves = _playerScore });
                    _gameProgressStorage.SaveGameProgressFile(_gameProgress);
                }
                else if (_playerScore >= _targetScore)
                {
                    Status = GameStatus.Failure;
                }
            }
            else if (Status == GameStatus.Menu)
            {
                inputCommand = _menuInput.GetInputCommand();

                if (inputCommand is ExitCommand)
                {
                    Exit();
                }

                if (inputCommand != null)
                {
                    inputCommand.Execute();
                }

                if (SelectedLevel != 0)
                {
                    Status = GameStatus.InGame;
                    InitialiseGame();
                }
            }
            else if (Status == GameStatus.Failure || Status == GameStatus.Success)
            {
                inputCommand = _endGameInput.GetInputCommand();

                if (inputCommand != null)
                {
                    Status = GameStatus.Menu;
                    InitialiseMenu();
                }
            }

            base.Update(gameTime);
        }

        private void DrawTile(TileColour colour, Vector2 position)
        {
            var drawablePosition = ConvertToDrawableTilePosition(position);
            _spriteBatch.Draw(
                _tilesTexture,
                drawablePosition,
                _tilesTextureDictionary[colour],
                Color.White,
                0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0f
            );
        }
        private void DrawArrow(ArrowDirection direction, Vector2 position)
        {
            var drawablePosition = ConvertToDrawableArrowPosition(position);
            _spriteBatch.Draw(
                _arrowsTexture,
                drawablePosition,
                _arrowsTextureDictionary[direction],
                Color.White,
                0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0f
            );
        }

        private void DrawMenuTile(int levelNumber, Vector2 position)
        {
            var drawablePosition = ConvertToDrawableMenuPosition(position);
            _spriteBatch.Draw(
             _menuTileRectangle,
             new Rectangle((int)drawablePosition.X, (int)drawablePosition.Y, 80, 80),
             new Color(20, 20, 20, 200)
            );

            if (levelNumber <= _gameProgress.GetMinimumIncompleteLevel())
            {
                _spriteBatch.DrawString(_headerFont, $"{levelNumber}", new Vector2(drawablePosition.X + (levelNumber > 9 ? 15 : 27), drawablePosition.Y + 15), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_headerFont, "X", new Vector2(drawablePosition.X + 27, drawablePosition.Y + 15), Color.White);
            }
        }

        private void DrawMenuPaginationTile(bool isIncrement, Vector2 position)
        {
            var drawablePosition = ConvertToDrawableMenuPosition(position);
            _spriteBatch.Draw(
             _menuTileRectangle,
             new Rectangle((int)drawablePosition.X, (int)drawablePosition.Y, 80, 80),
             new Color(20, 20, 20, 200)
            );

            if (isIncrement)
            {
                _spriteBatch.DrawString(_headerFont, $">", new Vector2(drawablePosition.X + 27, drawablePosition.Y + 15), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_headerFont, "<", new Vector2(drawablePosition.X + 27, drawablePosition.Y + 15), Color.White);
            }
        }

        private void DrawGhostTile(int moveNumber, Vector2 position)
        {
            var drawablePosition = ConvertToDrawableTilePosition(position);
            _spriteBatch.Draw(
             _menuTileRectangle,
             new Rectangle((int)drawablePosition.X, (int)drawablePosition.Y, 48, 48),
             new Color(20, 20, 20)
            );

            _spriteBatch.DrawString(_textFont, $"{moveNumber}", new Vector2(drawablePosition.X + 16, drawablePosition.Y + 12), Color.White);

        }

        private void DrawAccentedGhostTile(int moveNumber, Vector2 position)
        {
            var drawablePosition = ConvertToDrawableTilePosition(position);
            _spriteBatch.Draw(
             _menuTileRectangle,
             new Rectangle((int)drawablePosition.X, (int)drawablePosition.Y, 48, 48),
             new Color(255, 0, 0)
            );

            _spriteBatch.Draw(
             _menuTileRectangle,
             new Rectangle((int)drawablePosition.X + 1, (int)drawablePosition.Y + 1, 46, 46),
             new Color(20, 20, 20)
            );

            _spriteBatch.DrawString(_textFont, $"{moveNumber}", new Vector2(drawablePosition.X + 16, drawablePosition.Y + 12), Color.White);

        }

        public void DrawLine(Vector2 begin, Vector2 end, Color color, int width = 1)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            _spriteBatch.Draw(_onePixel, r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        private Vector2 ConvertToDrawableTilePosition(Vector2 position)
        {
            return new Vector2(_gridOrigin.X + position.X * 48, _gridOrigin.Y + position.Y * 48);
        }

        private Vector2 ConvertToDrawableArrowPosition(Vector2 position)
        {
            return new Vector2(_gridOrigin.X + 8 + position.X * 48, _gridOrigin.Y + 8 + position.Y * 48);
        }

        private Vector2 ConvertToDrawableMenuPosition(Vector2 position)
        {
            return new Vector2(_gridOrigin.X + position.X * 100, _gridOrigin.Y + position.Y * 100);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (Status == GameStatus.InGame || Status == GameStatus.Success || Status == GameStatus.Failure)
            {
                _spriteBatch.DrawString(_textFont, $"Moves: {_playerScore}", new Vector2(118, 10), Color.White);
                _spriteBatch.DrawString(_textFont, $"Target: {((int)(_targetScore))}", new Vector2(104, 35), Color.White);

                _spriteBatch.DrawString(_textFont, "Cycle:", new Vector2(10, 150), Color.White);

                DrawTile(TileColour.Red, new Vector2 { X = -4, Y = 1 });
                DrawArrow(ArrowDirection.Right, new Vector2 { X = -3, Y = 1 });
                DrawTile(TileColour.Yellow, new Vector2 { X = -2, Y = 1 });
                DrawArrow(ArrowDirection.Down, new Vector2 { X = -2, Y = 2 });
                DrawTile(TileColour.Green, new Vector2 { X = -2, Y = 3 });
                DrawArrow(ArrowDirection.Left, new Vector2 { X = -3, Y = 3 });
                DrawTile(TileColour.Blue, new Vector2 { X = -4, Y = 3 });
                DrawArrow(ArrowDirection.Up, new Vector2 { X = -4, Y = 2 });

                _spriteBatch.DrawString(_textFont, "Click\nEffect:", new Vector2(10, 275), Color.White);

                DrawGhostTile(1, new Vector2(-3, 5));
                DrawGhostTile(1, new Vector2(-4, 6));
                DrawAccentedGhostTile(2, new Vector2(-3, 6));
                DrawGhostTile(1, new Vector2(-2, 6));
                DrawGhostTile(1, new Vector2(-3, 7));

                _spriteBatch.DrawString(_textFont, "Click the grid and try to turn all tiles green", new Vector2(20, 440), Color.White);

                DrawLine(new Vector2(256, 20), new Vector2(256, 430), Color.White);

                foreach (var tile in _levelGrid.TileGrid)
                {
                    if (tile.IsActive)
                    {
                        DrawTile(tile.Colour, tile.Position);
                    }
                }

                _spriteBatch.DrawString(_textFont, "Undo", new Vector2(705, 90), Color.White);
                DrawTile(TileColour.Red, new Vector2 { X = 9, Y = 0 });
            }

            if (Status == GameStatus.Success || Status == GameStatus.Failure)
            {
                _spriteBatch.Draw(
                    _overlayRectangle,
                    new Rectangle(0, 100, 800, 300),
                    new Color(0, 0, 0, 200)
                );

                string message = Status == GameStatus.Success ? "You Win!" : "Sorry, you ran out of moves!";
                Vector2 messagePosition = Status == GameStatus.Success ? new Vector2(350, 200) : new Vector2(100, 200);

                _spriteBatch.DrawString(_headerFont, message, messagePosition, Color.White);
            }

            if (Status == GameStatus.Menu)
            {
                _spriteBatch.DrawString(_headerFont, $"Choose a level: ", new Vector2(100, 10), Color.White);

                foreach (var tile in _menuGrid.GetLevelsForCurrentPage())
                {
                    DrawMenuTile(tile.LevelNumber, tile.Position);
                }

                DrawMenuPaginationTile(false, new Vector2(5, 3));
                DrawMenuPaginationTile(true, new Vector2(6, 3));
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
