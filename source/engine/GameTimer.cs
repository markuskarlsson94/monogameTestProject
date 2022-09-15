using System;
using System.Xml.Linq;

//namespace testProjectPrompt {
namespace testProject {
    public class GameTimer {
        public bool expired;
        protected int mSec;
        protected TimeSpan timer = new TimeSpan();
        
        public GameTimer(int m) {
            expired = false;
            mSec = m;
        }

        public GameTimer(int m, bool startLoaded) {
            expired = startLoaded;
            mSec = m;
        }

        public int MSec {
            get { return mSec; }
            set { mSec = value; }
        }
        
        public int Timer {
            get { return (int)timer.TotalMilliseconds; }
        }

        public void UpdateTimer() {
            timer += Globals.gameTime.ElapsedGameTime;
        }

        public void UpdateTimer(float speed) {
            timer += TimeSpan.FromTicks((long)(Globals.gameTime.ElapsedGameTime.Ticks * speed));
        }

        public virtual void AddToTimer(int mSec) {
            timer += TimeSpan.FromMilliseconds((long)(mSec));
        }

        public bool HasExpired() {
            return timer.TotalMilliseconds >= mSec || expired;
        }

        public void Reset() {
            timer = timer.Subtract(new TimeSpan(0, 0, mSec/60000, mSec/1000, mSec % 1000));
            
            if (timer.TotalMilliseconds < 0) {
                timer = TimeSpan.Zero;
            }

            expired = false;
        }

        public void Reset(int newTimer) {
            timer = TimeSpan.Zero;
            MSec = newTimer;
            expired = false;
        }

        public void ResetToZero() {
            timer = TimeSpan.Zero;
            expired = false;
        }

        public virtual XElement ReturnXML() {
            return new XElement("Timer", new XElement("mSec", mSec), new XElement("timer", Timer));
        }

        public void SetTimer(TimeSpan time) {
            timer = time;
        }

        public virtual void SetTimer(int mSec) {
            timer = TimeSpan.FromMilliseconds((long)(mSec));
        }
    }
}
