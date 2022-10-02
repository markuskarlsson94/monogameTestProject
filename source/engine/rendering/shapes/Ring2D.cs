using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace topdownShooter {
    public class Ring2D : Shape2D {
        private Vector2 p;
        private float innerRadius;
        private float outerRadius;
        private int segments;

        public Vector2 P {
            get { return p; }

            set {
                p = value;
                Update();
            }
        }

        public float InnerRadius {
            get { return innerRadius; }

            set {
                innerRadius = value;
                Update();
            }
        }

        public float OuterRadius {
            get { return outerRadius; }

            set {
                outerRadius = value;
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

        public Ring2D(Vector2 p, float innerRadius, float outerRadius, Color color = default(Color), int segments = 32) : base(color) {
            this.p = p;
            this.innerRadius = innerRadius;
            this.outerRadius = outerRadius;
            this.segments = ValidSegmentCount(segments);
            Update();
        }

        private int ValidSegmentCount(int segments) {
            return Math.Max(segments, 3);
        }

        protected override void Update() {
            int vertexCount = segments*2 + 1; 
            primitiveCount = vertexCount - 1;
            vertices = new VertexPositionColor[vertexCount + 1];
            primitiveType = PrimitiveType.TriangleStrip;

            float ang = (float)Math.PI*2f;
            float angIncrement = (float)(2*Math.PI/segments);

            for (int i = 0; i < vertexCount; i += 2) {
                float cos = (float)Math.Cos(ang);
                float sin = (float)Math.Sin(ang);
                float innerRad = Math.Min(innerRadius, outerRadius);
                float outerRad = Math.Max(innerRadius, outerRadius);
                ang -= angIncrement;

                float xInner = p.X + cos*innerRad;
                float yInner = p.Y + sin*innerRad;
                float xOuter = p.X + cos*outerRad;
                float yOuter = p.Y + sin*outerRad;

                vertices[i] = new VertexPositionColor(new Vector3(xOuter, yOuter, 0), color);
                vertices[i + 1] = new VertexPositionColor(new Vector3(xInner, yInner, 0), color);
            }
        }
    }
}