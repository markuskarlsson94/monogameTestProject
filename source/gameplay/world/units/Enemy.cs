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

        public override void GetHit(Vector2 vel) {
            AddExternalVel(vel);

            movementComponent.SetAcc(new Vector2(0, 0));
            hitTimer = hitTimerMax;

            hp -= 1;
            if (hp <= 0) {
                remove = true;
                Orb orb = new Orb(pos, player);
                orb.AddExternalVel(vel);
                GameGlobals.PassOrb(orb);
                GameGlobals.IncreaseScore();
            }
        }

        public override void Update() {
            float playerDis = Vector2.Distance(pos, player.pos);

            if (hitTimer <= 0) {
                if (playerDis > 10) {
                    var dir = player.pos - pos;
                    dir.Normalize();
                    movementComponent.SetAcc(dir*acc);
                } else {
                    movementComponent.SetAcc(new Vector2(0, 0));
                    player.Hurt();
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