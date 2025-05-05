using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов XAML
            InitializeScene();    // Настройка 3D-сцены
        }

        private void InitializeScene()
        {
            // Проверка инициализации Viewport
            if (Viewport == null)
            {
                MessageBox.Show("Ошибка инициализации 3D-области");
                Close();
                return;
            }

            // Добавление тестового куба при запуске
            AddCube();
        }

        private void AddCube_Click(object sender, RoutedEventArgs e)
        {
            AddCube();
        }

        private void AddCube()
        {
            var cube = CubeGenerator.CreateTexturedCube(
                size: 2.0,
                textureBrush: CubeGenerator.CreateBlueTexture());

            Viewport.Children.Add(cube);
        }

        private void ResetScene_Click(object sender, RoutedEventArgs e)
        {
            Viewport.Children.Clear(); // Очистка сцены

            // Восстановление освещения
            var light = new ModelVisual3D
            {
                Content = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1))
            };
            Viewport.Children.Add(light);
        }
    }
}