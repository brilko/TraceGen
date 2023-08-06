using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceGenerator {
    /// <summary>
    /// Определяет пересекает ли отрезок хотя бы одну грань сложного полигона
    /// </summary>
    static class IsSegmentIntersectComplPolygAlgorithm {
        public static bool IsSegmentIntersectCompPolyg(Segment seg, ComplexPolygon cmpPolyg) {
            foreach (var edge in cmpPolyg.GetAllEdges()) {
                if (seg.IsIntersect(edge)) return true;
            }
            return false;
        }
    }
}
