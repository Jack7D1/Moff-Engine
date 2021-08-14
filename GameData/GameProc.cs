using MoffEngine.Engine;
using System;

namespace MoffEngine
{
    internal static class GameProc
    {
        //Change this to set desired screen size
        public const int vPixelsWidth = 8;

        //Example vPixel matrix when vPixelScreenWidth = 4: This would be a small white square on a black background.
        // [[ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{255,255,255},{255,255,255},{0,0,0}]
        //  [ {0,0,0},{0,0,0},      {0,0,0},      {0,0,0}]]
        //vPixel table of RGB values that is plotted to the screen. Call Graphic.UpdateScreen with this buffer when ready to make a change to the screen.
        private static Graphic.VPixel[,] frameBuffer = new Graphic.VPixel[Graphic.vPixelScreenWidth, Graphic.vPixelScreenWidth];

        static public void GameTick()
        {
            //Disco Floor Demo
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int y = 0; y < Graphic.vPixelScreenWidth; y++)
                for (int x = 0; x < Graphic.vPixelScreenWidth; x++)
                {
                    int top = 255; 
                    frameBuffer[x, y].R = (byte)rand.Next(0, top);
                    frameBuffer[x, y].G = (byte)rand.Next(0, top);
                    frameBuffer[x, y].B = (byte)rand.Next(0, top);
                }

            Graphic.UpdateScreen(frameBuffer);
        }

        static public void Init()
        {
            for (int y = 0; y < Graphic.vPixelScreenWidth; y++)
                for (int x = 0; x < Graphic.vPixelScreenWidth; x++)
                    frameBuffer[x, y] = new Graphic.VPixel();
            Graphic.UpdateScreen(frameBuffer);
        }
    }
}