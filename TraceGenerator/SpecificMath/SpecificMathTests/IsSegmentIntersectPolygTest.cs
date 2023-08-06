using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class IsSegmentIntersectPolygTest {
        private static readonly Point begin = new Point(2, 2);

        private static readonly Tuple<Point, bool>[] pointCases =
            new[] { 
                new Tuple<Point, bool>(new Point(0, 1), false),
                new Tuple<Point, bool>(new Point(2, -1), true),
                new Tuple<Point, bool>(new Point(3, 2), true),
            };

        private static readonly Tuple<Segment, bool>[] cases
            = pointCases
            .Select((pntCase) => Tuple.Create(new Segment(begin, pntCase.Item1), pntCase.Item2))
            .ToArray();

        private static readonly ComplexPolygon cmpPolyg
            = new ComplexPolygon() { 
                FillingPolygones = new List<TrGenPolygon>() { 
                    new TrGenPolygon(new Point[]{
                        new Point(3, 3),
                        new Point(2, 0),
                        new Point(3, -3),
                        new Point(-1, 1)
                    })
                },
                VoidPolygones = new List<TrGenPolygon>() { 
                    new TrGenPolygon(new Point[] {
                        new Point(1, 1),
                        new Point(4, 1),
                        new Point(4, -2),
                    })
                }
            };

        [Test]
        public void TestIntersecting() {
            foreach (var aCase in cases) {
                var actual = cmpPolyg.IsSegmentIntersectEdjes(aCase.Item1);
                Assert.AreEqual(aCase.Item2, actual);
            }
        }
    }
}
