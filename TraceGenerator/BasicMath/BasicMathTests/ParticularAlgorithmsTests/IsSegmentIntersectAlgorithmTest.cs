using NUnit.Framework;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class IsSegmentIntersectAlgorithmTest {
        private static readonly Segment[] segments =
            new Segment[] {
                new Segment(new Point(-6, -8), new Point(-8, -3)),
                new Segment(new Point(8, -8), new Point(6, -3)),
                new Segment(new Point(-10, 9), new Point(-10, 5)),
                new Segment(new Point(-9, 7), new Point(-6, 7)),
                new Segment(new Point(-10, 4), new Point(8, 4)),
                new Segment(new Point(-2, 2), new Point(-4, 6)),
                new Segment(new Point(5, 8), new Point(7, 2)),
                new Segment(new Point(5, 2), new Point(7, 8)),
            };

        private static readonly bool[,] expecteds =
            new bool[,] {
                { true, false, false, false, false, false, false, false },
                { false, true, false, false, false, false, false, false },
                { false, false, true, false, false, false, false, false },
                { false, false, false, true, false, false, false, false },
                { false, false, false, false, true, true, true, true },
                { false, false, false, false, true, true, false, false },
                { false, false, false, false, true, false, true, true },
                { false, false, false, false, true, false, true, true },
            };

        [Test]
        public void IntersectTest() {
            int size = segments.Length;
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    bool actual = Combine(segments[i], segments[j]);
                    Assert.AreEqual(expecteds[i, j], actual, i.ToString() + " " + j.ToString());
                }
            }
        }

        private bool Combine(Segment seg1, Segment seg2) {
            bool ans = TwoWayTest(seg1, seg2);
            var seg3 = seg1.Reverse();
            CombineTest(seg3, seg2, ans);
            var seg4 = seg2.Reverse();
            CombineTest(seg1, seg4, ans);
            CombineTest(seg3, seg4, ans);
            return ans;
        }

        private bool TwoWayTest(Segment seg1, Segment seg2) {
            bool ans1 = Geometry.IsIntersect(seg1, seg2);
            bool ans2 = Geometry.IsIntersect(seg2, seg1);
            Assert.AreEqual(ans1, ans2);
            return ans1;
        }

        private void CombineTest(Segment seg1, Segment seg2, bool expected) {
            bool ans = TwoWayTest(seg1, seg2);
            Assert.AreEqual(expected, ans);
        }
    }
}
