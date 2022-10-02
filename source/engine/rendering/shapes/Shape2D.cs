using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace topdownShooter {
    public abstract class Shape2D {
        private static BasicEffect basicEffect = null;
        protected Color color;
        protected PrimitiveType primitiveType;
        protected VertexPositionColor[] vertices;
        protected int primitiveCount;

        public Color Color {
            get { return color; }

            set {
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
}