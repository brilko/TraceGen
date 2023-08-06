using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TraceGenerator {

    [TestFixture]
    class MathAbstractsTests {
        public class VectorTests {
            [Test]
            public void MakeVector() {
                var pnt1 = new Point(10, 3);
                var pnt2 = new Point(3, 11);
                var vect = new Vector(pnt1, pnt2);
                Assert.AreEqual(-7, vect.X);
                Assert.AreEqual(8, vect.Y);
            }

            private static readonly Vector[] vects =
                new Vector[] {
                new Vector(1, 0),
                new Vector(0, 1),
                new Vector(-5, 5),
                new Vector(100, 0),
                new Vector(41, 38)
                };

            [Test]
            public void TestCrossProducts() {
                TwoWayCrossProduct(vects[0], vects[1], 1);
                TwoWayCrossProduct(vects[0], vects[2], 5);
                TwoWayCrossProduct(vects[0], vects[3], 0);
                TwoWayCrossProduct(vects[0], vects[4], 38);

                TwoWayCrossProduct(vects[1], vects[2], 5);
                TwoWayCrossProduct(vects[1], vects[3], -100);
                TwoWayCrossProduct(vects[1], vects[4], -41);

                TwoWayCrossProduct(vects[2], vects[3], -500);
                TwoWayCrossProduct(vects[2], vects[4], -395);

                TwoWayCrossProduct(vects[3], vects[4], 3800);

                foreach (var vect in vects) {
                    TwoWayCrossProduct(vect, vect, 0);
                }
            }

            private void TwoWayCrossProduct(Vector vect1, Vector vect2, double expected) {
                TestCrossProduct(vect1, vect2, expected);
                TestCrossProduct(vect2, vect1, -expected);
            }

            private void TestCrossProduct(Vector vect1, Vector vect2, double expected) {
                double actual = vect1.CrossProduct(vect2);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void TestAngle() {
                Assert.AreEqual(90, vects[0].AngleBetween(vects[1]));
                Assert.AreEqual(-90, vects[1].AngleBetween(vects[0]));
                Assert.AreEqual(-135, vects[2].AngleBetween(vects[0]));
                Assert.AreEqual(-45, vects[2].AngleBetween(vects[1]));
            }
        }

        public class SegmentTests {
            [Test]
            public void MakeSegment() {
                var pnt1 = new Point(11, 13);
                var pnt2 = new Point(8, -5);
                var seg = new Segment(pnt1, pnt2);
                TestPoints(seg, pnt1, pnt2);
                seg = seg.Reverse();
                TestPoints(seg, pnt2, pnt1);
            }

            private void TestPoints(Segment seg, Point pnt1, Point pnt2) {
                Assert.AreEqual(seg.Pnt1.X, pnt1.X);
                Assert.AreEqual(seg.Pnt1.Y, pnt1.Y);
                Assert.AreEqual(seg.Pnt2.X, pnt2.X);
                Assert.AreEqual(seg.Pnt2.Y, pnt2.Y);
            }

            [TestCase(-4, 1, -2, 1, -3, 3, -3, 1)]
            [TestCase(-4, 1, -2, 1, -3, -1, -3, 1)]
            [TestCase(3, 2, 3, 4, 3, 1, 3, 2.2)]
            public void TestSquareRelationShip(
                double seg1X, double seg1Y, double seg2X, double seg2Y, 
                double pntX, double pntY, 
                double pntExpX, double pntExpY) {
                var seg = new Segment(new Point(seg1X, seg1Y), new Point(seg2X, seg2Y));
                var pnt = new Point(pntX, pntY);
                var expected = new Point(pntExpX, pntExpY);
                Assert.AreEqual(expected, seg.GetSquareRelationPoint(pnt));
            }
        }

        public class BoundingBoxTest {
            [TestCase(-9, 8, -7, 50)]
            [TestCase(7, 5, 6, -8)]
            [TestCase(0, 0, 1, 1)]
            public void MakeBox(double X1, double Y1, double X2, double Y2) {
                var minPnt = new Point(Math.Min(X1, X2), Math.Min(Y1, Y2));
                var maxPnt = new Point(Math.Max(X1, X2), Math.Max(Y1, Y2));
                TestBox(X1, X2, Y1, Y2, maxPnt, minPnt);
                TestBox(X1, X2, Y2, Y1, maxPnt, minPnt);
                TestBox(X2, X1, Y1, Y2, maxPnt, minPnt);
                TestBox(X2, X1, Y2, Y1, maxPnt, minPnt);
            }

            private void TestBox(double X1, double X2, double Y1, double Y2,
                Point maxPnt, Point minPnt) {
                var pnt1 = new Point(X1, Y1);
                var pnt2 = new Point(X2, Y2);
                var seg = new Segment(pnt1, pnt2);
                var box = new BoundingBox(seg);
                Assert.AreEqual(box.MaxPnt, maxPnt);
                Assert.AreEqual(box.MinPnt, minPnt);
            }

            private readonly Segment[] segments =
                new Segment[] {
                new Segment(new Point(-5, -6), new Point(-2, -2)),
                new Segment(new Point(-6, 4), new Point(5, 4)),
                new Segment(new Point(0, -1), new Point(6, 6)),
                };

            private readonly bool[,] expecteds =
                new bool[,] {
                { true, false, false},
                { false, true, true},
                { false, true, true}
                };

            [Test]
            public void TestIntersectBounds() {
                int size = expecteds.GetLength(0);
                for (int i = 0; i < size; i++) {
                    for (int j = 0; j < size; j++) {
                        foreach (var tup in Combine(segments[i], segments[j])) {
                            var box1 = new BoundingBox(tup.Item1);
                            var box2 = new BoundingBox(tup.Item2);
                            bool actual = box1.IsIntersect(box2);
                            Assert.AreEqual(expecteds[i, j], actual);
                        }
                    }
                }
            }

            private IEnumerable<Tuple<Segment, Segment>> Combine(Segment seg1, Segment seg2) {
                Segment[] seg2Comb = Combine(seg2).ToArray();
                foreach (var seg3 in Combine(seg1)) {
                    foreach (var seg4 in seg2Comb) {
                        yield return Tuple.Create(seg3, seg4);
                    }
                }
            }

            private IEnumerable<Segment> Combine(Segment seg) {
                yield return seg;
                yield return seg.Reverse();
                var pnt3 = new Point(seg.Pnt1.X, seg.Pnt2.Y);
                var pnt4 = new Point(seg.Pnt2.X, seg.Pnt1.Y);
                yield return new Segment(pnt3, pnt4);
                yield return new Segment(pnt4, pnt3);
            }

        }
    }
}
