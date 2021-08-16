using System.Collections.Generic;
using System.Drawing;
using static MoffEngine.Engine.Master;

namespace MoffEngine
{
    internal static class GameProc
    {
        //Change this to set desired screen size, the engine is NOT meant for values over 32!!
        public const int desiredvPixelRes = 16;

        //Set this to your desired window name.
        public const string desiredWindowName = "Moth Paint";

        //Set this to your desired BG color
        public static readonly Color desiredBGColor = Color.Black;

        //Set this to your desired ticks per second, less than or equal to 24 reccomended to prevent window from hanging, set to 0 if GameTick is not utilized, to conserve system resources.
        public static readonly int desiredTPS = 0; //REQUIRED DECLARATION

        //REQUIRED DECLARATION

        private static readonly List<Color> colors = new List<Color> { Color.Black, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet, Color.White };

        //REQUIRED DECLARATION
        //Example vPixel matrix when vPixelScreenWidth = 4: This would be a small white square on a black background.
        // [[ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]]
        //vPixel table of RGB values that is plotted to the screen. Call Engine.UpdateScreen with this buffer when ready to make a change to the screen.
        private static readonly VPixel[,] frameBuffer = new VPixel[screenVPixelWidth, screenVPixelWidth];

        private static int colorIndex = 0;

        //REQUIRED DECLARATION
        private static bool fillInProgress = false;

        public static void GameTick(long currentTick) //REQUIRED DECLARATION
        {
            fillInProgress = false;
        }

        public static void Init() //REQUIRED DECLARATION
        {
            for (int y = 0; y < screenVPixelWidth; y++)
                for (int x = 0; x < screenVPixelWidth; x++)
                    frameBuffer[x, y] = new VPixel(desiredBGColor);
            //Reccomended Frame Buffer init code.
            frameBuffer[0, 0].SetRGB(colors[colorIndex].R, colors[colorIndex].G, colors[colorIndex].B);

            frameBuffer[0, 1].SetColor(Color.DimGray);
            frameBuffer[1, 0].SetColor(Color.DimGray);
            frameBuffer[1, 1].SetColor(Color.DimGray);
            UpdateScreen(frameBuffer);
        }

        public static void MouseDown(Point selectedVPixel, MouseButtonState oldMouseState, bool lDown, bool rDown) //REQUIRED DECLARATION
        {
            if (lDown && !fillInProgress)
            {
                PlotColor(colors[colorIndex], selectedVPixel.X, selectedVPixel.Y);
            }
            else if (rDown && !oldMouseState.R)
            {
                colorIndex++;
                if (colorIndex >= colors.Count)
                    colorIndex = 0;

                frameBuffer[0, 0].SetColor(colors[colorIndex]);
                UpdateScreen(frameBuffer);
            }
        }

        public static void MouseMove(Point oldSelectedVPixel, Point newSelectedVPixel, MouseButtonState mouseState) //REQUIRED DECLARATION
        {
            if (mouseState.L && oldSelectedVPixel != newSelectedVPixel && !(newSelectedVPixel.X == 0 && newSelectedVPixel.Y == 0))
            {
                PlotColor(colors[colorIndex], newSelectedVPixel.X, newSelectedVPixel.Y);
            }
        }

        public static void MouseUp(Point selectedVPixel, MouseButtonState oldMouseState, bool lUp, bool rUp) //REQUIRED DECLARATION
        {
        }

        private static void PlotColor(Color color, int x, int y)
        {
            if (x == 0 && y == 0 && !fillInProgress)
            {
                fillInProgress = true;
                for (y = 0; y < screenVPixelWidth; y++)
                    for (x = 0; x < screenVPixelWidth; x++)
                        if (x + y > 0)
                            PlotColor(color, x, y);
            }
            else if (x > 1 || y > 1)
            {
                frameBuffer[x, y].SetColor(color);
                UpdateScreen(frameBuffer);
            }
        }
    }
}