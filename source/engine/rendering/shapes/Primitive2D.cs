using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace topdownShooter {
    public class Primitive2D : Shape2D {
        public PrimitiveType PrimitiveType {
            get { return primitiveType; }

            set {
                primitiveType = value;
                Update();
            }
        }

        public VertexPositionColor[] Vertices {
            get { return vertices; }

            set {
                vertices = value;
                Update();
            }
        }

        public VertexPositionColor this[int i] {
            get { return vertices[i]; }

            set {
                vertices[i] = value;
                Update();
            }
        }

        public Primitive2D(PrimitiveType type, VertexPositionColor[] vertices) : base(default(Color)) {
            primitiveType = type;
            this.vertices = vertices;
            Update();
        }

        protected override void Update() {
            int count = vertices.Length;

            if (primitiveType == PrimitiveType.TriangleList) {
                primitiveCount = (int)Math.Floor((decimal)count/3);
            } else if (primitiveType == PrimitiveType.TriangleStrip) {
                primitiveCount = count - 2;
            } else if (primitiveType == PrimitiveType.LineList) {
                primitiveCount = (int)Math.Ceiling((decimal)count/2);
            } else {
                primitiveCount = count - 1; //LineStrip
            }
        }
    }
}