using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveOverlayForm {
    public class Overlay {
        //The process(es) that will be overlayed by this program
        //private static readonly string[] overlayedProcessNames = new string[32];
        private static readonly List<string> overlayedProcessNames = new List<string>();
        //The handle of the currently overlayed process. Null if there is not one NOTE: possibly inaccurate comment
        private static IntPtr foregroundWindow;
        //The background color set by the user
        private static Color backgroundColor;
        //The most characters that a window can have
        private static readonly int maxWindowTitleLength = 256;
        //Timer that checks the active state of the overlay and changes it if needed
        private static readonly Timer updateTimer = new Timer();
        //The frequency at which the updateTimer ticks in milliseconds
        private static readonly int updateRate = 100;
        //True when the overlay should be set to be in the foreground
        private static bool shouldBeForeground;
        //True when the overlay should have been set to be in the foreground on the last tick
        private static bool shouldBeForegroundPrior;
        //The task to run UpdateShouldBeForeground
        private static Task updateTask;

        //Gets the active window (more literally the foremost window)
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        //Sets the active window
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        //Gets the active window's name
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        //Sets the background color
        public static void SetBackgroundColor(Color color) {
            Program.overlayForm.SetBackgroundColor(color, false);
            backgroundColor = color;
        }

        //Gets the background color
        public static Color GetBackgroundColor() {
            return backgroundColor;
        }
        
        //Start timer to update the foreground window every updateRate ms
        internal static void StartTimer() {
            updateTimer.Tick += new EventHandler(UpdateForegroundWindow);
            updateTimer.Interval = updateRate;
            updateTimer.Start();
        }

        private static int counter = 0; //TODO: Put this variable at the top of class
        //Makes the appriopriate window the foreground
        private async static void UpdateForegroundWindow(Object myObject, EventArgs myEventArgs) {

            if(Control.MouseButtons == MouseButtons.Left || counter >= 10) {
                if(counter >= 10) {
                    counter = 0;
                }

                foregroundWindow = GetForegroundWindow();
                //Console.WriteLine(foregroundWindow);
                if(foregroundWindow.ToInt32() == 65792) {
                    Console.WriteLine("Desktop");
                }

                updateTask = Task.Run(() => UpdateShouldBeForeground());
                await Task.WhenAll(updateTask);

                if(shouldBeForeground && !shouldBeForegroundPrior) {
                    Program.overlayForm.TopMost = true; //TODO: Figure out why this line causes poor graphics performance
                    SetForegroundWindow(Program.overlayForm.Handle); //Note sure whether this line is needed
                    SetForegroundWindow(foregroundWindow);
                    ImageRenderer.StopRendering();
                } else if(!shouldBeForeground && shouldBeForegroundPrior) {
                    Program.overlayForm.TopMost = false;
                    SetForegroundWindow(foregroundWindow);
                    ImageRenderer.StartRendering();
                }
                shouldBeForegroundPrior = shouldBeForeground;
            }
            counter++;
        }
        
        //Updates the shouldBeForeground variable
        private static void UpdateShouldBeForeground() {
            //Handles desktop overlay
            if(overlayedProcessNames.Contains("~Desktop~") && foregroundWindow.ToInt32() == 65792) {
                shouldBeForeground = false;
                return;
            }

            StringBuilder Buff = new StringBuilder(maxWindowTitleLength);
            GetWindowText(foregroundWindow, Buff, maxWindowTitleLength);
            foreach(string processName in overlayedProcessNames) {
                if(Buff.ToString().Contains(processName)) {
                    shouldBeForeground = false; //NOTE: I'm not sure why this words
                    return;                    //It would seem that shouldBeForeground should equal true here
                }
            }
            shouldBeForeground = true; //NOTE: See prior note. This also seems inverted
        }

        //Adds the given process to overlayedProcesses. If there is not enough space in the array, the method returns false
        public static void AddOverlayedProcess(string processName) {
            if(!overlayedProcessNames.Contains(processName)) {
                overlayedProcessNames.Add(processName);
            }
        }

        //Removes the given process to overlayedProcesses. If the process isn't there, the method returns false
        public static bool RemoveOverlayedProcess(string processName) {
            if(!overlayedProcessNames.Contains(processName)) {
                return false;
            }
            overlayedProcessNames.Remove(processName);
            return true;
        }
    }
}
