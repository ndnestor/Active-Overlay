using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsTest.CustomControls;

namespace ActiveOverlayForm.Components {
    public class TextBox : Component {
        //The text to be displayed
        private string text;
        public string Text {
            get { return text; }
            set {
                if(text != value) {
                    text = value;
                    control.Text = "";
                    int priorIndex = -1;
                    int lastNewline = 0;
                    foreach (char character in value) {
                        if(priorIndex != -1) {
                            if(character == '\n') {
                                ((System.Windows.Forms.TextBox)control).AppendText(value.Substring(lastNewline, priorIndex + 1 - lastNewline));
                                ((System.Windows.Forms.TextBox)control).AppendText(Environment.NewLine);
                                lastNewline = priorIndex + 2;
                            }
                        }
                        priorIndex++;
                    }
                    ((System.Windows.Forms.TextBox)control).AppendText(value.Substring(lastNewline));
                    Size size = TextRenderer.MeasureText(control.Text, control.Font);
                    control.Width = size.Width;
                    control.Height = size.Height + (int)(((System.Windows.Forms.TextBox)control).Font.SizeInPoints) / 3;
                    textBasedXOffset = (int)(control.Width / 2.5) * 2 + 70; //TODO: Figure out why I need to add 70 here
                    textBasedYOffset = (int)(control.Height / 2.5) * 2;
                }
            }
        }
        //The size of the text
        private float fontSize;
        public float FontSize {
            get { return fontSize; }
            set {
                fontSize = value;
                control.Font = new Font(control.Font.FontFamily, fontSize);
                Size size = TextRenderer.MeasureText(control.Text, control.Font);
                control.Width = size.Width;
                control.Height = size.Height + (int)(((System.Windows.Forms.TextBox)control).Font.SizeInPoints) / 3;
            }
        }
        //The font of the text
        private Font font;
        public Font Font {
            get { return font; }
            set {
                font = value;
                control.Font = value;
            }
        }
        //The background color of this textbox
        private Color backgroundColor;
        public Color BackgroundColor {
            get { return backgroundColor; }
            set {
                if(value == Color.Transparent) {
                    backgroundColor = Overlay.GetBackgroundColor();
                    control.BackColor = Overlay.GetBackgroundColor();
                } else {
                    Console.WriteLine(value);
                    backgroundColor = value;
                    control.BackColor = value;
                }
            }
        }
        //The transform component of the parent entity of this
        private Transform transform;
        //The Control object for this component
        private Control control;
        //The amount of pixels that this text box is offset from the transform by (set by user of library)
        private int xOffset;
        private int yOffset;
        //The amount of pixels that this text box is offset from the transform by (set by library)
        private int textBasedXOffset;
        private int textBasedYOffset;
        //A timer to check for the nullness of parentEntity
        private readonly Timer checkForParentTimer = new Timer();
        //The time in milliseconds between checks for a parent entity
        private readonly int checkForParentInterval = 100;

        //Constructors
        public TextBox() : base("TextBox") {
            TextBoxSetup();
            Text = "";
            FontSize = 12;
        }
        public TextBox(string text, float fontSize) : base("TextBox") {
            TextBoxSetup();
            Text = text;
            FontSize = fontSize;
        }

        private void TextBoxSetup() {
            Program.overlayForm.AddControl<TextBoxT>(out control);
            ((System.Windows.Forms.TextBox)control).Multiline = true;
            ((System.Windows.Forms.TextBox)control).WordWrap = true;
            ((System.Windows.Forms.TextBox)control).ScrollBars = ScrollBars.None;

            if(parentEntity != null) {
                transform = parentEntity.GetComponent<Transform>();
            } else {
                checkForParentTimer.Tick += new EventHandler(CheckForParentEntity);
                checkForParentTimer.Interval = checkForParentInterval;
                checkForParentTimer.Start();
            }
        }

        private void CheckForParentEntity(Object myObject, EventArgs myEventArgs) {
            if(parentEntity != null) {
                transform = parentEntity.GetComponent<Transform>();
                UpdateControlPosition();
                checkForParentTimer.Stop();
            }
        }

        //Updates the corresponding text box control to the be at the correct position
        internal void UpdateControlPosition() {
            control.Location = new Point((int)transform.Position.X + xOffset - textBasedXOffset,
                                         (int)transform.Position.Y - yOffset - textBasedYOffset);
        }

        public void Offset(int x, int y) {
            xOffset = x;
            yOffset = y;
            if(transform != null) {
                UpdateControlPosition();
            }
        }
    }
}
