using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Orb : GameObject, IMovementComponent {
        private Player player;
        private MovementComponent movementComponent;
        private float lifeTimer;

        public Orb(Vector2 pos, Player player) : base("sprOrb", pos) {
            this.player = player;
            lifeTimer = player.OrbLifetime;
            movementComponent = new MovementComponent();
            movementComponent.MaxSpeed = 5f;
            movementComponent.SetFriction(0.1f);
        }

        public override void Update() {
            float dist = Vector2.Distance(pos, player.pos);
            float distMax = player.OrbDistanceCollectionRadius;
            Vector2 acc = new Vector2(0, 0);

            if (dist < 10f) {
                Remove();
                GameGlobals.IncreaseXp();
            } else if (dist < distMax) {
                var dir = player.pos - pos;
                dir.Normalize();

                float frac = 1f - (dist/distMax);
                movementComponent.AddVel(dir*frac*1f);
            }

            movementComponent.SetAcc(acc);
            movementComponent.Update(ref pos);

            lifeTimer -= 1f;
            if (lifeTimer <= 0) {
                Remove();
            }

            base.Update();
        }

        public void AddVel(Vector2 vel) {
            movementComponent.AddVel(vel);
        }

        public void AddExternalVel(Vector2 vel) {
            movementComponent.AddExternalVel(vel);
        }
    }
}