using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class GameObject : Basic2d {
        public bool remove;
        public float hitDistance;
        
        public GameObject(string path, Vector2 pos, Vector2 size) : base(path, pos, size) {
            remove = false;
        }

        public override void Update() {
            base.Update();
        }

        public virtual void GetHit() {

        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}