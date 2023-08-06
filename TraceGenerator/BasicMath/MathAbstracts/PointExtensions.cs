using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    static class PointExtensions {
        public static double GetSquareOfRange(this Point pnt, Point other) {
            double deltaX = pnt.X - other.X;
            double deltaY = pnt.Y - other.Y;
            return deltaX * deltaX + deltaY * deltaY;
        }
    }
}
