using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace topdownShooter {
    static class DrawFunctions {
        public static void DrawLine(Vector2 p0, Vector2 p1, Color color = default(Color)) {
            Line2D line = new Line2D(p0, p1, color);
            line.Draw();
        }

        public static void DrawLine(float x0, float y0, float x1, float y1, Color color = default(Color)) {
            Line2D line = new Line2D(new Vector2(x0, y0), new Vector2(x1, y1), color);
            line.Draw();
        }

        public static void DrawRectangle(Vector2 p0, Vector2 p1, bool filled, Color color = default(Color)) {
            Rectangle2D rect = new Rectangle2D(p0, p1, filled, color);
            rect.Draw();
        }

        public static void DrawRectangle(float x0, float y0, float x1, float y1, bool filled, Color color = default(Color)) {
            Rectangle2D rect = new Rectangle2D(new Vector2(x0, y0), new Vector2(x1, y1), filled, color);
            rect.Draw();
        }

        public static void DrawCircle(Vector2 p, float radius, bool filled, Color color = default(Color), int segments = 32) {
            Circle2D circle = new Circle2D(p, radius, filled, color, segments);
            circle.Draw();
        }

        public static void DrawCircle(float x, float y, float radius, bool filled, Color color = default(Color), int segments = 32) {
            Circle2D circle = new Circle2D(new Vector2(x, y), radius, filled, color, segments);
            circle.Draw();
        }

        public static void DrawRing(Vector2 p, float innerRadius, float outerRadius, Color color = default(Color), int segments = 32) {
            Ring2D ring = new Ring2D(p, innerRadius, outerRadius, color, segments);
            ring.Draw();
        }

        public static void DrawRing(float x, float y, float innerRadius, float outerRadius, Color color = default(Color), int segments = 32) {
            Ring2D ring = new Ring2D(new Vector2(x, y), innerRadius, outerRadius, color, segments);
            ring.Draw();
        }

        public static void DrawTriangle(Vector2 p0, Vector2 p1, Vector2 p2, bool filled, Color color = default(Color)) {
            Triangle2D tri = new Triangle2D(p0, p1, p2, filled, color);
            tri.Draw();
        }

        public static void DrawTriangle(float x0, float y0, float x1, float y1, float x2, float y2, bool filled, Color color = default(Color)) {
            Triangle2D tri = new Triangle2D(new Vector2(x0, y0), new Vector2(x1, y1), new Vector2(x2, y2), filled, color);
            tri.Draw();
        }

        public static void DrawPrimitive(PrimitiveType primitiveType, VertexPositionColor[] vertices) {
            Primitive2D prim = new Primitive2D(primitiveType, vertices);
            prim.Draw();
        }
    }
}