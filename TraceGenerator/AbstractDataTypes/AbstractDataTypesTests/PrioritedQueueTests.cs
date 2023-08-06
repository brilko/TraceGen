using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TraceGenerator {
    [TestFixture]
    class PrioritedQueueTests {
        [Test]
        public void TestPrioritedDotAndSegment() {
            var factory = new DotAndSegmentFactory(new Point(0, 2));
            var dots = new PrioritedDotAndSegment[] {
                factory.Build(1, 2),
                factory.Build(1, 3),
                factory.Build(0, 4),
                factory.Build(1, 5),
                factory.Build(3, 5),
            };
            var queue = new PriorityQueue<PrioritedDotAndSegment>(BestPriority.Minimal);
            var dotsToAdd = new PrioritedDotAndSegment[] {
                dots[4],
                dots[3],
                dots[0],
                dots[2],
                dots[1]
            };
            foreach (var dotSeg in dotsToAdd) {
                queue.PrEnqueue(dotSeg);
            }
            foreach (var dotSeg in dots) {
                Assert.IsTrue(queue.PrDequeue() == dotSeg);
            }
        }
    }
}
