using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Enemy1 : Enemy {
        public Enemy1(Vector2 pos, Player player) : base("sprEnemy", pos, player) {
            collisionRadius = 10f;
            hp = 20;
        }
    }
}