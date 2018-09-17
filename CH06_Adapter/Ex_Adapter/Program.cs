﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Adapter
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        int width, height;

        public SquareToRectangleAdapter(Square square)
        {
            width = height = square.Side;
        }

        public int Width => width;

        public int Height => height;
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
