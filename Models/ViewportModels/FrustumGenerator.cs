using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik.Models.ViewportModels
{
    public static class FrustumGenerator
    {
        public static ModelVisual3D CreateTriangularFrustum(double bottomSize, double topSize, double height)
        {
            var group = new Model3DGroup();

            Point3D[] bottomPoints = CreateTriangleVertices(bottomSize, -height / 2);

            Point3D[] topPoints = CreateTriangleVertices(topSize, height / 2);

 
            for (int i = 0; i < 3; i++)
                AddThickLine(group, bottomPoints[i], bottomPoints[(i + 1) % 3], Colors.Red);

  
            for (int i = 0; i < 3; i++)
                AddThickLine(group, topPoints[i], topPoints[(i + 1) % 3], Colors.Blue);

 
            for (int i = 0; i < 3; i++)
                AddThickLine(group, bottomPoints[i], topPoints[i], Colors.Green);

            return new ModelVisual3D { Content = group };
        }

        private static Point3D[] CreateTriangleVertices(double size, double y)
        {
            double radius = size / Math.Sqrt(3);
            return new Point3D[]
            {
                new Point3D(0, y, radius), // Верхняя вершина
                new Point3D(radius * Math.Cos(7 * Math.PI / 6), y, radius * Math.Sin(7 * Math.PI / 6)), // Левая нижняя
                new Point3D(radius * Math.Cos(11 * Math.PI / 6), y, radius * Math.Sin(11 * Math.PI / 6)) // Правая нижняя
            };
        }

        private static void AddThickLine(Model3DGroup group, Point3D start, Point3D end, Color color)
        {
            var line = new ThickLine3D { Color = color, Thickness = 0.1 };
            line.SetPoints(start, end);
            group.Children.Add((GeometryModel3D)line.Content);
        }
    }
}