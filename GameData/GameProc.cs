using System.Collections.Generic;
using System.Drawing;
using static MoffEngine.Engine.Master;

namespace MoffEngine
{
    internal static class GameProc
    {
        public const int desiredvPixelRes = 16;

        public const string desiredWindowName = "Moth Paint";

        public static readonly Color desiredBGColor = Color.Black;

        public static readonly int desiredTPS = 24;

        private static readonly List<Color> colors = new List<Color> { Color.Black, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet, Color.White };

        private static readonly VPixel[,] frameBuffer = new VPixel[screenVPixelWidth, screenVPixelWidth];

        private static int colorIndex = 0;

        private static bool fillInProgress = false;

        public static void GameTick(long currentTick)
        {
            fillInProgress = false;
        }

        public static void Init()
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

        public static void MouseDown(Point selectedVPixel, MouseButtonState oldMouseState, bool lDown, bool rDown)
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

        public static void MouseMove(Point oldSelectedVPixel, Point newSelectedVPixel, MouseButtonState mouseState)
        {
            if (mouseState.L && oldSelectedVPixel != newSelectedVPixel && !(newSelectedVPixel.X == 0 && newSelectedVPixel.Y == 0))
            {
                PlotColor(colors[colorIndex], newSelectedVPixel.X, newSelectedVPixel.Y);
            }
        }

        public static void MouseUp(Point selectedVPixel, MouseButtonState oldMouseState, bool lUp, bool rUp)
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