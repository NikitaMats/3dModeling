using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    public class CustomModel3D
    {
        public List<Point3D> Vertices { get; set; }
        public List<(int Start, int End)> Edges { get; set; }

        public CustomModel3D()
        {
            InitializeCube();
        }

        private void InitializeCube()
        {
            Vertices = new List<Point3D>
            {
                new Point3D(-1, -1, -1), // 0
                new Point3D(1, -1, -1),  // 1
                new Point3D(1, 1, -1),   // 2
                new Point3D(-1, 1, -1),  // 3
                new Point3D(-1, -1, 1),  // 4
                new Point3D(1, -1, 1),   // 5
                new Point3D(1, 1, 1),    // 6
                new Point3D(-1, 1, 1)    // 7
            };

            Edges = new List<(int, int)>
            {
                (0, 1), (1, 2), (2, 3), (3, 0), // Нижняя грань
                (4, 5), (5, 6), (6, 7), (7, 4), // Верхняя грань
                (0, 4), (1, 5), (2, 6), (3, 7)  // Боковые ребра
            };
        }
    }
}