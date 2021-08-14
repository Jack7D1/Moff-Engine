using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoffEngine.Engine
{
    public partial class Master : Form
    {
        public const int maxTPS = 1;
        public const int milisPerTick = 1000 / maxTPS;
        private static readonly Timer runTimer = new Timer();

        private Master()
        {
            runTimer.Tick += new EventHandler(RunTick);
            runTimer.Interval = milisPerTick;
            InitializeComponent();
            Graphics = CreateGraphics();

            ClientSize = new Size(Graphic.ScreenWidth, Graphic.ScreenWidth);
            MaximumSize = new Size(Graphic.ScreenWidth + 16, Graphic.ScreenWidth + 39);
            MinimumSize = new Size(Graphic.ScreenWidth + 16, Graphic.ScreenWidth + 39); //Resize bounds of window to take into account margins and actions bar to get the PERFECT SQUARE.

            GameProc.Init();
            runTimer.Start();
        }

        public static Graphics Graphics { get; private set; }
        public static long Tick { get; private set; } = 0;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Master());
        }

        private static void RunTick(object sender, EventArgs e)
        {
            runTimer.Stop();
            Tick++;
            GameProc.GameTick();
            runTimer.Start();
        }
    }
}