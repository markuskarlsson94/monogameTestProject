using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace topdownShooter {
    public class PlayerProjectile : Projectile {
        private float hitSpeed = 2f;
        private int damage;

        public PlayerProjectile(Vector2 pos, GameObject owner, Vector2 direction) : base("sprBullet", pos, owner, direction) {
            rot = owner.rot + Globals.DegToRad(270);
            damage = ((Player)owner).Damage;
        }

        public override bool HitSomething() {
            List<Enemy> enemies = (List<Enemy>)GameGlobals.GetEnemies();

            foreach (Enemy obj in enemies) {
                if (CollidingWith(obj)) {
                    Vector2 v = Vector2.Normalize(direction)*hitSpeed;
                    obj.GetHit(v, damage);
                    return true;
                }
            }

            return false;
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}