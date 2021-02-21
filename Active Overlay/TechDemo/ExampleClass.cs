using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Size = System.Windows.Size;
using System.Windows.Input;
using ActiveOverlayForm;
using ActiveOverlayForm.Components;

namespace TechDemo {
    class ExampleClass {

        private Entity myEntity;
        private Transform transform;
        //private Sprite sprite;
        private TextBox textBox;
        private bool toggleOverlay;
        private bool enterIsPressed;

        public void StartingMethod() {
            myEntity = new Entity("My Entity");
            transform = new Transform(1920 / 2, 1080 / 2);
            //sprite = new Sprite(new Bitmap(@"C:\Users\user\Desktop\New Piskel.bmp"));
            textBox = new TextBox("Hello, World!\nI am a text box", 12);
            myEntity.AddComponent(ref transform);
            myEntity.AddComponent(ref textBox);
            //myEntity.AddComponent(ref sprite);

            Overlay.SetBackgroundColor(Color.FromArgb(20, 0, 255, 0));
        }

        //double i = 0.1;
        public void RecurringMethod() {
            if(Keyboard.IsKeyDown(Key.Up)) {
                if(!enterIsPressed) {
                    //Do something when enter/return is pressed
                    if(!toggleOverlay) {
                        Overlay.AddOverlayedProcess(" - Microsoft Visual Studio");
                        toggleOverlay = true;
                    } else {
                        Overlay.RemoveOverlayedProcess(" - Microsoft Visual Studio");
                        toggleOverlay = false;
                    }
                }
                enterIsPressed = true;
                textBox.Text = "enter is pressed";
            } else {
                enterIsPressed = false;
                textBox.Text = "enter is not pressed";
            }


            //Setting position example
            //transform.position = new Point(i, i);

            //Adding rotation example
            //sprite.Rotate((float)i);

            //Setting scale example
            /*transform.scale = new Size(i, i);
            i += 0.01;
            if(i >= 1.5) {
                i = 0.1;
            }
            ImageRenderer.PenDraw("Rectangle", new Pen(Color.Green), new int[] { 0, 0, 100, 100 }, true)*/;
        }
    }
}
