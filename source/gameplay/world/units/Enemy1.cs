using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy1 : Enemy {
        public Enemy1(Vector2 pos) : base("sprPlayer", pos) {
            hitDistance = 8;
            hp = 2;
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