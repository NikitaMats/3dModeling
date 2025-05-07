using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    public partial class MainWindow : Window
    {
        private ModelVisual3D currentModel;
        private Transform3DGroup modelTransformations;

        public MainWindow()
        {
            InitializeComponent();
            InitializeScene();
        }

        #region General functionality

        private void InitializeScene()
        {
            if (Viewport == null)
            {
                MessageBox.Show("Ошибка инициализации 3D-области");
                Close();
                return;
            }

            AddCoordinateAxes();
            AddFrustum();
        }

        private void AddThickLine(Model3DGroup group, Point3D start, Point3D end, Color color)
        {
            var line = new ThickLine3D { Color = color, Thickness = 0.04 };
            line.SetPoints(start, end);
            group.Children.Add((GeometryModel3D)line.Content);
        }

        private void AddCoordinateAxes()
        {
            var axes = new Model3DGroup();

            AddThickLine(axes, new Point3D(-4, -4, -4), new Point3D(5, -4, -4), Colors.Red);
            AddThickLine(axes, new Point3D(-4, -4, -4), new Point3D(-4, 5, -4), Colors.Green);
            AddThickLine(axes, new Point3D(-4, -4, -4), new Point3D(-4, -4, 5), Colors.Blue);

            Viewport.Children.Add(new ModelVisual3D { Content = axes });
        }

        private void AddFrustum()
        {
            currentModel = FrustumGenerator.CreateWireframeFrustum(
                bottomSize: 3.0,
                topSize: 1.5,
                height: 2.0);

            modelTransformations = new Transform3DGroup();
            currentModel.Transform = modelTransformations;

            Viewport.Children.Add(currentModel);
        }

        private void ResetScene_Click(object sender, RoutedEventArgs e)
        {
            Viewport.Children.Clear();

            var light = new ModelVisual3D
            {
                Content = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1))
            };
            Viewport.Children.Add(light);

            AddCoordinateAxes();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel != null)
            {
                modelTransformations.Children.Clear();
            }

            Viewport.Camera = new PerspectiveCamera
            {
                Position = new Point3D(8, 8, 8),
                LookDirection = new Vector3D(-1, -1, -1),
                UpDirection = new Vector3D(0, 1, 0),
                FieldOfView = 45,
                NearPlaneDistance = 0.1,
                FarPlaneDistance = 100
            };

            rbPerspective.IsChecked = true;
        }

        #endregion

        #region Transformation Methods

        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            if (double.TryParse(txtTranslateX.Text, out double x) &&
                double.TryParse(txtTranslateY.Text, out double y) &&
                double.TryParse(txtTranslateZ.Text, out double z))
            {
                var translation = new TranslateTransform3D(x, y, z);
                modelTransformations.Children.Add(translation);
            }
            else
            {
                MessageBox.Show("Invalid translation values");
            }
        }

        private void RotateX_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            if (double.TryParse(txtRotateX.Text, out double angle))
            {
                var rotation = new RotateTransform3D(
                    new AxisAngleRotation3D(new Vector3D(1, 0, 0), angle));
                modelTransformations.Children.Add(rotation);
            }
            else
            {
                MessageBox.Show("Invalid rotation angle");
            }
        }

        private void RotateY_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            if (double.TryParse(txtRotateY.Text, out double angle))
            {
                var rotation = new RotateTransform3D(
                    new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle));
                modelTransformations.Children.Add(rotation);
            }
            else
            {
                MessageBox.Show("Invalid rotation angle");
            }
        }

        private void RotateZ_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            if (double.TryParse(txtRotateZ.Text, out double angle))
            {
                var rotation = new RotateTransform3D(
                    new AxisAngleRotation3D(new Vector3D(0, 0, 1), angle));
                modelTransformations.Children.Add(rotation);
            }
            else
            {
                MessageBox.Show("Invalid rotation angle");
            }
        }

        private void Scale_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            if (double.TryParse(txtScale.Text, out double scale) && scale > 0)
            {
                var scaleTransform = new ScaleTransform3D(scale, scale, scale);
                modelTransformations.Children.Add(scaleTransform);
            }
            else
            {
                MessageBox.Show("Invalid scale value");
            }
        }

        private void MirrorXY_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            var scale = new ScaleTransform3D(1, 1, -1);
            modelTransformations.Children.Add(scale);
        }

        private void MirrorXZ_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            var scale = new ScaleTransform3D(1, -1, 1);
            modelTransformations.Children.Add(scale);
        }

        private void MirrorYZ_Click(object sender, RoutedEventArgs e)
        {
            if (currentModel == null) return;

            var scale = new ScaleTransform3D(-1, 1, 1);
            modelTransformations.Children.Add(scale);
        }

        #endregion

        #region Projection Methods

        private void Projection_Changed(object sender, RoutedEventArgs e)
        {
            if (Viewport == null) return;
            if (rbPerspective?.IsChecked == true)
            {
                Viewport.Camera = new PerspectiveCamera
                {
                    Position = new Point3D(8, 8, 8),
                    LookDirection = new Vector3D(-1, -1, -1),
                    UpDirection = new Vector3D(0, 1, 0),
                    FieldOfView = 45,
                    NearPlaneDistance = 0.1,
                    FarPlaneDistance = 100
                };
            }
            else if (rbOrthographic?.IsChecked == true)
            {
                Viewport.Camera = new OrthographicCamera
                {
                    Position = new Point3D(8, 8, 8),
                    LookDirection = new Vector3D(-1, -1, -1),
                    UpDirection = new Vector3D(0, 1, 0),
                    Width = 10,
                    NearPlaneDistance = 0.1,
                    FarPlaneDistance = 100
                };
            }
            else if (rbOrthographicFront?.IsChecked == true)
            {
                Viewport.Camera = new OrthographicCamera
                {
                    Position = new Point3D(0, 0, -15),
                    LookDirection = new Vector3D(0, 0, 1),
                    UpDirection = new Vector3D(0, 1, 0),
                    Width = 10,
                    NearPlaneDistance = 0.1,
                    FarPlaneDistance = 100
                };
            }
        }

        #endregion



    }
}