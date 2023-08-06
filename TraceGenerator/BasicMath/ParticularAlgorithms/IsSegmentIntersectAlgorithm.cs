using System.Windows;

namespace TraceGenerator {
    static class IsSegmentIntersectAlgorithm {
        public static bool IsIntersect(Segment segA, Segment segB) {
            return QuickRejection(segA, segB)
                && IsStradlessLine(segA, segB)
                && IsStradlessLine(segB, segA);
        }

        public static bool QuickRejection(Segment segA, Segment segB) {
            var boxA = new BoundingBox(segA);
            var boxB = new BoundingBox(segB);
            return boxA.IsIntersect(boxB);
        }

        public static bool IsStradlessLine(Segment segA, Segment segB) {
            var vect1 = segA.GetDirectionVector();
            var vect2 = new Vector(segA.Pnt1, segB.Pnt1);
            var vect3 = new Vector(segA.Pnt1, segB.Pnt2);
            return vect1.CrossProduct(vect2) * vect1.CrossProduct(vect3) <= 0;
        }
    }
}
