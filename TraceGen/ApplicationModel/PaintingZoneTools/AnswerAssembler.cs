using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Shapes;
using TraceGenerator;

namespace TraceGen {
    class AnswerAssembler {
        private readonly GeneratorOfTraces generator = new GeneratorOfTraces();
        private readonly TraceBuilder traceBuilder = TraceBuilder.StdTraceBuilder;
        public IEnumerable<Shape> Assembly(TaskTraceGen task) {
            var way = generator.GenerateTrace(task);
            foreach (var shape in traceBuilder.BuildWay(way)) {
                yield return shape;
            }
        }
    }
}
