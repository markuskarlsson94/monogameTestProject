using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy1 : Enemy {
        public Enemy1(Vector2 pos, Player player) : base("sprPlayer", pos, player) {
            hitDistance = 10;
            hp = 5;
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
    }
}