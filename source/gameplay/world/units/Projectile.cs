//using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace testProject {
    public class Projectile : Unit {
        public float speed;
        public Vector2 direction;
        public Unit owner;
        public GameTimer timer;
        public bool remove = false;

        public Projectile(string path, Vector2 pos, Vector2 size, Unit owner, Vector2 direction) : base(path, pos, size) {
            speed = 4;
            this.owner = owner;
            direction.Normalize();
            this.direction = direction;
            timer = new GameTimer(1000);
        }

        public virtual void Update(List<Unit> units) {
            pos += direction*speed;

            timer.UpdateTimer();

            if (timer.HasExpired()) {
                remove = true;
            }

            if (HitSomething(units)) {
                remove = true;
            }
        } 

        public virtual bool HitSomething(List<Unit> units) {
            return false;
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}