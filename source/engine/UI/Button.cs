using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class Button {
        private Vector2 pos;
        public Vector2 size;
        public string text;
        private Rectangle2D rect;
        public Color color, activeColor;
        private float activeValue;
        private Call callback;

        public Vector2 Pos {
            get => pos;
            set {
                pos = value;
                rect.P0 = pos;
                rect.P1 = pos + size;
            }
        }

        public Button(Vector2 pos, Vector2 size, string text, Call callback = null, Color color = default(Color), Color activeColor = default(Color)) {
            activeValue = 0;
            rect = new Rectangle2D(pos, pos + size, true, color);
            
            this.pos = pos;
            this.size = size;
            this.text = text;
            this.callback = callback;

            if (color == default(Color)) this.color = Color.LightSlateGray;
            if (activeColor == default(Color)) this.activeColor = Color.Teal;
        }

        public virtual void Update() {
            if (Active()) {
                if (Globals.mouse.LeftClick()) {
                    if (callback != null) callback();
                }

                activeValue += 0.1f;
            } else {
                activeValue -= 0.1f;
            }

            activeValue = MathHelper.Clamp(activeValue, 0, 1);
        }

        private bool Active() {
            float mx = Globals.mouse.newMousePos.X;
            float my = Globals.mouse.newMousePos.Y;

            return (mx >= pos.X && mx <= pos.X + size.X && my >= pos.Y && my <= pos.Y + size.Y);
        }

        public virtual void Draw() {
            rect.Color = Color.Lerp(color, activeColor, activeValue);
            rect.Draw();
            Utility.DrawText(Globals.spriteBatch, pos + size/2, text, Globals.gameFont, FontAlignment.middleCenter, Color.Black);
        }
    }
}