using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik.Model
{
    public class Cube
    {
        public List<Point3D> Vertices { get; set; }
        public List<Tuple<int, int>> Edges { get; set; }

        public Cube()
        {
            // Инициализация куба
            Vertices = new List<Point3D>
        {
            new Point3D(-1, -1, -1),
            new Point3D(1, -1, -1),
            new Point3D(1, 1, -1),
            new Point3D(-1, 1, -1),
            new Point3D(-1, -1, 1),
            new Point3D(1, -1, 1),
            new Point3D(1, 1, 1),
            new Point3D(-1, 1, 1)
        };

            Edges = new List<Tuple<int, int>>
        {
            Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3), Tuple.Create(3, 0),
            Tuple.Create(4, 5), Tuple.Create(5, 6), Tuple.Create(6, 7), Tuple.Create(7, 4),
            Tuple.Create(0, 4), Tuple.Create(1, 5), Tuple.Create(2, 6), Tuple.Create(3, 7)
        };
        }
    }
}
