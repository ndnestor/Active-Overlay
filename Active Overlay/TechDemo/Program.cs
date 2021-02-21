using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechDemo {
     static class Program {

        //private static ExampleClass myExampleClass;

        [STAThread]
        private static void Main() {
            //myExampleClass = new ExampleClass();
            //ActiveOverlayForm.Program.Start(myExampleClass.StartingMethod, myExampleClass.RecurringMethod);

            ActiveOverlayForm.Program.Start(Runner.Start, Runner.FixedUpdate);
        }
    }
}
