using System.Windows;
using System.Windows.Input;

namespace TraceGen {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly TraceGeneratorApplicationModel traceGen;

        public MainWindow() {
            InitializeComponent();
            traceGen = new TraceGeneratorApplicationModel(PaintZone);
        }
        #region Buttons
        private void ButtonPolygone_Click(object sender, RoutedEventArgs e) {
            traceGen.AppPaintingZone.CurrentTool = PolygoneBuilder.FillBuilder;
        }

        private void ButtonVoid_Click(object sender, RoutedEventArgs e) {
            traceGen.AppPaintingZone.CurrentTool = PolygoneBuilder.VoidBuilder;
        }

        private void ButtonBegin_Click(object sender, RoutedEventArgs e) {
            traceGen.AppPaintingZone.CurrentTool = PointBuilder.BeginBuilder;
        }

        private void ButtonEnd_Click(object sender, RoutedEventArgs e) {
            traceGen.AppPaintingZone.CurrentTool = PointBuilder.EndBuilder;
        }

        private void ButtonGenerate_Click(object sender, RoutedEventArgs e) {
            traceGen.AppPaintingZone.Generate();
        }
        #endregion

        private void PaintZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            Point newPnt = e.GetPosition(PaintZone);
            traceGen.AppPaintingZone.NextPoint(newPnt);
        }

        private void PaintZone_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            Point newPnt = e.GetPosition(PaintZone);
            traceGen.AppPaintingZone.EndShape(newPnt);
        }
    }
}