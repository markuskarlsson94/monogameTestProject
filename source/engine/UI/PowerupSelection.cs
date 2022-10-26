using Microsoft.Xna.Framework;
using System;

namespace topdownShooter {
    public class PowerupSelection {
        private PowerupCard card0;
        private PowerupCard card1;
        private PowerupCard card2;

        public PowerupSelection() {
            card0 = new HpPowerupCard();
            card1 = new BulletTimerPowerupCard();
            card2 = new DamagePowerupCard();

            card0.Pos = new Vector2(150, 150);
            card1.Pos = new Vector2(330, 150);
            card2.Pos = new Vector2(510, 150);
        }

        public void AddEventHandler(EventHandler eventHandler) {
            card0.powerupSelected += eventHandler;
            card1.powerupSelected += eventHandler;
            card2.powerupSelected += eventHandler;
        }

        public virtual void Update() {
            card0.Update();
            card1.Update();
            card2.Update();
        }

        public virtual void Draw() {
            card0.Draw();
            card1.Draw();
            card2.Draw();
        }
    }
}