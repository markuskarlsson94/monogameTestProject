using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace topdownShooter {
    public class World {
        public Player player;
        public List<Projectile> projectiles = new List<Projectile>();
        public List<Enemy> enemies = new List<Enemy>();
        public bool paused;

        public World() {
            player = new Player("sprPlayer", new Vector2(400, 200));
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassEnemy = AddEnemy;
            Vector2 offset = new Vector2(0, 0);
            paused = false;

            AddEnemy(new Enemy1(new Vector2(200, 200)));
        }

        public virtual void Update() {
            if (Globals.keyboard.GetPressed("P")) paused = !paused;

            if (!paused) {
                player.Update();

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
                        e.Update(player);
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

        public virtual void Draw(Vector2 offset) {
            player.Draw(offset);

            foreach (Projectile p in projectiles) {
                p.Draw(offset);
            }

            foreach (Enemy e in enemies) {
                e.Draw(offset);
            }

            string ammoString = $"Ammo: {player.Ammo.ToString()}/{player.AmmoMax.ToString()}";
            Globals.spriteBatch.DrawString(Globals.gameFont, ammoString , new Vector2(10, 10), Color.White);
        }
    }
}