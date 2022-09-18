using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy1 : Enemy {
        public Enemy1(Vector2 pos) : base("sprPlayer", pos) {
            hitDistance = 10;
            hp = 5;
        }

        public override void GetHit(Vector2 vel) {
            AddVel(vel);

            hp -= 1;
            if (hp <= 0) {
                remove = true;
            }
        }
    }
}