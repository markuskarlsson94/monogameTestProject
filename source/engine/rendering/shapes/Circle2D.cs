using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace topdownShooter {
    public class Circle2D : Shape2D {
        private Vector2 p;
        private float radius;
        private bool filled;
        private int segments;

        public Vector2 P {
            get { return p; }

            set {
                p = value;
                Update();
            }
        }

        public float Radius {
            get { return radius; }

            set {
                radius = value;
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

        public int Segments {
            get { return segments; }

            set {
                segments = ValidSegmentCount(value);
                Update();
            }
        }

        public Circle2D(Vector2 p, float radius, bool filled, Color color = default(Color), int segments = 32) : base(color) {
            this.p = p;
            this.radius = radius;
            this.filled = filled;
            this.segments = ValidSegmentCount(segments);
            Update();
        }

        private int ValidSegmentCount(int segments) {
            return Math.Max(segments, 3);
        }

        protected override void Update() {
            if (filled) {
                int vertexCount = segments*2; 
                primitiveCount = vertexCount - 1;
                vertices = new VertexPositionColor[vertexCount + 1];
                primitiveType = PrimitiveType.TriangleStrip;

                float ang = (float)Math.PI*2f;
                float angIncrement = (float)(2*Math.PI/segments);

                for (int i = 0; i < vertexCount; i += 2) {
                    float x = p.X + (float)Math.Cos(ang)*radius;
                    float y = p.Y + (float)Math.Sin(ang)*radius;
                    vertices[i] = new VertexPositionColor(new Vector3(x, y, 0), color);
                    vertices[i + 1] = new VertexPositionColor(new Vector3(p.X, p.Y, 0), color);
                    ang -= angIncrement;
                }

                vertices[vertexCount] = new VertexPositionColor(new Vector3(p.X + radius, p.Y, 0), color);
            } else {
                primitiveCount = segments;
                vertices = new VertexPositionColor[segments + 1];
                primitiveType = PrimitiveType.LineStrip;

                float ang = 0f;
                float angIncrement = (float)(2*Math.PI/segments);

                for (int i = 0; i < segments; i++) {
                    float x = p.X + (float)Math.Cos(ang)*radius;
                    float y = p.Y + (float)Math.Sin(ang)*radius;
                    vertices[i] = new VertexPositionColor(new Vector3(x, y, 0), color);
                    ang += angIncrement;
                }

                vertices[segments] = new VertexPositionColor(new Vector3(p.X + radius, p.Y, 0), color);
            }
        }
    }
}