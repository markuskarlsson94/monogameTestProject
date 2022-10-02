using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace topdownShooter {
    enum FontAlignment {
        topLeft,
        topCenter,
        topRight,
        middleLeft,
        middleCenter,
        middleRight,
        bottomLeft,
        bottomCenter,
        bottomRight
    };

    static class Utility {
        public static void Print(string str) {
            System.Console.WriteLine(str);
        }

        public static double RandomRange(double min, double max) {
            Random rnd = new Random();
            return rnd.NextDouble()*(max - min) + min;
        }

        public static Vector2 Vector2Rotated(Vector2 vec, float degrees) {
            var rot = Matrix.CreateRotationZ(Globals.DegToRad(degrees));
            return Vector2.Transform(vec, rot);
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 pos, string text, SpriteFont font, FontAlignment alignment = FontAlignment.topLeft, Color color = default(Color)) {
            if (color == default(Color)) color = Color.White;
            Vector2 size = font.MeasureString(text);
            Vector2 p = pos;

            switch (alignment) {
                case FontAlignment.topLeft: { p = pos; break; }
                case FontAlignment.topCenter: { p = new Vector2(pos.X - size.X/2, pos.Y); break; }
                case FontAlignment.topRight: { p = new Vector2(pos.X - size.X, pos.Y); break; }
                case FontAlignment.middleLeft: { p = new Vector2(pos.X, pos.Y - size.Y/2); break; }
                case FontAlignment.middleCenter: { p = new Vector2(pos.X - size.X/2, pos.Y - size.Y/2); break; }
                case FontAlignment.middleRight: { p = new Vector2(pos.X - size.X, pos.Y - size.Y/2); break; }
                case FontAlignment.bottomLeft: { p = new Vector2(pos.X, pos.Y - size.Y); break; }
                case FontAlignment.bottomCenter: { p = new Vector2(pos.X - size.X/2, pos.Y - size.Y); break; }
                case FontAlignment.bottomRight: { p = new Vector2(pos.X - size.X, pos.Y - size.Y); break; }
            }

            spriteBatch.DrawString(font, text, p, color);
        }
    }
}