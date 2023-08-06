using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TraceGenerator {
    static class VerticesToPointAlgorithm {
        public static IEnumerable<Point> FindVerticesToPoint(ComplexPolygon cmplPolyg, Point pnt) {
            var allEdges = cmplPolyg.GetAllEdges().ToArray();
            foreach (var point in IntersectSegmInPolygs(allEdges, cmplPolyg.FillingPolygones, pnt))
                yield return point;
            foreach (var point in IntersectSegmInPolygs(allEdges, cmplPolyg.VoidPolygones, pnt))
                yield return point;
        }

        public static IEnumerable<Point> IntersectSegmInPolygs(Segment[] allEdges,
            List<TrGenPolygon> polygones, Point pnt) {
            foreach (var polyg in polygones) {
                foreach(var point in IntersectSegmInPolyg(allEdges, polyg, pnt)) 
                    yield return point;
            }
        }

        //TODO: У нас должна быть ода линейно связное подпространство, значит ток 1 включающий многоугольник
        public static IEnumerable<Point> IntersectSegmInPolyg(Segment[] allEdges,
            TrGenPolygon polygon, Point pnt) {
            foreach (var pntP in polygon.Points) {
                var seg = new Segment(pnt, pntP);
                if (!IsSegmentIntersectWithEdges(allEdges, seg))
                    yield return pntP;
            }
        }

        private static bool IsSegmentIntersectWithEdges(Segment[] allEdges, Segment seg) {
            foreach (var edge in allEdges) {
                if (seg.Pnt2 != edge.Pnt1 && seg.Pnt2 != edge.Pnt2
                    && seg.IsIntersect(edge))
                    return true;
            }
            return false;
        }
    }
}
