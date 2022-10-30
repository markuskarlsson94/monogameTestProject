using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace topdownShooter {
    public class World {
        public Player player;
        public List<Projectile> projectiles = new List<Projectile>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Orb> orbs = new List<Orb>();
        public EnemySpawner enemySpawner;
        public bool paused;
        public int score;
        public int xp, xpMax, level;
        public HUD hud;
        private float flashAlpha;

        public World() {
            Init();
        }

        public void Init() {
            player = new Player("sprPlayer", new Vector2(400, 200));
            player.damaged += OnDamaged;
            enemySpawner = new EnemySpawner(player);
            score = 0;
            xp = 0;
            xpMax = 4;
            level = 1;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassEnemy = AddEnemy;
            GameGlobals.PassOrb = AddOrb;
            GameGlobals.IncreaseScore = IncreaseScore;
            GameGlobals.IncreaseXp = IncreaseXp;
            GameGlobals.GetEnemies = GetEnemies;
            GameGlobals.GetWorld = () => this;

            Vector2 offset = new Vector2(0, 0);
            paused = false;

            hud = new HUD(this);
            flashAlpha = 0;
        }

        public void Reset() {
            projectiles.Clear();
            enemies.Clear();
            orbs.Clear();
            player = null;
        }

        public virtual void Update() {
            if (Globals.keyboard.GetPressed("P")) paused = !paused;

            if (!paused) {
                player?.Update();
                enemySpawner.Update();

                for (int i = 0; i < projectiles.Count; i++) {
                    Projectile p = projectiles[i];

                    if (p.remove) {
                        projectiles.RemoveAt(i);
                        i--;
                    } else {
                        p.Update();
                    }
                }

                for (int i = 0; i < enemies.Count; i++) {
                    Enemy e = enemies[i];

                    if (e.remove) {
                        enemies.RemoveAt(i);
                        i--;
                    } else {
                        e.Update();
                    }
                }

                for (int i = 0; i < orbs.Count; i++) {
                    Orb o = orbs[i];

                    if (o.remove) {
                        orbs.RemoveAt(i);
                        i--;
                    } else {
                        o.Update();
                    }
                }
                
                /*foreach (Projectile p in projectiles) {
                    if (p.done) {
                        projectiles.Remove(p);
                    } else {
                        p.Update(null);
                    }
                }*/

            }

            hud.Update();

            flashAlpha = Math.Max(flashAlpha - 0.05f, 0f);
        }

        public virtual void AddProjectile(object obj) {
            projectiles.Add((Projectile)obj);
        }

        public virtual void AddEnemy(object obj) {
            enemies.Add((Enemy)obj);
        }

        public virtual void AddOrb(object obj) {
            orbs.Add((Orb)obj);
        }

        public virtual void IncreaseScore() {
            ++score;
        }

        public virtual void IncreaseXp() {
            if (++xp >= xpMax) {
                xp = 0;
                xpMax += 2;
                ++level;

                hud.CreatePowerupSelection();
                paused = true;
            }
        }

        public virtual void OnPowerupSelected(object sender, EventArgs eventArgs) {
            paused = false;
        }

        public List<Enemy> GetEnemies() {
            return enemies;
        }

        private void OnDamaged(object sender, EventArgs events) {
            flashScreen();
        }

        private void flashScreen() {
            flashAlpha = 0.5f;
        }

        public virtual void Draw(Vector2 offset) {
            player?.Draw(offset);

            foreach (Projectile p in projectiles) {
                p.Draw(offset);
            }

            foreach (Enemy e in enemies) {
                e.Draw(offset);
            }

            foreach (Orb o in orbs) {
                o.Draw(offset);
            }

            if (flashAlpha > 0) {
                DrawFunctions.DrawRectangle(new Vector2(0, 0), new Vector2(Globals.screenWidth, Globals.screenHeight), true, Color.Red*flashAlpha);
            }
        }

        public virtual void Draw2() {
            hud.Draw();
        }
    }
}