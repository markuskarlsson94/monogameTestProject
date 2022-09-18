using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class GameObject : Basic2d {
        public bool remove;
        public float hitDistance;
        
        public GameObject(string path, Vector2 pos) : base(path, pos) {
            remove = false;
        }

        public override void Update() {
            base.Update();
        }

        public virtual void GetHit(Vector2 vel) {

        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}