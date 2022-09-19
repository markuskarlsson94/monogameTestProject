using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Orb :GameObject {
        private Player player;
        private MovementComponent movementComponent;
        private float distanceThreshold = 128f;

        public Orb(Vector2 pos, Player player) : base("sprOrb", pos) {
            this.player = player;
            movementComponent = new MovementComponent();
            movementComponent.SetMaxSpeed(10f);
            movementComponent.SetFriction(0.1f);
        }

        public override void Update() {
            float dist = Vector2.Distance(pos, player.pos);
            Vector2 acc = new Vector2(0, 0);

            if (dist < 10f) {
                remove = true;
            } else if (dist < distanceThreshold) {
                var dir = player.pos - pos;
                dir.Normalize();

                float frac = 1f - (dist/distanceThreshold);
                movementComponent.AddVel(dir*frac*1.5f);
            }

            movementComponent.SetAcc(acc);
            movementComponent.Update(ref pos);

            base.Update();
        }
    }
}