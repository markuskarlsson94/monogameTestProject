using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace topdownShooter
{
    public class TopdownShootherGame : Game
    {
        private GraphicsDeviceManager _graphics;
        World world;
        Basic2d cursor;

        public TopdownShootherGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize() {
            Globals.screenWidth = 800;
            Globals.screenHeight = 500;

            _graphics.PreferredBackBufferWidth = Globals.screenWidth;
            _graphics.PreferredBackBufferHeight = Globals.screenHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent() {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.keyboard = new GameKeyboard();
            Globals.mouse = new GameMouse();
            Globals.gameFont = Content.Load<SpriteFont>("galleryFont");
            Globals.graphicsDevice = GraphicsDevice;

            world = new World();
            cursor = new Basic2d("cursor", Vector2.Zero);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            if (Globals.keyboard.GetPressed("R")) {
                world.Reset();
                world.Init();
            }

            world.Update();
            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            Globals.spriteBatch.Begin();
            world.Draw(Vector2.Zero);
            Globals.spriteBatch.End();

            Globals.spriteBatch.Begin();
            world.Draw2();
            Globals.spriteBatch.End();

            Globals.spriteBatch.Begin();
            world.Draw3();
            cursor.Draw(Globals.mouse.newMousePos, new Vector2(6, 6));
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
