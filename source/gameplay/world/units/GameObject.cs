using Microsoft.Xna.Framework;

namespace topdownShooter {
    public class GameObject : Basic2d {
        public bool remove;
        public float collisionRadius = 0f;
        
        public GameObject(string path, Vector2 pos) : base(path, pos) {
            remove = false;
        }

        public override void Update() {
            base.Update();
        }

        public virtual bool CollidingWith(GameObject obj, float buffer = 0f) {
            return Vector2.Distance(pos, obj.pos) <= collisionRadius + obj.collisionRadius + buffer;
        }

        public virtual void GetHit(Vector2 vel) {}

        public virtual void Remove() {
            remove = true;
        }

        public override void Draw(Vector2 offset) {
            base.Draw(offset);
        }
    }
}