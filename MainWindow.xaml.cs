using System;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Collections.Generic;
using System.Windows.Media;

namespace TestCase_Sputnik
{
    public partial class MainWindow : Window
    {
        private readonly CustomModel3D _originalModel;
        private CustomModel3D _currentModel;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Проверка инициализации Viewport
                if (Viewport == null)
                    throw new InvalidOperationException("Viewport not initialized in XAML");

                _originalModel = new CustomModel3D();
                ResetModel();
                DrawModel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization error: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void ResetModel()
        {
            _currentModel = new CustomModel3D
            {
                Vertices = new List<Point3D>(_originalModel.Vertices),
                Edges = new List<(int, int)>(_originalModel.Edges)
            };
        }

        private void DrawModel()
        {
            if (Viewport == null || _currentModel == null) return;

            try
            {
                Viewport.Children.Clear();

                // Отрисовка модели
                foreach (var edge in _currentModel.Edges)
                {
                    if (edge.Start >= _currentModel.Vertices.Count ||
                        edge.End >= _currentModel.Vertices.Count)
                        continue;

                    var line = new ScreenSpaceLines3D
                    {
                        Color = Colors.Blue,
                        Thickness = 2
                    };
                    line.SetPoints(
                        _currentModel.Vertices[edge.Start],
                        _currentModel.Vertices[edge.End]);

                    Viewport.Children.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rendering error: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            if (txtTranslateX == null || !double.TryParse(txtTranslateX.Text, out double dx))
            {
                MessageBox.Show("Invalid X value", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Transformations.Translate(_currentModel, dx, 0, 0);
            DrawModel();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetModel();
            DrawModel();
        }
    }
}