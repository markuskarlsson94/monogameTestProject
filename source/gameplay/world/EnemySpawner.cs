using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class EnemySpawner {
        private float timerMax = 180f;
        private float timer;
        private Player player;

        public EnemySpawner(Player player) {
            this.player = player;
            timer = timerMax;
        }

        public void Update() {
            timer -= 1f;

            if (timer <= 0f) {
                timer = timerMax;
                Vector2 spawnPos = new Vector2(200, 200);
                GameGlobals.PassEnemy(new Enemy1(spawnPos, player));
            }
        }
    }
}