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
        }

        public void CreatePowerupSelection() {
            powerupSelection = new PowerupSelection();
            powerupSelection.AddEventHandler(OnPowerupSelected);
            powerupSelection.AddEventHandler(world.OnPowerupSelected);
        }

        public void OnPowerupSelected(object sender, EventArgs events) {
            powerupSelection = null;
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
                string hpString = $"HP: {player.Hp.ToString()}/{player.HpMax.ToString()}";
                string ammoString = $"Ammo: {player.Ammo.ToString()}/{player.AmmoMax.ToString()}";
                string scoreString = $"Score: {world.score}";
                string xpString = $"Level {world.level} ({world.xp}/{world.xpMax})";

                Globals.spriteBatch.DrawString(Globals.gameFont, hpString , new Vector2(10, 10), Color.White);
                Globals.spriteBatch.DrawString(Globals.gameFont, ammoString , new Vector2(10, 30), Color.White);
                Globals.spriteBatch.DrawString(Globals.gameFont, scoreString , new Vector2(10, 50), Color.White);
                Globals.spriteBatch.DrawString(Globals.gameFont, xpString , new Vector2(middle + xpWidth/2 + 8, 4), Color.White);

                string damageString = $"Damage: {player.Damage}";
                Globals.spriteBatch.DrawString(Globals.gameFont, damageString, new Vector2(10, 470), Color.White);

                float ratio = (float)world.xp/(float)world.xpMax;
                xpRect.P1 = new Vector2(middle - xpWidth/2 + (ratio*xpWidth), 15);

                xpRect.Draw();
                xpRectOutline.Draw();

                if (powerupSelection != null) powerupSelection.Draw();
            }
        }
    }
}