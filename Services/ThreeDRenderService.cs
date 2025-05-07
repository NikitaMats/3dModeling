using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using TestCase_Sputnik.Models;
using TestCase_Sputnik.Models.ViewportModels;
using TestCase_Sputnik.Services.Interfaces;

namespace TestCase_Sputnik.Services
{
    public class ThreeDRenderService : I3DRenderService
    {
        private ModelVisual3D _currentModel;
        private Transform3DGroup _modelTransformations;

        public Viewport3D Viewport { get; }

        public ThreeDRenderService(Viewport3D viewport)
        {
            Viewport = viewport;
        }

        public void InitializeScene()
        {
            Viewport.Children.Clear();
            AddCoordinateAxes();
            AddFrustum();
        }

        private void AddCoordinateAxes()
        {
            var axes = new Model3DGroup();

            AddThickLine(axes, new Point3D(-4, -4, -4), new Point3D(5, -4, -4), Colors.Red);
            AddThickLine(axes, new Point3D(-4, -4, -4), new Point3D(-4, 5, -4), Colors.Green);
            AddThickLine(axes, new Point3D(-4, -4, -4), new Point3D(-4, -4, 5), Colors.Blue);

            Viewport.Children.Add(new ModelVisual3D { Content = axes });
        }

        private void AddThickLine(Model3DGroup group, Point3D start, Point3D end, Color color)
        {
            var line = new ThickLine3D { Color = color, Thickness = 0.04 };
            line.SetPoints(start, end);
            group.Children.Add((GeometryModel3D)line.Content);
        }

        private void AddFrustum()
        {
            _currentModel = FrustumGenerator.CreateTriangularFrustum(3.0, 1.5, 2.0);
            _modelTransformations = new Transform3DGroup();
            _currentModel.Transform = _modelTransformations;
            Viewport.Children.Add(_currentModel);
        }

        public void SetProjection(ProjectionType projectionType)
        {
            switch (projectionType)
            {
                case ProjectionType.Perspective:
                    Viewport.Camera = new PerspectiveCamera
                    {
                        Position = new Point3D(8, 8, 8),
                        LookDirection = new Vector3D(-1, -1, -1),
                        UpDirection = new Vector3D(0, 1, 0),
                        FieldOfView = 45,
                        NearPlaneDistance = 0.1,
                        FarPlaneDistance = 100
                    };
                    break;

                case ProjectionType.Orthographic:
                    Viewport.Camera = new OrthographicCamera
                    {
                        Position = new Point3D(8, 8, 8),
                        LookDirection = new Vector3D(-1, -1, -1),
                        UpDirection = new Vector3D(0, 1, 0),
                        Width = 10,
                        NearPlaneDistance = 0.1,
                        FarPlaneDistance = 100
                    };
                    break;

                case ProjectionType.OrthographicFront:
                    Viewport.Camera = new OrthographicCamera
                    {
                        Position = new Point3D(0, 0, -15),
                        LookDirection = new Vector3D(0, 0, 1),
                        UpDirection = new Vector3D(0, 1, 0),
                        Width = 10,
                        NearPlaneDistance = 0.1,
                        FarPlaneDistance = 100
                    };
                    break;
            }
        }

        public void Translate(double x, double y, double z)
        {
            if (_currentModel == null) return;
            _modelTransformations.Children.Add(new TranslateTransform3D(x, y, z));
        }

        public void Rotate(RotationAxis axis, double angle)
        {
            if (_currentModel == null) return;

            Vector3D rotationAxis = axis switch
            {
                RotationAxis.X => new Vector3D(1, 0, 0),
                RotationAxis.Y => new Vector3D(0, 1, 0),
                RotationAxis.Z => new Vector3D(0, 0, 1),
                _ => new Vector3D(0, 0, 1)
            };

            _modelTransformations.Children.Add(new RotateTransform3D(
                new AxisAngleRotation3D(rotationAxis, angle)));
        }

        public void Scale(double factor)
        {
            if (_currentModel == null || factor <= 0) return;
            _modelTransformations.Children.Add(new ScaleTransform3D(factor, factor, factor));
        }

        public void Mirror(MirrorType mirrorType)
        {
            if (_currentModel == null) return;

            var scale = mirrorType switch
            {
                MirrorType.XY => new ScaleTransform3D(1, 1, -1),
                MirrorType.XZ => new ScaleTransform3D(1, -1, 1),
                MirrorType.YZ => new ScaleTransform3D(-1, 1, 1),
                _ => new ScaleTransform3D(1, 1, 1)
            };

            _modelTransformations.Children.Add(scale);
        }

        public void Reset()
        {
            if (_currentModel != null)
            {
                _modelTransformations.Children.Clear();
            }

            SetProjection(ProjectionType.Perspective);
        }
    }
}