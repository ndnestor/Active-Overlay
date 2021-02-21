using ActiveOverlayForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechDemo {
    class Runner {

        private static bool toggleVisualStudioOverlay;
        private static bool visualStudioKeyIsPressed;

        private static bool toggleYouTubeOverlay;
        private static bool youTubeKeyIsPressed;

        private static bool toggleDesktopOverlay;
        private static bool desktopKeyIsPressed;

        public static void Start() {
            VisualStudioOverlay.Start();
            YouTubeOverlay.Start();
            DesktopOverlay.Start();
        }

        public static void FixedUpdate() {
            //Visual Studio
            if(Keyboard.IsKeyDown(Key.RightCtrl)) {
                if(!visualStudioKeyIsPressed) {
                    if(!toggleVisualStudioOverlay) {
                        Overlay.AddOverlayedProcess(" - Microsoft Visual Studio");
                        YouTubeOverlay.Stop();
                        DesktopOverlay.Stop();
                        VisualStudioOverlay.Restart();
                        toggleVisualStudioOverlay = true;
                    } else {
                        Overlay.RemoveOverlayedProcess(" - Microsoft Visual Studio");
                        VisualStudioOverlay.Stop();
                        toggleVisualStudioOverlay = false;
                    }
                }
                visualStudioKeyIsPressed = true;
            } else {
                visualStudioKeyIsPressed = false;
            }

            //YouTube
            if(Keyboard.IsKeyDown(Key.Delete)) {
                if(!youTubeKeyIsPressed) {
                    if(!toggleYouTubeOverlay) {
                        Overlay.AddOverlayedProcess(" - YouTube");
                        VisualStudioOverlay.Stop();
                        DesktopOverlay.Stop();
                        YouTubeOverlay.Restart();
                        toggleYouTubeOverlay = true;
                    } else {
                        Overlay.RemoveOverlayedProcess(" - YouTube");
                        YouTubeOverlay.Stop();
                        toggleYouTubeOverlay = false;
                    }
                }
                youTubeKeyIsPressed = true;
            } else {
                youTubeKeyIsPressed = false;
            }

            if(toggleYouTubeOverlay) {
                YouTubeOverlay.FixedUpdate();
            }

            //Desktop
            if(Keyboard.IsKeyDown(Key.NumLock)) {
                if(!desktopKeyIsPressed) {
                    if(!toggleDesktopOverlay) {
                        Overlay.AddOverlayedProcess("~Desktop~");
                        VisualStudioOverlay.Stop();
                        YouTubeOverlay.Stop();
                        DesktopOverlay.Restart();
                        toggleDesktopOverlay = true;
                    } else {
                        Overlay.RemoveOverlayedProcess("~Desktop~");
                        DesktopOverlay.Stop();
                        toggleDesktopOverlay = false;
                    }
                }
                desktopKeyIsPressed = true;
            } else {
                desktopKeyIsPressed = false;
            }

            if(toggleDesktopOverlay) {
                DesktopOverlay.FixedUpdate();
            }
        }
    }
}
