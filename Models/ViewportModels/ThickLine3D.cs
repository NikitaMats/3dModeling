using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik.Models.ViewportModels
{
    public class ThickLine3D : ModelVisual3D
    {
        private readonly GeometryModel3D _model = new GeometryModel3D();
        private readonly MeshGeometry3D _mesh = new MeshGeometry3D();

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(Color),
                typeof(ThickLine3D),
                new PropertyMetadata(Colors.Black, OnPropertyChanged));

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register(
                "Thickness",
                typeof(double),
                typeof(ThickLine3D),
                new PropertyMetadata(0.05, OnPropertyChanged));

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        public ThickLine3D()
        {
            _model.Geometry = _mesh;
            this.Content = _model;
            UpdateMaterial();
        }

        public void SetPoints(Point3D start, Point3D end)
        {
            Vector3D direction = end - start;
            Vector3D up = new Vector3D(0, 1, 0);

            if (Math.Abs(Vector3D.DotProduct(Normalize(direction), up)) > 0.99)
                up = new Vector3D(1, 0, 0);

            Vector3D right = Vector3D.CrossProduct(direction, up);
            right = Normalize(right) * (Thickness / 2);

            Vector3D forward = Vector3D.CrossProduct(right, direction);
            forward = Normalize(forward) * (Thickness / 2);

            _mesh.Positions = new Point3DCollection
            {
                start + right + forward, start + right - forward,
                start - right + forward, start - right - forward,
                end + right + forward, end + right - forward,
                end - right + forward, end - right - forward
            };

            _mesh.TriangleIndices = new Int32Collection
            {
                0, 2, 4, 2, 6, 4, 1, 5, 3, 3, 5, 7,
                0, 4, 1, 1, 4, 5, 2, 3, 6, 3, 7, 6,
                0, 1, 2, 1, 3, 2, 4, 6, 5, 5, 6, 7
            };

            _mesh.Normals = CalculateNormals();
        }

        private Vector3D Normalize(Vector3D vector)
        {
            if (vector.LengthSquared > 0)
            {
                return vector / vector.Length;
            }
            return vector;
        }

        private Vector3DCollection CalculateNormals()
        {
            var normals = new Vector3DCollection();
            for (int i = 0; i < _mesh.Positions.Count; i++)
            {
                Vector3D normal = new Vector3D(
                    _mesh.Positions[i].X,
                    _mesh.Positions[i].Y,
                    _mesh.Positions[i].Z);
                normal.Normalize();
                normals.Add(normal);
            }
            return normals;
        }

        private void UpdateMaterial()
        {
            _model.Material = new DiffuseMaterial(new SolidColorBrush(Color));
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ThickLine3D line) line.UpdateMaterial();
        }
    }
}