using Microsoft.Xna.Framework;

namespace topdownShooter
{
    public class Player : GameObject, IMovementComponent {
        private int hpMax = 4;
        private int hp;
        private float hurtTimerMax = 60f;
        private float hurtTimer;
        private int bulletTimerMax = 20;
        private int bulletTimer;
        private int ammoMax = 3;
        private int ammo;
        private int reloadTimerMax = 120;
        private int reloadTimer;
        private float acc = 0.5f;
        private int damage;
        private float orbDistanceCollectionRadius = 100f;
        private float bulletSpeed = 4f;
        private MovementComponent movementComponent;

        //Properties
        public int Ammo {
            get => ammo;
            set => ammo = value;
        }

        public int AmmoMax {
            get => ammoMax;
            set => ammoMax = value;
        }
        
        public int Hp {
            get => hp;
            set => hp = value;
        }
        
        public int HpMax {
            get => hpMax;
            set => hpMax = value;
        }

        public int Damage {
            get => damage;
            set => damage = value;
        }

        public int ReloadTimerMax {
            get => reloadTimerMax;
            set => reloadTimerMax = value;
        }

        public int BulletTimerMax {
            get => bulletTimerMax;
            set => bulletTimerMax = value;
        }

        public float OrbDistanceCollectionRadius {
            get => orbDistanceCollectionRadius;
            set => orbDistanceCollectionRadius = value;
        }

        public float BulletSpeed {
            get => bulletSpeed;
            set => bulletSpeed = value;
        }

        public Player(string path, Vector2 pos) : base(path, pos) {
            hp = hpMax;
            hurtTimer = 0;
            bulletTimer = 0;
            ammo = ammoMax;
            reloadTimer = reloadTimerMax;
            damage = 4;

            movementComponent = new MovementComponent();
        }

        public void AddVel(Vector2 vel) {
            movementComponent.AddVel(vel);
        }

        public void AddExternalVel(Vector2 vel) {
            movementComponent.AddExternalVel(vel);
        }

        public void Hurt() {
            if (hurtTimer <= 0f) {
                hurtTimer = hurtTimerMax;
                --hp;
            }
        }

        public bool IsAlive() {
            return hp > 0;
        }

        public override void Update() {
            if (IsAlive()) {
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
                pos = Vector2.Clamp(pos, new Vector2(0, 0), new Vector2(Globals.screenWidth, Globals.screenHeight));

                rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));

                //Shooting
                if (bulletTimer > 0) {
                    bulletTimer -= 1;
                }

                if (ammo <= 0) {
                    if (reloadTimer > 0) {
                        reloadTimer -= 1;

                        if (reloadTimer <= 0) {
                            reloadTimer = reloadTimerMax;
                            ammo = ammoMax;
                        }
                    }
                }

                if (Globals.mouse.LeftClickHold()) {
                    if (bulletTimer <= 0 && ammo > 0) {
                        Vector2 dir = Globals.mouse.newMousePos - pos;
                        GameGlobals.PassProjectile(new PlayerProjectile(pos, this, dir));
                        bulletTimer = bulletTimerMax;
                        ammo -= 1;
                        
                        dir.Normalize();
                        dir = -dir;
                        AddExternalVel(dir);
                    }
                }

                //Hurt timer
                if (hurtTimer >= 0) {
                    hurtTimer -= 1f;
                }
            }

            base.Update();
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}