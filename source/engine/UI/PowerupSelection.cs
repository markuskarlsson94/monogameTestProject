using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace topdownShooter {
    public class PowerupSelection {
        private PowerupCard card0;
        private PowerupCard card1;
        private PowerupCard card2;

        public PowerupSelection() {
            List<PowerupCard> cards = new List<PowerupCard> {
                new DamagePowerupCard(),
                new HpPowerupCard(),
                new AmmoPowerupCard(),
                new ReloadTimerPowerupCard(),
                new BulletTimerPowerupCard(),
                new XpCollectionRadiusPowerupCard(),
                new BulletSpeedPowerupCard()
            };

            Random rnd = new Random();
            cards = cards.OrderBy(x => rnd.Next()).ToList();

            card0 = cards[0];
            card1 = cards[1];
            card2 = cards[2];

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