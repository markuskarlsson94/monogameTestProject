using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace topdownShooter {
    public class World {
        public Player player;
        public List<Projectile> projectiles = new List<Projectile>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<XpOrb> orbs = new List<XpOrb>();
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
            if (Globals.keyboard.GetPressed("P")) {
                paused = !paused;
            }

            if (!paused && !hud.SelectingPowerup) {
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
                    XpOrb o = orbs[i];

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

            if (!paused) hud.Update();

            flashAlpha = Math.Max(flashAlpha - 0.05f, 0f);
        }

        public virtual void AddProjectile(object obj) {
            projectiles.Add((Projectile)obj);
        }

        public virtual void AddEnemy(object obj) {
            enemies.Add((Enemy)obj);
        }

        public virtual void AddOrb(object obj) {
            orbs.Add((XpOrb)obj);
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

            foreach (XpOrb o in orbs) {
                o.Draw(offset);
            }

            if (flashAlpha > 0) {
                DrawFunctions.DrawRectangle(new Vector2(0, 0), new Vector2(Globals.screenWidth, Globals.screenHeight), true, Color.Red*flashAlpha);
            }
        }

        public virtual void Draw2() {
            hud.Draw(); 
        }

        public virtual void Draw3() {
            if (paused) {
                DrawFunctions.DrawRectangle(new Vector2(0, 0), new Vector2(Globals.screenWidth, Globals.screenHeight), true, Color.Black*0.5f);
                Utility.DrawText(Globals.spriteBatch, new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), "Paused", Globals.gameFont, FontAlignment.middleCenter, Color.White);
            }
        } 
    }
}