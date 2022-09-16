using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace topdownShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;

        /*Texture2D targetSprite;
        Texture2D crosshairsSprite;
        Texture2D backgroundSprite;*/
        SpriteFont gameFont;

        Vector2 targetPosition = new Vector2(300, 300);
        const int targetRadius = 45;

        //MouseState mouseState;
        //bool mouseReleased;
        //int score = 0;

        //private Player player;
        World world;
        Basic2d cursor;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            //world = new World();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //player = new Player("sprPlayer", new Vector2(200, 200), new Vector2(16, 16));
            //Basic2d b = new Basic2d("ye", new Vector2(1, 0), new Vector2(2, 3));

            Globals.screenWidth = 800;
            Globals.screenHeight = 500;

            _graphics.PreferredBackBufferWidth = Globals.screenWidth;
            _graphics.PreferredBackBufferHeight = Globals.screenHeight;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.keyboard = new GameKeyboard();
            Globals.mouse = new GameMouse();

            /*targetSprite = Content.Load<Texture2D>("target");
            crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");*/
            gameFont = Content.Load<SpriteFont>("galleryFont");

            world = new World();
            cursor = new Basic2d("cursor", Vector2.Zero, new Vector2(13, 13));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //mouseState = Mouse.GetState();
            /*if (mouseState.LeftButton == ButtonState.Pressed && mouseReleased) {
                float mouseTargetDist = Vector2.Distance(targetPosition, mouseState.Position.ToVector2());

                if (mouseTargetDist <= targetRadius) {
                     ++score;
                    Random rand = new Random();
                    targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth);
                    targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight);
                }
               
                mouseReleased = false;
            }

            if (mouseState.LeftButton == ButtonState.Released) {
                mouseReleased = true;
            }*/

            //player.Update(gameTime);
            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();
            world.Update();
            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Globals.spriteBatch.Begin();
            //Globals.spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            //Globals.spriteBatch.Draw(targetSprite, targetPosition - new Vector2(targetRadius, targetRadius), Color.White);
            //Globals.spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(10, 10), Color.White);
            world.Draw(Vector2.Zero);
            cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(6, 6));
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
