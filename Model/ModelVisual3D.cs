using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

public class ScreenSpaceLines3D : ModelVisual3D
{
	private readonly GeometryModel3D _model = new GeometryModel3D();
	private readonly MeshGeometry3D _mesh = new MeshGeometry3D();
	private readonly Model3DGroup _modelGroup = new Model3DGroup();

	public static readonly DependencyProperty PointsProperty =
		DependencyProperty.Register("Points", typeof(Point3DCollection), typeof(ScreenSpaceLines3D),
		new PropertyMetadata(new Point3DCollection(), PointsChanged));

	public static readonly DependencyProperty ThicknessProperty =
		DependencyProperty.Register("Thickness", typeof(double), typeof(ScreenSpaceLines3D),
		new PropertyMetadata(1.0, VisualPropertyChanged));

	public static readonly DependencyProperty ColorProperty =
		DependencyProperty.Register("Color", typeof(Color), typeof(ScreenSpaceLines3D),
		new PropertyMetadata(Colors.Black, VisualPropertyChanged));

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
		_modelGroup.Children.Add(_model);
		this.Content = _modelGroup;
		Points = new Point3DCollection();
	}

	private static void PointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var lines = (ScreenSpaceLines3D)d;
		lines.UpdateGeometry();
	}

	private static void VisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var lines = (ScreenSpaceLines3D)d;
		lines.UpdateMaterial();
	}

	private void UpdateMaterial()
	{
		_model.Material = new DiffuseMaterial(new SolidColorBrush(Color));
		_model.BackMaterial = _model.Material;
	}

	private void UpdateGeometry()
	{
		_mesh.Positions.Clear();
		_mesh.TriangleIndices.Clear();

		if (Points.Count < 2)
			return;

		for (int i = 0; i < Points.Count - 1; i++)
		{
			AddSegment(Points[i], Points[i + 1]);
		}
	}

	private void AddSegment(Point3D start, Point3D end)
	{
		// Упрощенная реализация - для реального проекта нужно более сложное вычисление
		// Здесь мы просто создаем тонкий прямоугольник между точками

		Vector3D direction = end - start;
		Vector3D up = new Vector3D(0, 1, 0);
		Vector3D side = Vector3D.CrossProduct(direction, up);
		side.Normalize();
		side *= Thickness / 2;

		int baseIndex = _mesh.Positions.Count;

		_mesh.Positions.Add(start + side);
		_mesh.Positions.Add(start - side);
		_mesh.Positions.Add(end + side);
		_mesh.Positions.Add(end - side);

		_mesh.TriangleIndices.Add(baseIndex);
		_mesh.TriangleIndices.Add(baseIndex + 1);
		_mesh.TriangleIndices.Add(baseIndex + 2);

		_mesh.TriangleIndices.Add(baseIndex + 1);
		_mesh.TriangleIndices.Add(baseIndex + 3);
		_mesh.TriangleIndices.Add(baseIndex + 2);
	}
}