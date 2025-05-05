using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    public static class CubeGenerator
    {
        public static ModelVisual3D CreateTexturedCube(double size, Brush textureBrush)
        {
            double halfSize = size / 2;

            // 1. Создаем геометрию куба (8 вершин)
            var mesh = new MeshGeometry3D
            {
                Positions = new Point3DCollection
                {
                    // Нижняя грань
                    new Point3D(-halfSize, -halfSize, -halfSize),
                    new Point3D(halfSize, -halfSize, -halfSize),
                    new Point3D(halfSize, halfSize, -halfSize),
                    new Point3D(-halfSize, halfSize, -halfSize),
                    
                    // Верхняя грань
                    new Point3D(-halfSize, -halfSize, halfSize),
                    new Point3D(halfSize, -halfSize, halfSize),
                    new Point3D(halfSize, halfSize, halfSize),
                    new Point3D(-halfSize, halfSize, halfSize)
                },

                // Индексы треугольников (12 граней)
                TriangleIndices = new Int32Collection
                {
                    // Нижняя грань
                    0, 1, 2, 0, 2, 3,
                    // Верхняя грань
                    4, 5, 6, 4, 6, 7,
                    // Боковые грани
                    0, 1, 5, 0, 5, 4,
                    1, 2, 6, 1, 6, 5,
                    2, 3, 7, 2, 7, 6,
                    3, 0, 4, 3, 4, 7
                },

                // Координаты текстуры
                TextureCoordinates = new PointCollection
                {
                    new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1),
                    new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1)
                }
            };

            // 2. Создаем материал с текстурой
            var material = new DiffuseMaterial(textureBrush);

            // 3. Собираем 3D-модель
            var model = new GeometryModel3D(mesh, material);

            // 4. Возвращаем визуальный элемент
            return new ModelVisual3D { Content = model };
        }

        public static Brush CreateBlueTexture()
        {
            // Градиентная синяя текстура
            return new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.DarkBlue, 0),
                    new GradientStop(Colors.Blue, 0.5),
                    new GradientStop(Colors.LightBlue, 1)
                }
            };
        }
    }
}