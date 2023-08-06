using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    class PrioritedDotAndSegment : IPriorited {
        public Point Dot { get; }
        public Segment Segment { get; }
        public double Priority { get; }

        public PrioritedDotAndSegment(Point dot, Segment seg, double priority) {
            Dot = dot;
            Segment = seg;
            Priority = priority;
        }

        public int TakePriority() {
            return (int)(Priority * 100);
        }
    }

    class DotAndSegmentFactory {
        private Point end;
        public DotAndSegmentFactory(Point end) {
            this.end = end;
        }

        public PrioritedDotAndSegment Build(double x, double y) {
            var pnt = new Point(x, y);
            return new PrioritedDotAndSegment(pnt, default, end.GetSquareOfRange(pnt));
        }

        public PrioritedDotAndSegment Build(Point pnt, Segment seg) {
            return new PrioritedDotAndSegment(pnt, seg, end.GetSquareOfRange(pnt));
        }
    }
}
