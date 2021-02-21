using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsTest.CustomControls {
    //A text box that supports transparency
    class TextBoxT : TextBox {
        public TextBoxT() {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}
