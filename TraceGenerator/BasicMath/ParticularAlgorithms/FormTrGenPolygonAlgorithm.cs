using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    /// <summary>
    /// Создаёт из набора вершин и точки внутри выпуклого полигона полигон. Вершины могут идти в любом порядке
    /// </summary>
    static class FormTrGenPolygonAlgorithm {
        public static TrGenPolygon FormPolygon(Point[] vertices, Point interPnt) {
            var answer = (Point[])vertices.Clone();
            var angles = FindAngles(answer, interPnt);
            Array.Sort(angles, answer);
            return new TrGenPolygon(answer);
        }

        private static double[] FindAngles(Point[] vertices, Point interPnt) {
            double[] angles = new double[vertices.Length];
            var dirVect = new Vector(1, 0);
            var vect = new Vector();
            for (int i = 0; i < vertices.Length; i++) {
                vect.Recalc(interPnt, vertices[i]);
                double ang = dirVect.AngleBetween(vect);
                angles[i] = ang < 0 ? ang + 360 : ang;
            }
            return angles;
        } 
    }
}
