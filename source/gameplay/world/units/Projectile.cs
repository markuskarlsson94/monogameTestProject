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

        public override void Update() {
            pos += direction*speed;

            timer.UpdateTimer();

            if (timer.HasExpired() || HitSomething()) {
                Remove();
            }
        } 

        //public virtual bool HitSomething(List<GameObject> objects) {
        public virtual bool HitSomething() {
            return false;
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}