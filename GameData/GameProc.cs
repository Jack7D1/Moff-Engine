using MoffEngine.Engine;
using System.Drawing;
using System.Windows.Forms;
using static MoffEngine.Engine.Master;

namespace MoffEngine
{
    internal static class GameProc
    {
        //Change this to set desired screen size, the engine is NOT meant for values over 32!!
        public const int desiredvPixelRes = 32;

        //Example vPixel matrix when vPixelScreenWidth = 4: This would be a small white square on a black background.
        // [[ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]]
        //vPixel table of RGB values that is plotted to the screen. Call Engine.UpdateScreen with this buffer when ready to make a change to the screen.
        private static readonly VPixel[,] frameBuffer = new VPixel[screenvPixel, screenvPixel];

        public static void GameTick(long currentTick)
        {

        }

        public static void Init()
        {
            for (int y = 0; y < screenvPixel; y++)
                for (int x = 0; x < screenvPixel; x++)
                    frameBuffer[x, y] = new VPixel();
            //Reccomended Frame Buffer init code.
            UpdateScreen(frameBuffer);
        }

        
        public static void MouseUp(Point selectedVPixel, MouseButtonState oldMouseState, bool lUp, bool rUp)
        {

        }

        public static void MouseDown(Point selectedVPixel, MouseButtonState oldMouseState, bool lDown, bool rDown)
        {

        }

        public static void MouseMove(Point oldSelectedVPixel, Point newSelectedVPixel)
        {

        }

    }
}