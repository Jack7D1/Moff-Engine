using MoffEngine.Engine;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static MoffEngine.Engine.Master;

namespace MoffEngine
{
    internal static class GameProc
    {
        //Change this to set desired screen size, the engine is NOT meant for values over 32!!
        public const int desiredvPixelRes = 16;

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
                    frameBuffer[x, y] = new VPixel(255,255,255);
            //Reccomended Frame Buffer init code.
            frameBuffer[0, 0].SetRGB(colors[colorIndex].R, colors[colorIndex].G, colors[colorIndex].B);

            frameBuffer[0, 1].SetColor(Color.DimGray);
            frameBuffer[1, 0].SetColor(Color.DimGray);
            frameBuffer[1, 1].SetColor(Color.DimGray);
            UpdateScreen(frameBuffer);
        }

        
        public static void MouseUp(Point selectedVPixel, MouseButtonState oldMouseState, bool lUp, bool rUp)
        {

        }

        public static void MouseDown(Point selectedVPixel, MouseButtonState oldMouseState, bool lDown, bool rDown)
        {
            if (lDown)
            {
                PlotColor(colors[colorIndex], selectedVPixel.X, selectedVPixel.Y);
            }
            else if(rDown && !oldMouseState.R)
            {
                colorIndex++;
                if (colorIndex >= colors.Count)
                    colorIndex = 0;

                frameBuffer[0, 0].SetColor(colors[colorIndex]);
                UpdateScreen(frameBuffer);
            }
        }
        private static void PlotColor(Color color, int x, int y)
        {
            if ((x <= 1 && y <= 1))
                return;
            frameBuffer[x, y].SetColor(color);
            UpdateScreen(frameBuffer);
        }

        private static int colorIndex = 0;
        private static List<Color> colors = new List<Color> { Color.Black, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet, Color.White };
        public static void MouseMove(Point oldSelectedVPixel, Point newSelectedVPixel, MouseButtonState mouseState)
        {
            if(mouseState.L && oldSelectedVPixel != newSelectedVPixel)
            {
                PlotColor(colors[colorIndex], newSelectedVPixel.X, newSelectedVPixel.Y);
            }
        }

    }
}