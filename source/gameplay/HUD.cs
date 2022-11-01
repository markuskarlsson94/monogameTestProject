using Microsoft.Xna.Framework;
using System;

namespace topdownShooter {
    public class HUD {
        private World world;
        private Rectangle2D xpRect, xpRectOutline;
        private float middle, xpWidth, xpHeight;
        private PowerupSelection powerupSelection;
        string introText;
        float introTextAlpha;

        public bool SelectingPowerup { get; set; }

        public HUD(World world) {
            this.world = world;

            middle = Globals.screenWidth/2;
            xpWidth = 150;
            xpHeight = 5;
            //float xpHeight = 5;
            xpRect = new Rectangle2D(new Vector2(middle - xpWidth/2, 10), new Vector2(middle - xpWidth/2, 15), true);
            xpRectOutline = new Rectangle2D(new Vector2(middle - xpWidth/2, 10), new Vector2(middle + xpWidth/2, 10 + xpHeight), false);
            
            introText = "Move: WASD, Shoot: Left mouse button";
            introTextAlpha = 5f;

            SelectingPowerup = false;
        }

        public void CreatePowerupSelection() {
            powerupSelection = new PowerupSelection();
            powerupSelection.AddEventHandler(OnPowerupSelected);
            powerupSelection.AddEventHandler(world.OnPowerupSelected);
            SelectingPowerup = true;
        }

        public void OnPowerupSelected(object sender, EventArgs events) {
            powerupSelection = null;
            SelectingPowerup = false;
        }

        public virtual void Update() {
            if (powerupSelection != null) powerupSelection.Update();
            introTextAlpha = Math.Max(introTextAlpha - 0.01f, 0);
        }

        public virtual void Draw() {
            if (introTextAlpha > 0) {
                Utility.DrawText(Globals.spriteBatch, new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), introText, Globals.gameFont, FontAlignment.middleCenter, Color.White*introTextAlpha);
            }

            Player player = world.player;

            if (player != null) {
                float alpha = 0.5f;

                string hpString = $"HP: {player.Hp.ToString()}/{player.HpMax.ToString()}";
                string ammoString = $"Ammo: {player.Ammo.ToString()}/{player.AmmoMax.ToString()}";
                string scoreString = $"Score: {world.score}";
                string xpString = $"Level {world.level} ({world.xp}/{world.xpMax})";

                Globals.spriteBatch.DrawString(Globals.gameFont, hpString , new Vector2(10, 10), Color.White);
                Globals.spriteBatch.DrawString(Globals.gameFont, ammoString , new Vector2(10, 30), Color.White);
                Globals.spriteBatch.DrawString(Globals.gameFont, scoreString , new Vector2(10, 50), Color.White);
                Globals.spriteBatch.DrawString(Globals.gameFont, xpString , new Vector2(middle + xpWidth/2 + 8, 4), Color.White);
                
                string DPSString = $"{player.DamagePerSecond().ToString("0.0")} damage/s";
                float spawnRate = ((World)GameGlobals.GetWorld()).enemySpawner.SpawnRate*60f;
                string spawnString = $"Spawn rate: {spawnRate.ToString("0.000")}/s";
                Utility.DrawText(Globals.spriteBatch, new Vector2(790, 450), DPSString, Globals.gameFont, FontAlignment.topRight, Color.White*alpha);
                Utility.DrawText(Globals.spriteBatch, new Vector2(790, 470), spawnString, Globals.gameFont, FontAlignment.topRight, Color.White*alpha);

                string damageString = $"Damage: {player.Damage}";
                string speedString = $"Speed: {player.MaxSpeed}";
                string reloadTimerString = $"Reload time: {player.ReloadTimerMax}";
                string bulletTimerString = $"Bullet time: {player.BulletTimerMax}";
                string bulletAmountString = $"Bullet amount: {player.BulletAmount}";
                string bulletSpeedString = $"Bullet speed: {player.BulletSpeed}";
                string bulletHitsString = $"Bullet hits: {player.EnemyHitsMax}";
                string xpCollectionRadiusString = $"Xp pickup radius: {player.OrbDistanceCollectionRadius}";
                string xpLifetimeString = $"Xp lifetime: {player.OrbLifetime}";
                
                int yPos = 470;
                int i = 0;

                Globals.spriteBatch.DrawString(Globals.gameFont, xpLifetimeString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, xpCollectionRadiusString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, bulletHitsString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, bulletSpeedString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, bulletTimerString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, bulletAmountString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, reloadTimerString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, speedString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);
                Globals.spriteBatch.DrawString(Globals.gameFont, damageString, new Vector2(10, yPos - (20*i++)), Color.White*alpha);

                float ratio = (float)world.xp/(float)world.xpMax;
                xpRect.P1 = new Vector2(middle - xpWidth/2 + (ratio*xpWidth), 15);

                xpRect.Draw();
                xpRectOutline.Draw();

                if (powerupSelection != null) powerupSelection.Draw();
            }
        }
    }
}