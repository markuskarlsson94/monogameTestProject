using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace topdownShooter {
    public class PlayerProjectile : Projectile {
        public PlayerProjectile(Vector2 pos, GameObject owner, Vector2 direction) : base("sprBullet", pos, owner, direction) {
            rot = owner.rot + Globals.DegToRad(270);
        }

        public override void Update(List<GameObject> units) {
            base.Update(units);
        } 

        public override bool HitSomething(List<GameObject> units) {
            return base.HitSomething(units);
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}