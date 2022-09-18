using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy : GameObject {
        public float speed;
        public float hp;

        public Enemy(string path, Vector2 pos) : base(path, pos) {
            
        }

        public virtual void AI(Player player) {

        }

        public virtual void Update(Player player) {
            AI(player);
            base.Update();
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}