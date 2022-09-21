using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace topdownShooter {
    public delegate void Call();
    public delegate void PassObject(object obj);
    public delegate object PassObjectAndReturn(object obj);

    class Globals {
        public static int screenHeight, screenWidth;

        public static ContentManager content;
        public static SpriteBatch spriteBatch;
        public static GameKeyboard keyboard;
        public static GameMouse mouse;
        public static GameTime gameTime;
        public static SpriteFont gameFont;

        public static float RotateTowards(Vector2 Pos, Vector2 focus) {
            float h, sineTheta, angle;

            if (Pos.Y-focus.Y != 0) {
                h = (float)Math.Sqrt(Math.Pow(Pos.X-focus.X, 2) + Math.Pow(Pos.Y-focus.Y, 2));
                sineTheta = (float)(Math.Abs(Pos.Y-focus.Y)/h); //* ((item.Pos.Y-focus.Y)/(Math.Abs(item.Pos.Y-focus.Y))));
            } else {
                h = Pos.X-focus.X;
                sineTheta = 0;
            }

            angle = (float)Math.Asin(sineTheta);

            if (Pos.X-focus.X > 0 && Pos.Y-focus.Y > 0) {
                angle = (float)(Math.PI*3/2 + angle);
            } else if (Pos.X-focus.X > 0 && Pos.Y-focus.Y < 0) {
                angle = (float)(Math.PI*3/2 - angle);
            } else if (Pos.X-focus.X < 0 && Pos.Y-focus.Y > 0) {
                angle = (float)(Math.PI/2 - angle);
            } else if (Pos.X-focus.X < 0 && Pos.Y-focus.Y < 0) {
                angle = (float)(Math.PI/2 + angle);
            } else if (Pos.X-focus.X > 0 && Pos.Y-focus.Y == 0) {
                angle = (float)Math.PI*3/2;
            } else if (Pos.X-focus.X < 0 && Pos.Y-focus.Y == 0) {
                angle = (float)Math.PI/2;
            } else if (Pos.X-focus.X == 0 && Pos.Y-focus.Y > 0) {
                angle = (float)0;
            } else if(Pos.X-focus.X == 0 && Pos.Y-focus.Y < 0) {
                angle = (float)Math.PI;
            }

            return angle;
        }

        public static float DegToRad(float degrees) {
            return (float)(Math.PI/180)*degrees;
        }
    }
}