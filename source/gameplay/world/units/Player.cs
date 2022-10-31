using Microsoft.Xna.Framework;
using System;

namespace topdownShooter
{
    public class Player : GameObject, IMovementComponent {
        private float hurtTimerMax = 60f;
        private float hurtTimer;
        private float acc = 0.5f;
        private float bulletDirDiff = 12f;
        private MovementComponent movementComponent;
        public event EventHandler damaged;

        //Properties
        public int Ammo { get; set; }
        public int AmmoMax { get; set; }
        public int Hp { get; set; }
        public int HpMax { get; set; }
        public int Damage { get; set; }
        public int BulletTimer { get; set; }
        public int BulletTimerMax { get; set; }
        public float OrbDistanceCollectionRadius { get; set; }
        public int OrbLifetime { get; set; }
        public float BulletSpeed { get; set; }
        private int ReloadTimer { get; set; }
        public int ReloadTimerMax { get; set; }
        public int EnemyHitsMax { get; set; }
        public int BulletAmount { get; set; }

        public float MaxSpeed {
            get => movementComponent.MaxSpeed;
            set {
                movementComponent.MaxSpeed = value;
            }
        }

        public Player(string path, Vector2 pos) : base(path, pos) {
            HpMax = 2;
            Hp = HpMax;
            hurtTimer = 0;
            BulletTimerMax = 20;
            BulletTimer = 0;
            AmmoMax = 3;
            Ammo = AmmoMax;
            ReloadTimerMax = 120;
            ReloadTimer = ReloadTimerMax;
            OrbDistanceCollectionRadius = 60f;
            OrbLifetime = 480;
            BulletSpeed = 4f;
            Damage = 4;
            EnemyHitsMax = 1;
            BulletAmount = 1;

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
                --Hp;
                OnDamaged();
            }
        }

        public bool IsAlive() {
            return Hp > 0;
        }

        private void OnDamaged() {
            if (damaged != null) damaged(this, null); 
        }

        public float DamagePerSecond() {
            float duration = (AmmoMax - 1)*BulletTimerMax + ReloadTimerMax;
            float damage = Damage*AmmoMax*BulletAmount*EnemyHitsMax;

            return damage/(duration/60);
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
                if (BulletTimer > 0) {
                    BulletTimer -= 1;
                }

                if (Ammo <= 0) {
                    if (ReloadTimer > 0) {
                        ReloadTimer -= 1;

                        if (ReloadTimer <= 0) {
                            ReloadTimer = ReloadTimerMax;
                            Ammo = AmmoMax;
                        }
                    }
                }

                if (Globals.mouse.LeftClickHold()) {
                    if (BulletTimer <= 0 && Ammo > 0) {
                        Vector2 dir = Globals.mouse.newMousePos - pos;
                        Vector2 bulletDir = dir;
                        float offset = (bulletDirDiff*(BulletAmount - 1))/2;
                        bulletDir = Utility.Vector2Rotated(bulletDir, -offset);

                        for (int i = 0; i < BulletAmount; i++) {
                            GameGlobals.PassProjectile(new PlayerProjectile(pos, this, bulletDir));
                            bulletDir = Utility.Vector2Rotated(bulletDir, bulletDirDiff);
                        }

                        BulletTimer = BulletTimerMax;
                        Ammo -= 1;
                        
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