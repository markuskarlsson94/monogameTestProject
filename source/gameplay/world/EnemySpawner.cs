using System;
using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class EnemySpawner {
        private Player player;
        private Vector2 centerPos;
        private Random random;
        private float spawnAccumulator;
        private float spawnRate;
        private float spawnRateDelta;

        public float TimerMax { get; }
        public float SpawnRate { get => spawnRate; }

        public EnemySpawner(Player player) {
            this.player = player;
            centerPos = new Vector2(Globals.screenWidth/2, Globals.screenHeight/2);
            random = new Random();

            spawnAccumulator= 0f;
            spawnRate = 1/240f;
            spawnRateDelta = 1/800000f;
        }

        public void Update() {
            if (player.IsAlive()) {
                spawnRate += spawnRateDelta;
                spawnAccumulator += spawnRate;

                while (spawnAccumulator >= 1f) {
                    spawnAccumulator -= 1f;
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy() {
            Vector2 spawnVec = new Vector2(500, 0);

            float dir = random.Next(360);
            spawnVec = Utility.Vector2Rotated(spawnVec, dir);
            GameGlobals.PassEnemy(new Enemy1(centerPos + spawnVec, player));
        }
    }
}