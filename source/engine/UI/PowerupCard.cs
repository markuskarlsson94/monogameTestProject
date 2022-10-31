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
        protected Player player;

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
            player = ((World)GameGlobals.GetWorld()).player;

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

        public virtual bool Valid() {
            return true;
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
                player.Damage++;
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
                player.Hp++;
            };
        }

        protected override string PowerupText() {
            return "Increase hp by 1.";
        }


        public override bool Valid() {
            return player.Hp < player.HpMax;
        }
    }


    public class MaxHpPowerupCard : PowerupCard {
        public MaxHpPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.HpMax++;
            };
        }

        protected override string PowerupText() {
            return "Increase max\n hp by 1.";
        }
    }


    public class AmmoPowerupCard : PowerupCard {
        public AmmoPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.AmmoMax++;
            };
        }

        protected override string PowerupText() {
            return "Increase max\n ammo by 1.";
        }
    }


    public class ReloadTimerPowerupCard : PowerupCard {
        private int amount = 10;

        public ReloadTimerPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.ReloadTimerMax -= amount;
            };
        }

        protected override string PowerupText() {
            return "Decrease reload\n timer by 10 frames.";
        }

        public override bool Valid() {
            return player.ReloadTimerMax - amount > 0;
        }
    }


    public class BulletTimerPowerupCard : PowerupCard {
        private int amount = 2;

        public BulletTimerPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.BulletTimerMax -= amount;
            };
        }

        protected override string PowerupText() {
            return "Decrease bullet\n timer by 2 frames.";
        }


        public override bool Valid() {
            return player.BulletTimerMax - amount > 0;
        }
    }

    
    public class XpCollectionRadiusPowerupCard : PowerupCard {
        public XpCollectionRadiusPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.OrbDistanceCollectionRadius += 30f;
            };
        }

        protected override string PowerupText() {
            return "Increase xp\n pickup radius\n by 30 pixels.";
        }
    }


    public class XpLifetimePowerupCard : PowerupCard {
        public XpLifetimePowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.OrbLifetime += 120;
            };
        }

        protected override string PowerupText() {
            return "Increase xp\n lifetime by 2s.";
        }
    }


    public class BulletSpeedPowerupCard : PowerupCard {
        public BulletSpeedPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.BulletSpeed += 1f;
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
                player.MaxSpeed += 0.5f;
            };
        }

        protected override string PowerupText() {
            return "Increase speed\n by 0.5.";
        }
    }


    public class EnemyHitsPowerupCard : PowerupCard {
        public EnemyHitsPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.EnemyHitsMax++;
            };
        }

        protected override string PowerupText() {
            return "Increase enemy\n hits by 1";
        }
    }


    public class BulletAmountPowerupCard : PowerupCard {
        public BulletAmountPowerupCard() : base() {}

        protected override Call Powerup() {
            return () => {
                player.BulletAmount++;
            };
        }

        protected override string PowerupText() {
            return "Increase bullet\n amount by 1";
        }
    }
}