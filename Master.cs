using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoffEngine
{
    public partial class Master : Form
    {
        private static readonly Timer runTimer = new Timer();
        const int MaxFPS = 2;
        const int milisPerFrame = 1000 / MaxFPS;
        public Master()
        {
            runTimer.Tick += new EventHandler(RunTick);
            runTimer.Interval = milisPerFrame;
            runTimer.Start();
            InitializeComponent();
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Master());
        }

        void RunTick(object sender, EventArgs e)
        {
            runTimer.Stop();
            
            runTimer.Start();
        }
    }
}
