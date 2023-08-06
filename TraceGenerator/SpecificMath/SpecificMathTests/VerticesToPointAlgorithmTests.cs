using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class VerticesToPointAlgorithmTests {
        private static readonly ComplexPolygon cmplPolyg =
            new ComplexPolygon() {
                FillingPolygones = new List<TrGenPolygon>() {
                    new TrGenPolygon(new Point[]{
                        new Point(-3, 5),
                        new Point(-5, -2),
                        new Point(-2, -4),
                        new Point(-2, -8),
                        new Point(5, -9),
                        new Point(5, -7),
                        new Point(8, -7),
                        new Point(8, -6),
                        new Point(9, -6),
                        new Point(9, -5),
                        new Point(8, -4),
                        new Point(9, -3),
                        new Point(9, -1),
                        new Point(6, 3),
                        new Point(1, -2),
                        new Point(3, 4),
                        new Point(-2, 2)
                    })
                },
                VoidPolygones = new List<TrGenPolygon>() { 
                    new TrGenPolygon(new Point[]{
                        new Point(5, -6),
                        new Point(7, -4),
                        new Point(5, -3)
                    })
                }
            };

        private static readonly Point[] startPoints =
            new Point[] {
                new Point(1, -6),
                new Point(5, -1)
            };

        private static readonly Dictionary<Point, HashSet<Point>> expectedsVariants =
            new Dictionary<Point, HashSet<Point>>() { 
                [startPoints[0]] = new HashSet<Point>() {
                    new Point(1, -2),
                    new Point(-2, 2),
                    new Point(-2, -4),
                    new Point(-2, -8),
                    new Point(5, -9),
                    new Point(5, -7),
                    new Point(8, -7),
                    new Point(5, -6),
                    new Point(5, -3),
                    new Point(6, 3),
                },
                [startPoints[1]] = new HashSet<Point>() {
                    new Point(6, 3),
                    new Point(1, -2),
                    new Point(-2, -4),
                    new Point(-2, -8),
                    new Point(5, -3),
                    new Point(7, -4),
                    new Point(9, -6),
                    new Point(8, -4),
                    new Point(9, -3),
                    new Point(9, -1),
                }
            };

        [TestCase(0)]
        [TestCase(1)]
        public void TryFindVerticesToPoint(int variantInd) {
            Point pnt = startPoints[variantInd];
            HashSet<Point> expecteds = expectedsVariants[pnt];
            var testPnts = TrGenAlgorithms.FindVerticesToPoint(cmplPolyg, pnt).ToArray();
            HashSet<Point> delete = new HashSet<Point>();
            HashSet<Point> intersect = new HashSet<Point>();
            foreach (var pntT in testPnts) {
                if (IsContain(expecteds, pntT)) {
                    intersect.Add(pntT);
                } else {
                    delete.Add(pntT);
                }
            }
            foreach (var pntT in testPnts) {
                TestPoint(expecteds, pntT);
            }
            Assert.IsTrue(expecteds.Count() == 0, "Не все точки");
        }

        private bool IsContain(HashSet<Point> expect, Point pnt) {
            foreach (var pntE in expect) {
                if (pntE.Equals(pnt))
                    return true;
            }
            return false;
        }

        private void TestPoint(HashSet<Point> expecteds, Point pnt) {
            Assert.IsTrue(IsContain(expecteds, pnt), "Не содержит " + pnt.ToString());
            expecteds.Remove(pnt);
        }
    }
}
