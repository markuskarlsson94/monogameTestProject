using Microsoft.Xna.Framework;
using System;

namespace topdownShooter {
    public class MovementComponent {
        private Vector2 acc;
        private Vector2 vel;
        private Vector2 externalVel;
        private float maxVel;
        private float friction;

        public MovementComponent() {
            acc = new Vector2();
            vel = new Vector2();
            externalVel = new Vector2();
            maxVel = 3;
            friction = 0.2f;
        }

        public void Update(ref Vector2 pos) {
            vel += acc;

            float curVel = vel.Length();
            if (curVel > maxVel) {
                vel.Normalize();
                vel *= maxVel;
            }

            Vector2 totalVel = vel + externalVel;
            pos += totalVel;

            //Apply friction
            float speed = totalVel.Length();
            float speedReduced = Math.Max(speed - friction, 0);

            if (speedReduced > 0) {
                float scale = speedReduced/speed;
                vel *= scale;
                externalVel *= scale;
            } else {
                vel = new Vector2(0, 0);
                externalVel = new Vector2(0, 0);
            }
        }

        public void SetAcc(Vector2 acc) {
            this.acc = acc;
        }

        public void AddVel(Vector2 vel) {
            this.vel += vel;
        }

        public void AddExternalVel(Vector2 vel) {
            externalVel += vel;
        }

        public void SetMaxSpeed(float max) {
            maxVel = max;
        }

        public void SetFriction(float friction) {
            this.friction = friction;
        }
    }

    public interface IMovementComponent {
        public void AddVel(Vector2 vel);
        public void AddExternalVel(Vector2 vel);
    }
}