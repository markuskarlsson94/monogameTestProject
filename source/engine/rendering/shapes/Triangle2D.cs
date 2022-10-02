using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace topdownShooter {
    public class Triangle2D : Shape2D {
        private Vector2 p0, p1, p2;
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

        public Vector2 P2 {
            get { return p2; }

            set {
                p2 = value;
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

        public Triangle2D(Vector2 p0, Vector2 p1, Vector2 p2, bool filled, Color color = default(Color)) : base(color) {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.filled = filled;
            Update();
        }

        protected override void Update() {
            if (filled) {
                primitiveType = PrimitiveType.TriangleStrip;
                primitiveCount = 1;

                if (isClockwise()) {
                    vertices = new VertexPositionColor[] {
                        new VertexPositionColor(new Vector3(p2, 0), color),
                        new VertexPositionColor(new Vector3(p1, 0), color),
                        new VertexPositionColor(new Vector3(p0, 0), color)
                    };
                } else {
                    vertices = new VertexPositionColor[] {
                        new VertexPositionColor(new Vector3(p0, 0), color),
                        new VertexPositionColor(new Vector3(p1, 0), color),
                        new VertexPositionColor(new Vector3(p2, 0), color)
                    };
                }
            } else {
                primitiveType = PrimitiveType.LineStrip;
                primitiveCount = 3;

                vertices = new VertexPositionColor[] {
                    new VertexPositionColor(new Vector3(p0, 0), color),
                    new VertexPositionColor(new Vector3(p1, 0), color),
                    new VertexPositionColor(new Vector3(p2, 0), color),
                    new VertexPositionColor(new Vector3(p0, 0), color)
                };
            }
        }

        private bool isClockwise() {
            return ((p1.Y - p0.Y) * (p2.X - p1.X) - (p1.X - p0.X) * (p2.Y - p1.Y)) > 0;
        }
    }
}