using Microsoft.Xna.Framework;
using System;

namespace topdownShooter {
    public class MovementComponent {
        private Vector2 acc;
        private Vector2 vel;
        private Vector2 externalVel;
        private float maxSpeed;
        private float maxExtSpeed;
        private float friction;

        public float MaxSpeed {
            get => maxSpeed;
            set => maxSpeed = value;
        }

        public MovementComponent() {
            acc = new Vector2();
            vel = new Vector2();
            externalVel = new Vector2();
            maxSpeed = 3f;
            maxExtSpeed = 6f;
            friction = 0.2f;
        }

        public void Update(ref Vector2 pos) {
            vel += acc;

            float curVel = vel.Length();
            if (curVel > maxSpeed) {
                vel.Normalize();
                vel *= maxSpeed;
            }

            float extSpeed = externalVel.Length();
            if (extSpeed > maxExtSpeed) {
                externalVel.Normalize();
                externalVel *= maxExtSpeed;
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

        public void SetMaxExtSpeed(float max) {
            maxExtSpeed = max;
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