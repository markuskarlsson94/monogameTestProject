using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace topdownShooter {
    static class DrawFunctions {
        public static void DrawLine(Vector2 p0, Vector2 p1, Color color = default(Color)) {
            if (object.Equals(color, default(Color))) color = Color.White;
            DrawLine(p0.X, p0.Y, p1.X, p1.Y, color);
        }

        public static void DrawLine(float x0, float y0, float x1, float y1, Color color = default(Color)) {
            if (object.Equals(color, default(Color))) color = Color.White;
            VertexPositionColor[] _vertexPositionColors;
            BasicEffect _basicEffect;

            _vertexPositionColors = new[] {
                new VertexPositionColor(new Vector3(x0, y0, 0), color),
                new VertexPositionColor(new Vector3(x1, y1, 0), color)
            };

            _basicEffect = new BasicEffect(Globals.graphicsDevice);
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.World = Matrix.CreateOrthographicOffCenter(0, Globals.graphicsDevice.Viewport.Width, Globals.graphicsDevice.Viewport.Height, 0, 0, 1);

            EffectTechnique effectTechnique = _basicEffect.Techniques[0];
            EffectPassCollection effectPassCollection = effectTechnique.Passes;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            Globals.graphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, _vertexPositionColors, 0, 1);
        }

        public static void DrawRectangle(float x0, float y0, float x1, float y1, bool filled, Color color = default(Color)) {
            if (object.Equals(color, default(Color))) color = Color.White;
            VertexPositionColor[] _vertexPositionColors;
            BasicEffect _basicEffect;

            if (filled) {
                _vertexPositionColors = new[] {
                    new VertexPositionColor(new Vector3(x0, y0, 0), color),
                    new VertexPositionColor(new Vector3(x1, y0, 0), color),
                    new VertexPositionColor(new Vector3(x0, y1, 0), color),
                    new VertexPositionColor(new Vector3(x1, y1, 0), color)
                };
            } else {
                _vertexPositionColors = new[] {
                    new VertexPositionColor(new Vector3(x0, y0, 0), color),
                    new VertexPositionColor(new Vector3(x1, y0, 0), color),
                    new VertexPositionColor(new Vector3(x1, y1, 0), color),
                    new VertexPositionColor(new Vector3(x0, y1, 0), color),
                    new VertexPositionColor(new Vector3(x0, y0, 0), color)
                };
            }

            _basicEffect = new BasicEffect(Globals.graphicsDevice);
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.World = Matrix.CreateOrthographicOffCenter(0, Globals.graphicsDevice.Viewport.Width, Globals.graphicsDevice.Viewport.Height, 0, 0, 1);

            EffectTechnique effectTechnique = _basicEffect.Techniques[0];
            EffectPassCollection effectPassCollection = effectTechnique.Passes;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            PrimitiveType primType = (filled ? PrimitiveType.TriangleStrip : PrimitiveType.LineStrip);
            Globals.graphicsDevice.DrawUserPrimitives(primType, _vertexPositionColors, 0, filled ? 2 : 4);
        }
    }
}