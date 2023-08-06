using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class FindFreeEdgesAlgorithmTest {
        private static readonly ComplexPolygon cmplPolyg =
            new ComplexPolygon() {
                FillingPolygones = new List<TrGenPolygon>() {
                    new TrGenPolygon(new Point[]{ 
                        new Point(2, 7),
                        new Point(-6, 5),
                        new Point(-3, -4),
                        new Point(6, -2),
                        new Point(5, 4),
                    })
                },
                VoidPolygones = new List<TrGenPolygon>() { 
                    new TrGenPolygon(new Point[]{
                        new Point(4, 3),
                        new Point(2, 1),
                        new Point(4, -2),
                    })
                }
            };

        private static readonly TrGenPolygon polygon =
            new TrGenPolygon(new Point[] {
                new Point(-6, 5),
                new Point(-3, -4),
                new Point(4, -2),
                new Point(2, 1),
                new Point(4, 3)
            });

        private static readonly List<Segment> expected =
            new List<Segment> { 
                new Segment(new Point(-3, -4), new Point(4, -2)),
                new Segment(new Point(-6, 5), new Point(4, 3))
            };

        [Test]
        public void TestFindFreeEdges() {
            var actual = TrGenAlgorithms.FindFreeEdges(cmplPolyg, polygon).ToArray();
            Assert.AreEqual(expected.Count, actual.Length);
            foreach (var seg in actual) {
                Assert.IsTrue(expected.Contains(seg));
                expected.Remove(seg);
            }

        }
    }
}
