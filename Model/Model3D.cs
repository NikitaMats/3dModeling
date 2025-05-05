using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    /// <summary>
    /// Класс для хранения 3D-модели (куб по умолчанию)
    /// </summary>
    public class CustomModel3D
    {
        /// <summary>
        /// Коллекция вершин модели
        /// </summary>
        public List<Point3D> Vertices { get; set; } = new List<Point3D>();

        /// <summary>
        /// Коллекция рёбер как пар индексов вершин
        /// </summary>
        public List<(int Start, int End)> Edges { get; set; } = new List<(int, int)>();

        /// <summary>
        /// Конструктор создаёт куб 2x2x2
        /// </summary>
        public CustomModel3D()
        {
            // Инициализация 8 вершин куба
            Vertices.AddRange(new[]
            {
                // Нижняя грань
                new Point3D(-1, -1, -1), // 0
                new Point3D(1, -1, -1),  // 1
                new Point3D(1, 1, -1),   // 2
                new Point3D(-1, 1, -1),  // 3
                
                // Верхняя грань
                new Point3D(-1, -1, 1),  // 4
                new Point3D(1, -1, 1),   // 5
                new Point3D(1, 1, 1),    // 6
                new Point3D(-1, 1, 1)    // 7
            });

            // Инициализация 12 рёбер куба
            Edges.AddRange(new[]
            {
                // Нижняя грань
                (0, 1), (1, 2), (2, 3), (3, 0),
                
                // Верхняя грань
                (4, 5), (5, 6), (6, 7), (7, 4),
                
                // Вертикальные рёбра
                (0, 4), (1, 5), (2, 6), (3, 7)
            });
        }
    }
}