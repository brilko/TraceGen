using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    struct BoundingBox {
        public readonly Point MinPnt;
        public readonly Point MaxPnt;

        public BoundingBox(Segment seg) {
            MinPnt = MakePoint(seg, Math.Min);
            MaxPnt = MakePoint(seg, Math.Max);
        }

        private static Point MakePoint(Segment seg, Func<double, double, double> func) {
            double x = XSeg(seg, func);
            double y = YSeg(seg, func);
            return new Point(x, y);
        }

        private static double XSeg(Segment seg, Func<double, double, double> func)
             => func(seg.Pnt1.X, seg.Pnt2.X);

        private static double YSeg(Segment seg, Func<double, double, double> func)
            => func(seg.Pnt1.Y, seg.Pnt2.Y);

        public bool IsIntersect(BoundingBox box) {
            return MaxPnt.X >= box.MinPnt.X
                && MinPnt.X <= box.MaxPnt.X
                && MaxPnt.Y >= box.MinPnt.Y
                && MinPnt.Y <= box.MaxPnt.Y;
        }
    }
}
