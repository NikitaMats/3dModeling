using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    public class ScreenSpaceLines3D : ModelVisual3D
    {
        private readonly GeometryModel3D _model = new GeometryModel3D();
        private readonly MeshGeometry3D _mesh = new MeshGeometry3D();

        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(Point3DCollection), typeof(ScreenSpaceLines3D),
            new PropertyMetadata(new Point3DCollection(), PointsChanged));

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(ScreenSpaceLines3D),
            new PropertyMetadata(1.0));

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ScreenSpaceLines3D),
            new PropertyMetadata(Colors.Black));

        public Point3DCollection Points
        {
            get => (Point3DCollection)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public ScreenSpaceLines3D()
        {
            _model.Geometry = _mesh;
            this.Content = _model;
            Points = new Point3DCollection();
            UpdateMaterial();
        }

        private static void PointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScreenSpaceLines3D lines)
            {
                lines.UpdateGeometry();
            }
        }

        private void UpdateMaterial()
        {
            _model.Material = new DiffuseMaterial(new SolidColorBrush(Color));
        }

        private void UpdateGeometry()
        {
            _mesh.Positions = new Point3DCollection(Points);
            _mesh.TriangleIndices.Clear();

            // Простая реализация - рисуем линии как треугольные полосы
            for (int i = 0; i < Points.Count; i++)
            {
                _mesh.TriangleIndices.Add(i);
            }
        }
    }
}