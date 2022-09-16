using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy : Unit {
        public float speed;
        public float hp;
        //public float hitDist;

        public Enemy(string path, Vector2 pos, Vector2 size) : base(path, pos, size) {
            //dead = false;
            //hitDist = 35;
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