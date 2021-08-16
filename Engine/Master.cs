using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoffEngine.Engine
{
    public partial class Master : Form
    {
        //Width screen in vPixels
        public const int screenVPixelWidth = screenPixelWidth / vPixelWidth;

        public static readonly Random rand = new Random((int)DateTime.Now.Ticks);

        public MouseButtonState mouseButtonState;

        public Point mousevPixelCoords;

        //Width of screen in pixels
        private const int screenPixelWidth = 512;

        //Width of vPixel in screen pixels
        private const int vPixelWidth = screenPixelWidth / GameProc.desiredvPixelRes;

        private static Graphics graphics;
        private readonly Timer runTimer;

        private Master()
        {
            InitializeComponent();

            ClientSize = new Size(screenPixelWidth, screenPixelWidth);
            MaximumSize = new Size(screenPixelWidth + 16, screenPixelWidth + 39);
            MinimumSize = new Size(screenPixelWidth + 16, screenPixelWidth + 39); //Resize bounds of window to take into account margins and actions bar to get the PERFECT SQUARE.
            Text = GameProc.desiredWindowName;
            BackColor = GameProc.desiredBGColor;
            graphics = CreateGraphics();
            mousevPixelCoords = new Point();
            mouseButtonState = new MouseButtonState(false, false);
            runTimer = new Timer();
            runTimer.Tick += new EventHandler(RunTick);
            if (GameProc.desiredTPS > 0)
            {
                runTimer.Interval = 1000 / GameProc.desiredTPS;
            }
            else
            {
                runTimer.Interval = 1;
                Tick = -1;
            }
            runTimer.Start();
        }

        public long Tick { get; private set; } = 0;

        //Pass a 2D VPixel array of the proper dimensions (vPixelScreenWidth x vPixelScreenWidth) to this function. Returns true if screen updated successfully, false otherwise.
        public static bool UpdateScreen(VPixel[,] newFrame)
        {
            if (newFrame.GetLength(0) != screenVPixelWidth || newFrame.GetLength(1) != screenVPixelWidth)
                return false;

            SolidBrush brush = new SolidBrush(Color.Transparent);

            for (int y = 0; y < screenVPixelWidth; y++)
                for (int x = 0; x < screenVPixelWidth; x++)
                {
                    brush.Color = Color.FromArgb(newFrame[x, y].R, newFrame[x, y].G, newFrame[x, y].B);
                    graphics.FillRectangle(brush, vPixelWidth * x, vPixelWidth * y, vPixelWidth, vPixelWidth);
                }

            brush.Dispose();
            return true;
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Master());
        }

        private void Master_MouseDown(object sender, MouseEventArgs e)
        {
            bool L = e.Button.HasFlag(MouseButtons.Left), R = e.Button.HasFlag(MouseButtons.Right);

            if (L || R)
                GameProc.MouseDown(mousevPixelCoords, mouseButtonState, L, R);

            if (L)
                mouseButtonState.L = true;
            else if (R)
                mouseButtonState.R = true;
        }

        private void Master_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.X > (screenPixelWidth - 1) || e.Y < 0 || e.Y > (screenPixelWidth - 1))
                return;
            Point nextP = new Point(e.X / vPixelWidth, e.Y / vPixelWidth);
            GameProc.MouseMove(mousevPixelCoords, nextP, mouseButtonState);
            mousevPixelCoords = nextP;
        }

        private void Master_MouseUp(object sender, MouseEventArgs e)
        {
            bool L = e.Button.HasFlag(MouseButtons.Left), R = e.Button.HasFlag(MouseButtons.Right);

            if (L || R)
                GameProc.MouseUp(mousevPixelCoords, mouseButtonState, L, R);

            if (L)
                mouseButtonState.L = false;
            else if (R)
                mouseButtonState.R = false;
        }

        private void RunTick(object sender, EventArgs e)
        {
            runTimer.Stop();
            if (Tick <= 0)
            {
                GameProc.Init();
                if (Tick < 0)
                {
                    runTimer.Dispose();
                    return;
                }
            }
            else
            {
                GameProc.GameTick(Tick);
            }
            Tick++;
            runTimer.Start();
        }

        public struct MouseButtonState
        {
            public bool L, R;

            public MouseButtonState(bool l, bool r)
            {
                L = l;
                R = r;
            }
        }

        //Virtual Pixel, largely a concept made to decrease the resolution of a high resolution monitor.
        public class VPixel
        {
            public byte R, G, B;

            public VPixel(byte redVal = 0, byte grnVal = 0, byte bluVal = 0)
            {
                SetRGB(redVal, grnVal, bluVal);
            }

            public VPixel(Color color)
            {
                SetColor(color);
            }

            public void GetRGB(out byte redVal, out byte grnVal, out byte bluVal)
            {
                redVal = R;
                grnVal = G;
                bluVal = B;
            }

            public void SetColor(Color newcolor)
            {
                R = newcolor.R;
                G = newcolor.G;
                B = newcolor.B;
            }

            public void SetRGB(byte redVal, byte grnVal, byte bluVal)
            {
                R = redVal;
                G = grnVal;
                B = bluVal;
            }
        }
    }
}