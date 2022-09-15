using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace topdownShooter {
    public class World {
        public Player player;
        public List<Projectile> projectiles = new List<Projectile>();

        public World() {
            player = new Player("sprPlayer", new Vector2(400, 200), new Vector2(16, 16));
            GameGlobals.PassProjectile = AddProjectile;
            Vector2 offset = new Vector2(0, 0);
        }

        public virtual void Update() {
            player.Update();

            for (int i = 0; i < projectiles.Count; i++) {
                Projectile p = projectiles[i];

                if (p.remove) {
                    projectiles.RemoveAt(i);
                    i--;
                } else {
                    p.Update(null);
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

        public virtual void AddProjectile(object obj) {
            projectiles.Add((Projectile)obj);
        }

        public virtual void Draw(Vector2 offset) {
            player.Draw(offset);

            foreach (Projectile p in projectiles) {
                p.Draw(offset);
            }
        }
    }
}