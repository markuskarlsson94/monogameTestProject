using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy : GameObject {
        public float speed;
        public float hp;
        private MovementComponent movementComponent;

        public Enemy(string path, Vector2 pos) : base(path, pos) {
            movementComponent = new MovementComponent();
        }

        public void AddVel(Vector2 vel) {
            movementComponent.AddVel(vel);
        }

        public virtual void AI(Player player) {

        }

        public virtual void Update(Player player) {
            AI(player);
            base.Update();
            movementComponent.Update(ref pos);
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}