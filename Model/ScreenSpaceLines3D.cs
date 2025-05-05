using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    public class ScreenSpaceLines3D : ModelVisual3D
    {
        private readonly GeometryModel3D _model = new GeometryModel3D();
        private readonly MeshGeometry3D _mesh = new MeshGeometry3D();

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(Color),
                typeof(ScreenSpaceLines3D),
                new PropertyMetadata(Colors.Black, OnColorChanged));

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public ScreenSpaceLines3D()
        {
            _model.Geometry = _mesh;
            this.Content = _model;
            UpdateMaterial();
        }

        public void SetPoints(Point3D start, Point3D end)
        {
            _mesh.Positions = new Point3DCollection { start, end };
            _mesh.TriangleIndices = new Int32Collection { 0, 1 };
        }

        private void UpdateMaterial()
        {
            _model.Material = new DiffuseMaterial(new SolidColorBrush(Color));
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScreenSpaceLines3D lines)
            {
                lines.UpdateMaterial();
            }
        }
    }
}