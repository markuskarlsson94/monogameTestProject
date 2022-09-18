using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Orb :GameObject {
        private Player player;

        public Orb(Vector2 pos, Player player) : base("sprOrb", pos) {
            this.player = player;
        }

        public override void Update() {
            if (Vector2.Distance(pos, player.pos) < 8f) remove = true;
            base.Update();
        }
    }
}