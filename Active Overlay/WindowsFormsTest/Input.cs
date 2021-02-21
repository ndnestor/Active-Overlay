using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsTest {
    class Input {
        //The maximum amount of key presses that will be registered by this program per tick
        private static readonly int maxInputPerTick = 16;
        //An array of keys that have been pressed since the last tick while the program is active
        private static readonly string[] keysPressed = new string[maxInputPerTick];
        //A number representing the first null position of keysPressed. Is -1 if keysPressed is full
        private static int keysPressedCursor = 0;

        //Adds a key to keysPressed
        public static void AddKeyPress(string key) {
            //Add new key to array and move cursor forwards
            if(keysPressedCursor != -1) {
                keysPressed[keysPressedCursor] = key;
                keysPressedCursor++;

                //Stop adding keys once keysPressed is full
                if (keysPressedCursor == keysPressed.Length) {
                    keysPressedCursor = -1;
                }
            }
        }

        //Clears keysPressed
        public static void ClearKeysPressed() {
            Array.Clear(keysPressed, 0, keysPressed.Length);
            keysPressedCursor = 0;
        }

        //Returns keysPressed
        public static string[] GetKeysPressed() {
            return keysPressed;
        }

        //Return true if key is in keysPressed
        public static bool GetKey(string key) {
            foreach(string currKey in keysPressed) {
                if(currKey == key) {
                    return true;
                }
            }
            return false;
        }
    }
}
