using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy1 : Enemy {
        public Enemy1(Vector2 pos) : base("sprPlayer", pos, new Vector2(16, 16)) {
            hitDistance = 8;
            hp = 3;
        }

        public override void GetHit() {
            System.Console.WriteLine("ouch!");
            hp -= 1;

            if (hp <= 0) {
                remove = true;
            }
        }
    }
}