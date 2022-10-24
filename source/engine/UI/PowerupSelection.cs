using Microsoft.Xna.Framework;
using System;

namespace topdownShooter {
    public class PowerupSelection {
        private PowerupCard card0;
        private PowerupCard card1;
        private PowerupCard card2;

        public PowerupSelection() {
            card0 = new HpPowerupCard(new Vector2(200, 200));
            card1 = new AmmoPowerupCard(new Vector2(400, 200));
            card2 = new DamagePowerupCard(new Vector2(600, 200));
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