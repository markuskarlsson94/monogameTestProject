using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace topdownShooter {
    public class PlayerProjectile : Projectile {
        private float hitSpeed = 2f;

        public PlayerProjectile(Vector2 pos, GameObject owner, Vector2 direction) : base("sprBullet", pos, owner, direction) {
            rot = owner.rot + Globals.DegToRad(270);
        }

        public override void Update(List<GameObject> objects) {
            base.Update(objects);
        } 

        public override bool HitSomething(List<GameObject> objects) {
            foreach (GameObject obj in objects) {
                if (Vector2.Distance(pos, obj.pos) < obj.hitDistance) {
                    Vector2 v = Vector2.Normalize(direction)*hitSpeed;
                    obj.GetHit(v);
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