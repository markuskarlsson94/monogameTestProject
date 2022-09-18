using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy : GameObject {
        public float speed;
        public float hp;
        protected float hitTimer, hitTimerMax;
        protected MovementComponent movementComponent;

        public Enemy(string path, Vector2 pos) : base(path, pos) {
            movementComponent = new MovementComponent();
            movementComponent.SetMaxSpeed(1);
            hitTimer = 0;
            hitTimerMax = 30f;
        }

        public void AddVel(Vector2 vel) {
            movementComponent.AddVel(vel);
        }

        public virtual void Update(Player player) {
            if (hitTimer <= 0) {
                if (Vector2.Distance(pos, player.pos) > 16) {
                    var dir = player.pos - pos;
                    dir.Normalize();
                    movementComponent.SetAcc(dir*0.5f);
                } else {
                    movementComponent.SetAcc(new Vector2(0, 0));
                }
            } else {
                hitTimer -= 1f;
            }

            movementComponent.Update(ref pos);
            base.Update();
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}