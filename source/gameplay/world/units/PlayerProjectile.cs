using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace testProject {
    public class PlayerProjectile : Projectile {

        public PlayerProjectile(Vector2 pos, Unit owner, Vector2 direction) : base("sprPlayer", pos, new Vector2(16, 16), owner, direction) {

        }

        public override void Update(List<Unit> units) {
            base.Update(units);
        } 

        public override bool HitSomething(List<Unit> units) {
            return false;
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}