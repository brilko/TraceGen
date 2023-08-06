using System.Security.Principal;
using System.Windows;

namespace TraceGenerator {
    public struct Segment {
        public readonly Point Pnt1;
        public readonly Point Pnt2;

        public Segment(Point pnt1, Point pnt2) {
            Pnt1 = pnt1;
            Pnt2 = pnt2;
        }

        public Vector GetDirectionVector()
            => new Vector(Pnt1, Pnt2);

        public Segment Reverse()
            => new Segment(Pnt2, Pnt1);

        public override bool Equals(object obj) {
            if (obj is Segment seg) {
                if (seg.Pnt1.Equals(Pnt1)) {
                    return seg.Pnt2.Equals(Pnt2);
                } else if (seg.Pnt2.Equals(Pnt1)) {
                    return seg.Pnt1.Equals(Pnt2);
                } 
            }
            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return Pnt1.ToString() + "~" + Pnt2.ToString();
        }

        public bool IsIntersect(Segment other) 
            => Geometry.IsIntersect(this, other);

        /// <summary>
        /// Возвращает такую точку на этом отрезке, что отношение расстояния от точки до конца отрезка
        /// пропорционально квадрату расстояния от этого конца до заданной точки  
        /// </summary>
        public Point GetSquareRelationPoint(Point pnt) {
            var range1 = pnt.GetSquareOfRange(Pnt1);
            var range2 = pnt.GetSquareOfRange(Pnt2);
            var rangeSum = range1 + range2;
            var vect =  GetDirectionVector().ToWinVector();
            vect /= rangeSum;
            vect *= range1;
            return Pnt1 + vect;
        }
    }
}
