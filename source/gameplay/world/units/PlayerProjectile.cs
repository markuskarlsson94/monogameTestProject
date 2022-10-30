using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace topdownShooter {
    public class PlayerProjectile : Projectile {
        private float hitSpeed = 2f;
        private int damage;
        private HashSet<Enemy> hitEnemies;
        private int enemyHits;
        private int enemyHitsMax;

        public PlayerProjectile(Vector2 pos, GameObject owner, Vector2 direction) : base("sprBullet", pos, owner, direction) {
            rot = (float)System.Math.Atan2(direction.Y, direction.X);
            Player player = (Player)owner;
            damage = player.Damage;
            speed = player.BulletSpeed;
            hitEnemies = new HashSet<Enemy>();
            enemyHits = 0;
            enemyHitsMax = player.EnemyHitsMax;
        }

        public override bool HitSomething() {
            List<Enemy> enemies = (List<Enemy>)GameGlobals.GetEnemies();

            foreach (Enemy obj in enemies) {
                if (CollidingWith(obj)) {
                    if (!hitEnemies.Contains(obj)) {
                        hitEnemies.Add(obj);
                        enemyHits++;
                        Vector2 v = Vector2.Normalize(direction)*hitSpeed;
                        obj.GetHit(v, damage);

                        return enemyHits >= enemyHitsMax;
                    }
                }
            }

            return false;
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}