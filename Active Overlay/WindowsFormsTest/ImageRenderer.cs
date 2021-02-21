using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActiveOverlayForm.Components;

namespace ActiveOverlayForm {
    public class ImageRenderer {
        //Sprites to be rendered
        internal static readonly List<Sprite> renderQueue = new List<Sprite>();
        //Images to be rendered using Pen object
        internal static readonly List<PenInstruction> penQueue = new List<PenInstruction>();
        //A list of all sprites
        internal static readonly List<Sprite> sprites = new List<Sprite>();
        //The maximum frame rate measured in milliseconds per frame
        private static readonly int refreshRate = 17;
        //Timer used to render the overlay every refreshRate milliseconds
        private static readonly System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
        //Determines whether the overlay should render anything
        internal static bool isRendering = true;
        //The background color of the form (literally the graphicsBox color)
        internal static Color backgroundColor;
        //The task responsible for updating the render queue
        //private static Task renderTask;


        //Start the refresh timer
        internal static void StartTimer() {
            refreshTimer.Tick += new EventHandler(CreateRenderTask);
            refreshTimer.Interval = refreshRate;
            refreshTimer.Start();
        }

        //Start rendering the overlay
        internal static void StartRendering() {
            isRendering = true;
            Program.overlayForm.SetBackgroundColor(backgroundColor, true);

            foreach(Control control in Program.overlayForm.userAddedControls) {
                control.Visible = true;
            }
        }

        //Stop rendering the overlay
        internal static void StopRendering() {
            isRendering = false;
            backgroundColor = Program.overlayForm.GetBackgroundColor();
            Program.overlayForm.SetBackgroundColor(Color.Transparent, true);

            foreach(Control control in Program.overlayForm.userAddedControls) {
                control.Visible = false;
            }
        }

        //Create a thread to run UpdateRenderQueue
        internal static void CreateRenderTask(Object myObject, EventArgs myEventArgs) {
            //TODO: Finish this part to support multithreading
            /*if(renderTask != null) {
                await Task.WhenAll(renderTask);
            }*/

            Program.overlayForm.Refresh();
            //renderTask = Task.Run(() => UpdateRenderQueue(sprites, renderQueue, Program.overlayForm));
            UpdateRenderQueue(sprites, renderQueue, Program.overlayForm);
        }

        //Update renderQueue with sprites that can be rendered
        internal static void UpdateRenderQueue(List<Sprite> sprites, List<Sprite> renderQueue, OverlayForm overlayForm) {
            renderQueue.Clear();
            foreach(Sprite sprite in sprites) {
                //Determine if conditions are met for the sprite to be rendered
                if(sprite.parentEntity != null
                   && sprite.parentEntity.GetComponent<Transform>() != null
                   && sprite.GetSprite() != null) {

                    while(overlayForm.isPainting) {
                        Thread.Sleep(100);
                    }

                    sprite.SetSpriteToRender();
                    
                    while(overlayForm.isPainting) {
                        Thread.Sleep(100);
                    }
                    renderQueue.Add(sprite);
                }
            }
        }

        //Holds information about GDI+ drawing instructions with pen
        internal class PenInstruction {
            public string shape;
            public Pen pen;
            public int[] settings;
            public bool fill;

            //Constructor
            public PenInstruction(string shape, Pen pen, int[] settings, bool fill) {
                this.shape = shape;
                this.pen = pen;
                this.settings = settings;
                this.fill = fill;
            }
        }

        //Adds a PenInstruction to penQueue
        public static void PenDraw(string shape, Pen pen, int[] settings, bool fill) {
            penQueue.Add(new PenInstruction(shape, pen, settings, fill));
        }
    }
}
