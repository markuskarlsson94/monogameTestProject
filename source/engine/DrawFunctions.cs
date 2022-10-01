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

    public abstract class Shape2D {
        private static BasicEffect basicEffect = null;
        protected Color color;
        protected PrimitiveType primitiveType;
        protected VertexPositionColor[] vertices;
        protected int primitiveCount;

        public Color Color {
            get { return color; }

            set {
                System.Console.WriteLine("update color");
                color = value;
                Update();
            }
        }

        public Shape2D(Color color) {
            if (object.Equals(color, default(Color))) color = Color.White;
            this.color = color;
            
            if (basicEffect == null) {
                basicEffect = new BasicEffect(Globals.graphicsDevice);
                basicEffect.VertexColorEnabled = true;
                basicEffect.World = Matrix.CreateOrthographicOffCenter(0, Globals.graphicsDevice.Viewport.Width, Globals.graphicsDevice.Viewport.Height, 0, 0, 1);
            }
        }

        protected abstract void Update();

        public void Draw() {
            EffectTechnique effectTechnique = basicEffect.Techniques[0];
            EffectPassCollection effectPassCollection = effectTechnique.Passes;
            basicEffect.CurrentTechnique.Passes[0].Apply();
            Globals.graphicsDevice.DrawUserPrimitives(primitiveType, vertices, 0, primitiveCount);
        }
    }

    public class Line2D : Shape2D {
        private Vector2 p0;
        private Vector2 p1;

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

        public Line2D(Vector2 p0, Vector2 p1, Color color = default(Color)) : base(color) {
            this.p0 = p0;
            this.p1 = p1;
            Init();
        }

        public Line2D(float x0, float y0, float x1, float y1, Color color = default(Color)) : base(color) {
            p0 = new Vector2(x0, y0);
            p1 = new Vector2(x1, y1);
            Init();
        }

        private void Init() {
            primitiveType = PrimitiveType.LineStrip;
            primitiveCount = 1;
            Update();
        }

        protected override void Update() {
            vertices = new[] {
                new VertexPositionColor(new Vector3(p0, 0), color),
                new VertexPositionColor(new Vector3(p1, 0), color)
            };
        }
    }

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