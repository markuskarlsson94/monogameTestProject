using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace topdownShooter {
    public class Rectangle2D : Shape2D {
        private Vector2 p0;
        private Vector2 p1;
        private bool filled;

        public Vector2 P0 {
            get { return p0; }

            set {
                p0 = value;
                Update();
            }
        }

        public Vector2 P1 {
            get { return p1; }

            set {
                p1 = value;
                Update();
            }
        }

        public bool Filled {
            get { return filled; }

            set {
                filled = value;
                Update();
            }
        }

        public Rectangle2D(Vector2 p0, Vector2 p1, bool filled, Color color = default(Color)) : base(color) {
            this.p0 = p0;
            this.p1 = p1;
            this.filled = filled;
            Update();
        }

        public Rectangle2D(float x0, float y0, float x1, float y1, bool filled, Color color = default(Color)) : base(color) {
            p0 = new Vector2(x0, y0);
            p1 = new Vector2(x1, y1);
            this.filled = filled;
            Update();
        }

        override protected void Update() {
            if (filled) {
                primitiveType = PrimitiveType.TriangleStrip;
                primitiveCount = 2;

                bool inverted = (p0.X < p1.X && p0.Y > p1.Y) ||
                                 p0.X > p1.X && p0.Y < p1.Y;

                if (inverted) {
                    vertices = new[] {
                        new VertexPositionColor(new Vector3(p0, 0), color),
                        new VertexPositionColor(new Vector3(p0.X, p1.Y, 0), color),
                        new VertexPositionColor(new Vector3(p1.X, p0.Y, 0), color),
                        new VertexPositionColor(new Vector3(p1, 0), color)
                    };
                } else {
                    vertices = new[] {
                        new VertexPositionColor(new Vector3(p0, 0), color),
                        new VertexPositionColor(new Vector3(p1.X, p0.Y, 0), color),
                        new VertexPositionColor(new Vector3(p0.X, p1.Y, 0), color),
                        new VertexPositionColor(new Vector3(p1, 0), color)
                    };
                }
                
            } else {
                primitiveType = PrimitiveType.LineStrip;
                primitiveCount = 4;

                vertices = new[] {
                    new VertexPositionColor(new Vector3(p0, 0), color),
                    new VertexPositionColor(new Vector3(p1.X, p0.Y, 0), color),
                    new VertexPositionColor(new Vector3(p1, 0), color),
                    new VertexPositionColor(new Vector3(p0.X, p1.Y, 0), color),
                    new VertexPositionColor(new Vector3(p0, 0), color)
                };
            }
        }
    }
}