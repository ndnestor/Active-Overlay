using ActiveOverlayForm;
using ActiveOverlayForm.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo {
    class YouTubeOverlay {

        private static Entity entity;
        private static Transform transform;
        private static TextBox time;

        private static readonly Point startingPoint = new Point(1920 - 100, 1080 - 100);
        private static readonly Point stoppingPoint = new Point(10000, 10000);

        public static void Start() {
            Overlay.SetBackgroundColor(Color.Transparent);

            entity = new Entity("YouTube Overlay");
            transform = new Transform(1920 - 100, 1080 - 100);
            time = new TextBox();

            entity.AddComponent(ref transform);
            entity.AddComponent(ref time);
        }

        public static void FixedUpdate() {
            time.Text = DateTime.Now.ToString("h:mm:s");
        }

        public static void Stop() {
            transform.Position = stoppingPoint;
            Overlay.SetBackgroundColor(Color.Transparent);
        }

        public static void Restart() {
            Overlay.SetBackgroundColor(Color.Transparent);
            transform.Position = startingPoint;
        }
    }
}
