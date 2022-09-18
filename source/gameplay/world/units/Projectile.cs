//using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace topdownShooter {
    public class Projectile : GameObject {
        public float speed;
        public Vector2 direction;
        public GameObject owner;
        public GameTimer timer;

        public Projectile(string path, Vector2 pos, GameObject owner, Vector2 direction) : base(path, pos) {
            speed = 4;
            this.owner = owner;
            direction.Normalize();
            this.direction = direction;
            timer = new GameTimer(1000);
        }

        public virtual void Update(List<GameObject> units) {
            pos += direction*speed;

            timer.UpdateTimer();

            if (timer.HasExpired()) {
                remove = true;
            }

            if (HitSomething(units)) {
                remove = true;
            }
        } 

        public virtual bool HitSomething(List<GameObject> units) {
            foreach (GameObject u in units) {
                if (Vector2.Distance(pos, u.pos) < u.hitDistance) {
                    System.Console.WriteLine(u);
                    u.GetHit();
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