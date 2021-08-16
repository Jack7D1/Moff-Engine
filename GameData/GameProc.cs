using System.Drawing;
using static MoffEngine.Engine.Master;

namespace MoffEngine
{
    internal static class GameProc
    {
        //REQUIRED DECLARATION
        //Change this to set desired screen size, expect long screen refresh times for values over 32.
        public const int desiredvPixelRes = 32;

        //REQUIRED DECLARATION
        //Set this to your desired window name.
        public const string desiredWindowName = "Moth Paint";

        //REQUIRED DECLARATION
        //Set this to your desired BG color.
        public static readonly Color desiredBGColor = Color.Black;

        //REQUIRED DECLARATION
        //Set this to your desired ticks per second, less than or equal to 24 reccomended to prevent window from hanging, set to 0 if GameTick is not utilized, to conserve system resources.
        public static readonly int desiredTPS = 24;

        //REQUIRED DECLARATION
        //Example vPixel matrix when desiredvPixelRes = 4: This would be a small white square on a black background.
        // [[ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]]
        //vPixel table of RGB values that is plotted to the screen. Call Engine.UpdateScreen with this buffer when ready to make a change to the screen.
        private static readonly VPixel[,] frameBuffer = new VPixel[screenVPixelWidth, screenVPixelWidth];

        //REQUIRED DECLARATION
        //Called on every gametick at a rate specified above, current tick is passed into this, starting at 1.
        public static void GameTick(long currentTick)
        {
        }

        //REQUIRED DECLARATION
        //Called on the first tick instead of gametick (tick 0), if desiredTPS is 0, this will still be called when tick 0 would have happened.
        public static void Init()
        {
            for (int y = 0; y < screenVPixelWidth; y++)
                for (int x = 0; x < screenVPixelWidth; x++)
                    frameBuffer[x, y] = new VPixel(desiredBGColor);
            //Reccomended Frame Buffer init code.
        }

        //REQUIRED DECLARATION
        //Called when a mouse button is pressed.
        public static void MouseDown(Point selectedVPixel, MouseButtonState oldMouseState, bool lDown, bool rDown)
        {
        }

        //REQUIRED DECLARATION
        //Called when the mouse is moved over to a new vPixel.
        public static void MouseMove(Point oldSelectedVPixel, Point newSelectedVPixel, MouseButtonState mouseState)
        {
        }

        //REQUIRED DECLARATION
        //Called when a mouse button is released.
        public static void MouseUp(Point selectedVPixel, MouseButtonState oldMouseState, bool lUp, bool rUp)
        {
        }
    }
}