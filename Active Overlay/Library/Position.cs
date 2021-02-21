using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveOverlayForm {
    public class Position {
        public int x;
        public int y;

        public Position() {
            x = y = 0;
        }
        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
