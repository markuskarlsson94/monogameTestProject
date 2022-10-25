using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace topdownShooter {
    public class Enemy : GameObject {
        public float speed;
        public float hp;
        protected float hitTimer, hitTimerMax;
        private float moveAwayTimer, moveAwayTimerMax = 4f;
        protected float acc = 0.25f;
        protected float speedMax = 1;
        protected MovementComponent movementComponent;
        protected Player player;

        public Enemy(string path, Vector2 pos, Player player) : base(path, pos) {
            movementComponent = new MovementComponent();
            movementComponent.SetMaxSpeed(speedMax);
            movementComponent.SetFriction(0.1f);
            hitTimer = 0;
            hitTimerMax = 30f;
            moveAwayTimer = 0;
            this.player = player;
        }

        public void AddVel(Vector2 vel) {
            movementComponent.AddVel(vel);
        }

        public void AddExternalVel(Vector2 vel) {
            movementComponent.AddExternalVel(vel);
        }

        public virtual void GetHit(Vector2 vel, int damage) {
            AddExternalVel(vel);

            movementComponent.SetAcc(new Vector2(0, 0));
            hitTimer = hitTimerMax;

            hp -= damage;
            
            if (hp <= 0) {
                Remove();
                Orb orb = new Orb(pos, player);
                orb.AddExternalVel(vel);
                GameGlobals.PassOrb(orb);
                GameGlobals.IncreaseScore();
            }
        }

        public void StopMoving() {
            movementComponent.SetAcc(new Vector2(0, 0));
        }

        public override void Update() {
            if (player.IsAlive()) {
                if (hitTimer <= 0) {
                    if (!CollidingWith(player)) {
                        var dir = player.pos - pos;
                        dir.Normalize();
                        movementComponent.SetAcc(dir*acc);
                    } else {
                        StopMoving();
                        player.Hurt();
                    }
                } else {
                    hitTimer -= 1f;
                }
            } else {
                StopMoving();
            }

            moveAwayTimer -= 1f;
            if (moveAwayTimer <= 0) {
                moveAwayTimer = moveAwayTimerMax;
                MoveAwayFromEnemies();
            }
            
            movementComponent.Update(ref pos);
            base.Update();
        }

        private void MoveAwayFromEnemies() {
            List<Enemy> enemies = (List<Enemy>)GameGlobals.GetEnemies();

            foreach (Enemy enemy in enemies) {
                if (enemy == this) continue;

                if (CollidingWith(enemy)) {
                    Vector2 diff = enemy.pos - pos;
                    diff.Normalize();
                    AddVel(-diff/2);
                }
            }
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}