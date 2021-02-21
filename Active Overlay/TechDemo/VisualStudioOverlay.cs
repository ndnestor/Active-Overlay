using ActiveOverlayForm;
using ActiveOverlayForm.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDemo {
    class VisualStudioOverlay {

        private static Entity entity;
        private static Transform transform;
        private static TextBox title;
        private static TextBox stringOperations;
        private static TextBox arrays;

        private static readonly Point startingPoint = new Point(1920 / 2, 200);
        private static readonly Point stoppingPoint = new Point(10000, 10000);

        public static void Start() {
            Overlay.SetBackgroundColor(Color.FromArgb(53, 37, 77));

            entity = new Entity("Visual Studio Overlay");
            transform = new Transform(1920 / 2, 200);
            title = new TextBox(" Visual Studio C#\n Quick Reference", 24);
            stringOperations = new TextBox(" String Operations\n str.IndexOf(string val)\n" +
                " str.Replace(string old, string new)\n" +
                " str.Substring(int start, int length)", 18);
            arrays = new TextBox(" Arrays\n type[] arrayName = {val1, val2, val3}\n" +
                " type[] arrayName = new type[size]\n" +
                " type[,] twoDimensionalArray = new type[size1, size2]", 18);
            
            entity.AddComponent(ref transform);
            entity.AddComponent(ref title);
            entity.AddComponent(ref stringOperations);
            entity.AddComponent(ref arrays);

            title.BackgroundColor = Color.Red;
            stringOperations.Offset(0, -300);
            arrays.Offset(0, -600);
        }

        public static void Stop() {
            transform.Position = stoppingPoint;
            Overlay.SetBackgroundColor(Color.Transparent);
        }

        public static void Restart() {
            Overlay.SetBackgroundColor(Color.FromArgb(53, 37, 77));
            transform.Position = startingPoint;
        }
    }
}
