using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    /// <summary>
    /// Класс для отрисовки 3D-линий во Viewport3D
    /// </summary>
    public class ScreenSpaceLines3D : ModelVisual3D
    {
        private readonly GeometryModel3D _model = new GeometryModel3D();
        private readonly MeshGeometry3D _mesh = new MeshGeometry3D();

        #region Dependency Properties

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register(
                "Thickness",
                typeof(double),
                typeof(ScreenSpaceLines3D),
                new PropertyMetadata(1.0));

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(Color),
                typeof(ScreenSpaceLines3D),
                new PropertyMetadata(Colors.Black, OnColorChanged));

        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register(
                "Points",
                typeof(Point3DCollection),
                typeof(ScreenSpaceLines3D),
                new PropertyMetadata(new Point3DCollection(), OnPointsChanged));

        #endregion

        #region Public Properties

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

        public Point3DCollection Points
        {
            get => (Point3DCollection)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        #endregion

        public ScreenSpaceLines3D()
        {
            InitializeModel();
        }

        private void InitializeModel()
        {
            _model.Geometry = _mesh;
            _model.Material = new DiffuseMaterial(new SolidColorBrush(Color));
            this.Content = _model;
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScreenSpaceLines3D lines)
            {
                lines._model.Material = new DiffuseMaterial(new SolidColorBrush((Color)e.NewValue));
            }
        }

        private static void OnPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScreenSpaceLines3D lines && e.NewValue is Point3DCollection points)
            {
                lines.UpdateGeometry(points);
            }
        }

        private void UpdateGeometry(Point3DCollection points)
        {
            _mesh.Positions = points;
            _mesh.TriangleIndices.Clear();

            if (points.Count < 2) return;

            // Создаем упрощенную линию (два треугольника)
            for (int i = 0; i < points.Count - 1; i++)
            {
                _mesh.TriangleIndices.Add(i);
                _mesh.TriangleIndices.Add(i);
                _mesh.TriangleIndices.Add(i + 1);
            }
        }

        /// <summary>
        /// Устанавливает линию между двумя точками
        /// </summary>
        public void SetPoints(Point3D start, Point3D end)
        {
            Points = new Point3DCollection { start, end };
        }
    }
}