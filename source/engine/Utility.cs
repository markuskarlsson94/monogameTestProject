using System;
using Microsoft.Xna.Framework;

namespace topdownShooter {
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
    }
}