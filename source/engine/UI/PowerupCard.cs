using Microsoft.Xna.Framework;
using System;

namespace topdownShooter {
    public abstract class PowerupCard {
        private Vector2 pos;
        public Vector2 size;
        private Rectangle2D background;
        public Button button;
        private Vector2 buttonSize;
        private Vector2 buttonPos;
        public event EventHandler powerupSelected;

        public Vector2 Pos {
            get => pos;
            set {
                pos = value;
                Init();
            }
        }

        public PowerupCard() {
            pos = new Vector2(0, 0);
            size = new Vector2(140, 200);

            buttonSize = new Vector2(100, 20);
            buttonPos = new Vector2((size.X - buttonSize.X)/2, size.Y - buttonSize.Y - 5);
            button = new Button(pos + buttonPos, buttonSize, "Select", Powerup() + OnPowerupSelected);
            Init();
        }

        public void Init() {
            buttonSize = new Vector2(100, 20);
            background = new Rectangle2D(pos, pos + size, true, Color.SlateBlue*0.5f);
            button.Pos = pos + new Vector2((size.X - buttonSize.X)/2, size.Y - buttonSize.Y - 5);
        }

        protected abstract Call Powerup();
        protected abstract string PowerupText();

        protected virtual void OnPowerupSelected() {
            if (powerupSelected != null) powerupSelected(this, null);
        }

        public void Update() {
            button.Update();
        }

        protected Player Player() {
            return ((World)GameGlobals.GetWorld()).player;
        }

        public void Draw() {
            background.Draw();
            Utility.DrawText(Globals.spriteBatch, new Vector2(pos.X + size.X/2, pos.Y + 10), "Upgrade", Globals.gameFont, FontAlignment.topCenter);
            Utility.DrawText(Globals.spriteBatch, new Vector2(pos.X + size.X/2, pos.Y + 40), PowerupText(), Globals.gameFont, FontAlignment.topCenter);
            button.Draw();
        }
    }


    public class DamagePowerupCard : PowerupCard {
        public DamagePowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player().Damage++;
            };
        }

        protected override string PowerupText() {
            return "Increase damage\n by 1.";
        }
    }


    public class HpPowerupCard : PowerupCard {
        public HpPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player player = Player();
                player.HpMax++;
                player.Hp++;
            };
        }

        protected override string PowerupText() {
            return "Increase hp by 1.";
        }
    }


    public class AmmoPowerupCard : PowerupCard {
        public AmmoPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player().AmmoMax++;
            };
        }

        protected override string PowerupText() {
            return "Increase max\n ammo by 1.";
        }
    }


    public class ReloadTimerPowerupCard : PowerupCard {
        public ReloadTimerPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player().ReloadTimerMax -= 10;
            };
        }

        protected override string PowerupText() {
            return "Decrease reload\n timer by 10 frames.";
        }
    }


    public class BulletTimerPowerupCard : PowerupCard {
        public BulletTimerPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player().BulletTimerMax -= 2;
            };
        }

        protected override string PowerupText() {
            return "Decrease bullet\n timer by 2 frames.";
        }
    }

    
    public class XpCollectionRadiusPowerupCard : PowerupCard {
        public XpCollectionRadiusPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player().OrbDistanceCollectionRadius += 20f;
            };
        }

        protected override string PowerupText() {
            return "Increase xp\n pickup radius\n by 20 pixels.";
        }
    }


    public class BulletSpeedPowerupCard : PowerupCard {
        public BulletSpeedPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player().BulletSpeed += 1f;
            };
        }

        protected override string PowerupText() {
            return "Increase bullet\n speed by 1.";
        }
    }


    public class SpeedPowerupCard : PowerupCard {
        public SpeedPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                Player().MaxSpeed += 0.5f;
            };
        }

        protected override string PowerupText() {
            return "Increase speed\n by 0.5.";
        }
    }
}