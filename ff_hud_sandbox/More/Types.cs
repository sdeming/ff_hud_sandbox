using System;
using System.Runtime.InteropServices;

namespace More
{
  namespace Types
  {

    [StructLayout(LayoutKind.Sequential)]
    public struct PSIZE
    {
      public int x;
      public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
      internal POINT(int x, int y)
      {
        this.x = x;
        this.y = y;
      }

      public int x;
      public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
      internal RECT(PSIZE size)
      {
        Left = 0;
        Top = 0;
        Right = size.x - 1;
        Bottom = size.y - 1;
      }

      internal RECT(int width, int height)
      {
        Left = 0;
        Top = 0;
        Right = width - 1;
        Bottom = height - 1;
      }

      internal RECT(int left, int top, int right, int bottom)
      {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
      }

      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    public static class CoreTypeExtensions
    {
      public static RECT At(this RECT self, int x, int y)
      {
        var dx = self.Left - x;
        var dy = self.Top - y;
        self.Left = x;
        self.Top = y;
        self.Right = self.Right + dx;
        self.Bottom = self.Bottom + dy;
        return self;
      }
    }
  }
}