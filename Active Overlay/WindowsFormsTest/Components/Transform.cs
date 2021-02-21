using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Size = System.Windows.Size;

namespace ActiveOverlayForm.Components {
    public class Transform : Component {
        //The position of an entity
        private PointF position;
        public Point Position {
            get { return Point.Round(position); } //TODO: Return a Point (not a PointF)
            set {
                position = value;
                foreach(Component component in parentEntity.GetComponents()) {
                    if(component.type == "TextBox") {
                        ((TextBox)component).UpdateControlPosition();
                    }
                }
            }
        }
        //The size multiplier of the entity in pixels
        public Size scale;
        //TODO: Rotation

        //Constructor
        public Transform(float xPosition, float yPosition) : base("Transform") {
            position = new PointF(xPosition, yPosition);
            scale = new Size(1, 1);
        }

        //Changes the position
        public void Move(int x, int y, MoveMode moveMode) {
            if(moveMode == MoveMode.Teleport) {
                Position = new Point(x, y);
            } else {
                Position = new Point(Position.X + x, Position.Y + y);
            }
        }

        public void Move(Point point, MoveMode moveMode) {
            Move(point.X, point.Y, moveMode);
        }

        public enum MoveMode {
            Teleport,
            Translate
        }
    }
}
