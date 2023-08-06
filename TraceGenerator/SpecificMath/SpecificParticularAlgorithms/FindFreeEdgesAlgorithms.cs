using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceGenerator {
    /// <summary>
    /// Находит грани polygon, которые не совпадают гранями cmplPolyg
    /// </summary>
    static class FindFreeEdgesAlgorithms {
        public static IEnumerable<Segment> FindFreeEdges(
            ComplexPolygon cmplPolyg, TrGenPolygon polygon) {
            var allEdges = cmplPolyg.GetAllEdges();
            foreach (var edgePolyg in polygon.Edges) {
                if (!IsSegmentEquals(edgePolyg, allEdges))
                    yield return edgePolyg;
            }
        }

        private static bool IsSegmentEquals(Segment seg, IEnumerable<Segment> segments) {
            foreach (var enumSeg in segments) {
                if (enumSeg.Equals(seg))
                    return true;
            }
            return false;
        }
    }
}
