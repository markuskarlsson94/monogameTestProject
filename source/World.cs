using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace topdownShooter {
    public class World {
        public Player player;
        public List<Projectile> projectiles = new List<Projectile>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Orb> orbs = new List<Orb>();
        public EnemySpawner enemySpawner;

        public bool paused;

        public World() {
            Init();
        }

        public void Init() {
            player = new Player("sprPlayer", new Vector2(400, 200));
            enemySpawner = new EnemySpawner(player);
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassEnemy = AddEnemy;
            GameGlobals.PassOrb = AddOrb;
            Vector2 offset = new Vector2(0, 0);
            paused = false;
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
                        p.Update(enemies.ToList<GameObject>());
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

            string ammoString = $"Ammo: {player?.Ammo.ToString()}/{player?.AmmoMax.ToString()}";
            Globals.spriteBatch.DrawString(Globals.gameFont, ammoString , new Vector2(10, 10), Color.White);
        }
    }
}