using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy : GameObject {
        public float speed;
        public float hp;
        protected float hitTimer, hitTimerMax;
        protected float acc = 0.25f;
        protected float speedMax = 1;
        protected MovementComponent movementComponent;
        protected Player player;

        public Enemy(string path, Vector2 pos, Player player) : base(path, pos) {
            movementComponent = new MovementComponent();
            movementComponent.SetMaxSpeed(speedMax);
            movementComponent.SetFriction(0.1f);
            hitTimer = 0;
            hitTimerMax = 30f;
            this.player = player;
        }

        public void AddVel(Vector2 vel) {
            movementComponent.AddVel(vel);
        }

        public void AddExternalVel(Vector2 vel) {
            movementComponent.AddExternalVel(vel);
        }

        public override void Update() {
            if (hitTimer <= 0) {
                if (Vector2.Distance(pos, player.pos) > 16) {
                    var dir = player.pos - pos;
                    dir.Normalize();
                    movementComponent.SetAcc(dir*acc);
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