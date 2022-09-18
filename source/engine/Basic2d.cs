using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace topdownShooter {
    public class Basic2d {
        public float rot;
        public Vector2 pos, size;
        public Texture2D texture;

        public Basic2d(string path, Vector2 pos) {
            this.pos = pos;
            //this.size = size;
            
            try {
                texture = Globals.content.Load<Texture2D>(path);
            } catch(System.NullReferenceException e) {
                System.Console.WriteLine($"Could not load texture {e}");
                //Game1.Exit();
            }

            this.size = new Vector2(texture.Width, texture.Height);
        }

        public virtual void Update() {

        }

        public virtual void Draw(Vector2 offset) {
            Globals.spriteBatch.Draw(texture, new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)(size.X), (int)(size.Y)), null, Color.White, rot, new Vector2(texture.Bounds.Width/2, texture.Bounds.Height/2), new SpriteEffects(), 0);
        }

        public virtual void Draw(Vector2 offset, Vector2 origin) {
            Globals.spriteBatch.Draw(texture, new Rectangle((int)(pos.X + offset.X), (int)(pos.Y + offset.Y), (int)(size.X), (int)(size.Y)), null, Color.White, rot, new Vector2(origin.X, origin.Y), new SpriteEffects(), 0);
        }
    }
}