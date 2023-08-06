using System.Windows;
using System.Windows.Controls;

namespace TraceGen {
    class PaintingZone {
        public IPaintZoneTool CurrentTool;
        private readonly Canvas canvas;
        public TaskTraseGenBuilder TaskBuilder;
        private readonly AnswerAssembler answerAsm = new AnswerAssembler();

        public PaintingZone(Canvas canvas) {
            CurrentTool = PolygoneBuilder.FillBuilder;
            this.canvas = canvas;
            TaskBuilder = new TaskTraseGenBuilder();
        }

        public void NextPoint(Point newPnt) {
            foreach (var shape in CurrentTool.NextPoint(newPnt, TaskBuilder)) {
                canvas.Children.Add(shape);
            }
        }

        public void EndShape(Point newPnt) {
            foreach (var shape in CurrentTool.EndShape(newPnt, TaskBuilder)) {
                canvas.Children.Add(shape);
            }
        }

        public void Generate() {
            foreach (var shape in answerAsm.Assembly(TaskBuilder.Form())) {
                canvas.Children.Add(shape);
            }
        }
    }
}
