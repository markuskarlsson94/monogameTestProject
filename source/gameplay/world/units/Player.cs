using Microsoft.Xna.Framework;

namespace topdownShooter
{
    public class Player : GameObject, IMovementComponent {
        private float canShootTimerMax = 20f;
        private float canShootTimer;
        private float ammoMax = 3;
        private float ammo;
        private float ammoTimerMax = 120f;
        private float ammoTimer;
        private float acc = 0.5f;

        //Properties
        public float Ammo => ammo;
        public float AmmoMax => ammoMax;

        private MovementComponent movementComponent;

        public Player(string path, Vector2 pos) : base(path, pos) {
            canShootTimer = 0;
            ammo = ammoMax;
            ammoTimer = ammoTimerMax;

            movementComponent = new MovementComponent();
        }

        public void AddVel(Vector2 vel) {
            movementComponent.AddVel(vel);
        }

        public void AddExternalVel(Vector2 vel) {
            movementComponent.AddExternalVel(vel);
        }

        public override void Update() {
            //Input
            float horizontalInput = 0.0F;
            if (Globals.keyboard.GetPress("D")) {
                horizontalInput += 1.0F;
            } else if (Globals.keyboard.GetPress("A")) {
                horizontalInput -= 1.0F;
            }

            float verticalInput = 0.0F;
            if (Globals.keyboard.GetPress("S")) {
                verticalInput += 1.0F;
            } else if (Globals.keyboard.GetPress("W")) {
                verticalInput -= 1.0F;
            }

            //Movement
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            if (inputVector.Length() > 0) inputVector.Normalize();

            movementComponent.SetAcc(inputVector*acc);
            movementComponent.Update(ref pos);

            rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));

            //Shooting
            if (canShootTimer > 0) {
                canShootTimer -= 1f;
            }

            if (ammo <= 0) {
                if (ammoTimer > 0) {
                    ammoTimer -= 1f;

                    if (ammoTimer <= 0) {
                        ammoTimer = ammoTimerMax;
                        ammo = ammoMax;
                    }
                }
            }

            if (Globals.mouse.LeftClickHold()) {
                if (canShootTimer <= 0 && ammo > 0) {
                    Vector2 dir = Globals.mouse.newMousePos - pos;
                    GameGlobals.PassProjectile(new PlayerProjectile(pos, this, dir));
                    canShootTimer = canShootTimerMax;
                    ammo -= 1;
                    
                    dir.Normalize();
                    dir = -dir;
                    AddVel(dir);
                }
            }

            base.Update();
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}