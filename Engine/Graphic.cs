using System.Drawing;

namespace MoffEngine.Engine
{
    internal static class Graphic
    {
        //Width of screen in pixels
        public const int ScreenWidth = 512;

        //Width screen in vPixels
        public const int vPixelScreenWidth = ScreenWidth / vPixelWidth;

        //Width of vPixel in screen pixels
        public const int vPixelWidth = ScreenWidth / GameProc.vPixelsWidth;

        //Pass a 2D VPixel array of the proper dimensions (vPixelScreenWidth x vPixelScreenWidth) to this function. Returns true if screen updated successfully, false otherwise.
        public static bool UpdateScreen(VPixel[,] newFrame)
        {
            if (newFrame.GetLength(0) != vPixelScreenWidth || newFrame.GetLength(1) != vPixelScreenWidth)
                return false;
            for (int y = 0; y < vPixelScreenWidth; y++)
                for (int x = 0; x < vPixelScreenWidth; x++)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(newFrame[x, y].R, newFrame[x, y].G, newFrame[x, y].B));
                    Master.Graphics.FillRectangle(brush, vPixelWidth * x, vPixelWidth * y, vPixelWidth, vPixelWidth);
                    brush.Dispose();
                }
            return true;
        }

        public class VPixel
        {
            public byte R, G, B;
            private const int byte2intRatio = (int.MaxValue) / (byte.MaxValue);

            public VPixel(byte redVal = 0, byte grnVal = 0, byte bluVal = 0)
            {
                R = redVal;
                G = grnVal;
                B = bluVal;
            }
        }
    }
}