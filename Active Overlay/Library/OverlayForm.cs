using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ActiveOverlayForm.Components;
using Timer = System.Windows.Forms.Timer;

namespace ActiveOverlayForm {
    public partial class OverlayForm : Form {
        //Makes a timer that will call a method every tick
        private readonly Timer ticker = new Timer();
        //The tick rate of the application in milliseconds
        private readonly int tickRate = 17;
        //The recurring method set by the user
        private Action recurringMethod;
        //The starting method set by the user
        private Action startingMethod;
        //Is true when the OnPaint method is currently rendering
        internal bool isPainting;
        //The form controls added by the user of this library
        internal readonly List<Control> userAddedControls = new List<Control>();

        //Methods and properties to allow clicking through this form
        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;

        public OverlayForm() {
            InitializeComponent();

            //Set the form size to cover the working area
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            WindowState = FormWindowState.Maximized;

            //Allows transparent controls (I think)
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //Prevent image artifacts with this graphics box I have no idea why this works the way it does
            graphicsBox.Top = graphicsBox.Left = 0;
            graphicsBox.Width = Screen.FromHandle(Handle).WorkingArea.Width;
            graphicsBox.Height = Screen.FromHandle(Handle).WorkingArea.Height;
            graphicsBox.BackColor = Color.FromArgb(100, 255, 0 , 0);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //Set the form to be transparent. Note that anything of the specified color will become transparent aswell
            BackColor = Color.FromArgb(0, 1, 5);
            TransparencyKey = Color.FromArgb(0, 1, 5);

            //NOTE: Possibly unneeded
            uint initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(Handle, -20, new IntPtr(initialStyle | 0x80000 | 0x20));

            //Call WallpaperForm_Load when form has loaded
            Load += new EventHandler(WallpaperForm_Load);
        }

        //An override for the method that paints the form
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if(ImageRenderer.isRendering) {
                foreach(ImageRenderer.PenInstruction penInstruction in ImageRenderer.penQueue) {
                    //Draw a rectangle
                    if(penInstruction.shape == "Rectangle") {
                        if(!penInstruction.fill) {
                            //No fill rectangle
                            e.Graphics.DrawRectangle(penInstruction.pen, penInstruction.settings[0],
                                                                         penInstruction.settings[1],
                                                                         penInstruction.settings[2],
                                                                         penInstruction.settings[3]);
                        } else {
                            //Rectangle with fill
                            e.Graphics.FillRectangle(new SolidBrush(penInstruction.pen.Color), penInstruction.settings[0],
                                                                                               penInstruction.settings[1],
                                                                                               penInstruction.settings[2],
                                                                                               penInstruction.settings[3]);
                        }
                    }
                }

                ImageRenderer.penQueue.Clear();
                
                isPainting = true;
                foreach(Sprite sprite in ImageRenderer.renderQueue) {
                    e.Graphics.DrawImage(sprite.GetSpriteToRender(), sprite.parentEntity.GetComponent<Transform>().Position);
                }
                isPainting = false;
            }
        }

        //Sets the background color (the color of graphicsBox)
        internal void SetBackgroundColor(Color color, bool mustSet) {
            if(mustSet || ImageRenderer.isRendering) {
                graphicsBox.BackColor = color;
            } else {
                ImageRenderer.backgroundColor = color;
            }
        }

        //Gets the background color (the color of graphicsBox)
        internal Color GetBackgroundColor() {
            return graphicsBox.BackColor;
        }

        //Gets called when this form has laoaded
        private void WallpaperForm_Load(object sender, System.EventArgs e) {
            //Start the refresh timer
            ImageRenderer.StartTimer();

            //Start the update active state timer
            Overlay.StartTimer();

            //Calling the starting method
            startingMethod();

            //Setup for the ticker
            ticker.Tick += new EventHandler(TickEvent);
            ticker.Interval = tickRate;
            ticker.Start();
        }

        //Gets called every tick
        private void TickEvent(Object myObject, EventArgs myEventArgs) {
            //Send key presses to user
            recurringMethod();
        }
        
        //Sets recurringMethod
        public void SetRecurringMethod(Action rMethod) {
            recurringMethod = rMethod;
        }
        //Sets startingMethod
        public void SetStartingMethod(Action sMethod) {
            startingMethod = sMethod;
        }

        //Add a control to this form
        internal void AddControl<T>(out Control control) where T : Control, new() {
            T newControl = new T();
            Controls.Add(newControl);
            userAddedControls.Add(newControl);
            newControl.BringToFront();
            control = newControl;
        }
    }
}
