using Microsoft.Xna.Framework;
using System;

namespace topdownShooter {
    public abstract class PowerupCard {
        public Vector2 pos;
        public Vector2 size;
        private Rectangle2D background;

        public Button button;
        private Vector2 buttonSize;
        private Vector2 buttonPos;

        public event EventHandler powerupSelected;

        public PowerupCard(Vector2 pos) {
            this.pos = pos;
            size = new Vector2(140, 200);
            background = new Rectangle2D(pos, pos + size, true, Color.SlateBlue*0.5f);

            buttonSize = new Vector2(100, 20);
            buttonPos = new Vector2((size.X - buttonSize.X)/2, size.Y - buttonSize.Y - 5);
            button = new Button(pos + buttonPos, buttonSize, "Select", Powerup() + OnPowerupSelected);
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
        public DamagePowerupCard(Vector2 pos) : base(pos) {}

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
        public HpPowerupCard(Vector2 pos) : base(pos) {}

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
        public AmmoPowerupCard(Vector2 pos) : base(pos) {}

        protected override Call Powerup() {
            return () => {
                Player().AmmoMax++;
            };
        }

        protected override string PowerupText() {
            return "Increase ammo\n by 1.";
        }
    }


    public class ReloadTimerPowerupCard : PowerupCard {
        public ReloadTimerPowerupCard(Vector2 pos) : base(pos) {}

        protected override Call Powerup() {
            return () => {
                Player().ReloadTimerMax -= 10;
            };
        }

        protected override string PowerupText() {
            return "Decrease reload\n timer by 10 frames.";
        }
    }
}