using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace topdownShooter {
    public class GameKeyboard {
        public KeyboardState newKeyboard, oldKeyboard;
        public List<GameKey> pressedKeys = new List<GameKey>(), previousPressedKeys = new List<GameKey>();

        public GameKeyboard() {}

        public virtual void Update() {
            newKeyboard = Keyboard.GetState();
            GetPressedKeys();
        }

        public void UpdateOld() {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<GameKey>();
            for (int i = 0; i < pressedKeys.Count; i++) {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }

        private bool pressedKeysContains(string key) {
            for (int i = 0; i < pressedKeys.Count; i++) {
                if (pressedKeys[i].key == key) {
                    return true;
                }
            }

            return false;
        }

        private bool previousPressedKeysContains(string key) {
            for (int i = 0; i < previousPressedKeys.Count; i++) {
                if (previousPressedKeys[i].key == key) {
                    return true;
                }
            }

            return false;
        }

        public bool GetPress(string key) {
            return pressedKeysContains(key);
        }

        public bool GetPressed(string key) {
            return pressedKeysContains(key) && !previousPressedKeysContains(key);
        }

        public bool GetReleased(string key) {
            return !pressedKeysContains(key) && previousPressedKeysContains(key);
        }

        public virtual void GetPressedKeys() {
            pressedKeys.Clear();
            
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++) {
                pressedKeys.Add(new GameKey(newKeyboard.GetPressedKeys()[i].ToString(), 1));
            }
        }
    }
}