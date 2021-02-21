using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveOverlayForm {
    public static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static OverlayForm overlayForm;
        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            overlayForm = new OverlayForm();
            Application.Run(overlayForm);
        }

        public static void Start(Action startingMethod, Action recurringMethod) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            overlayForm = new OverlayForm();
            overlayForm.SetStartingMethod(startingMethod);
            overlayForm.SetRecurringMethod(recurringMethod);
            Application.Run(overlayForm);
        }
    }
}
