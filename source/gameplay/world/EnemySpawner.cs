using System;
using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class EnemySpawner {
        private float timerMax = 240f;
        private float timer;
        private Player player;
        private Vector2 centerPos;
        private Random random;

        public EnemySpawner(Player player) {
            this.player = player;
            timer = timerMax;
            centerPos = new Vector2(Globals.screenWidth/2, Globals.screenHeight/2);
            random = new Random();
        }

        public void Update() {
            if (player.IsAlive()) {
                timer -= 1f;

                if (timer <= 0f) {
                    timer = timerMax;
                    Vector2 spawnVec = new Vector2(450, 0);

                    float dir = random.Next(360);
                    spawnVec = Utility.Vector2Rotated(spawnVec, dir);
                    GameGlobals.PassEnemy(new Enemy1(centerPos + spawnVec, player));
                }
            }
        }
    }
}