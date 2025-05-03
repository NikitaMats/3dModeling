using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using TestCase_Sputnik.Model;

namespace TestCase_Sputnik
{
    public partial class MainWindow : Window
    {
        private readonly Cube originalModel;
        private readonly Cube currentModel;

        public MainWindow()
        {
            InitializeComponent();
            originalModel = new Cube();
            currentModel = new Cube
            {
                Vertices = new System.Collections.Generic.List<Point3D>(originalModel.Vertices),
                Edges = new System.Collections.Generic.List<Tuple<int, int>>(originalModel.Edges)
            };
            DrawModel();
        }

        private void DrawModel()
        {
            viewport.Children.Clear();

            // Добавляем оси координат для ориентира
            AddCoordinateAxes();

            foreach (var edge in currentModel.Edges)
            {
                var line = new ScreenSpaceLines3D();
                line.Points.Add(currentModel.Vertices[edge.Item1]);
                line.Points.Add(currentModel.Vertices[edge.Item2]);
                line.Color = Colors.Blue;
                line.Thickness = 2;
                viewport.Children.Add(line);
            }
        }

        private void AddCoordinateAxes()
        {
            // Ось X (красная)
            var xAxis = new ScreenSpaceLines3D();
            xAxis.Points.Add(new Point3D(0, 0, 0));
            xAxis.Points.Add(new Point3D(2, 0, 0));
            xAxis.Color = Colors.Red;
            xAxis.Thickness = 1;
            viewport.Children.Add(xAxis);

            // Ось Y (зеленая)
            var yAxis = new ScreenSpaceLines3D();
            yAxis.Points.Add(new Point3D(0, 0, 0));
            yAxis.Points.Add(new Point3D(0, 2, 0));
            yAxis.Color = Colors.Green;
            yAxis.Thickness = 1;
            viewport.Children.Add(yAxis);

            // Ось Z (синяя)
            var zAxis = new ScreenSpaceLines3D();
            zAxis.Points.Add(new Point3D(0, 0, 0));
            zAxis.Points.Add(new Point3D(0, 0, 2));
            zAxis.Color = Colors.Blue;
            zAxis.Thickness = 1;
            viewport.Children.Add(zAxis);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            currentModel.Vertices = new System.Collections.Generic.List<Point3D>(originalModel.Vertices);
            currentModel.Edges = new System.Collections.Generic.List<Tuple<int, int>>(originalModel.Edges);
            DrawModel();
        }

        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double dx = double.Parse(txtDx.Text);
                double dy = double.Parse(txtDy.Text);
                double dz = double.Parse(txtDz.Text);

                Transformations.Translate(currentModel, dx, dy, dz);
                DrawModel();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RotateX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double angle = double.Parse(txtRotX.Text);
                Transformations.RotateX(currentModel, angle);
                DrawModel();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid angle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Scale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double scale = double.Parse(txtScale.Text);
                //Transformations.Scale(currentModel, scale);
                DrawModel();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid scale factor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}