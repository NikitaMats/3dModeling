using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    public static class FrustumGenerator
    {
        public static ModelVisual3D CreateWireframeFrustum(double bottomSize, double topSize, double height)
        {
            var group = new Model3DGroup();
            Point3D[] bottomPoints = CreateBaseVertices(bottomSize, -height / 2);
            Point3D[] topPoints = CreateBaseVertices(topSize, height / 2);

            for (int i = 0; i < 4; i++)
                AddThickLine(group, bottomPoints[i], bottomPoints[(i + 1) % 4], Colors.Red);

            for (int i = 0; i < 4; i++)
                AddThickLine(group, topPoints[i], topPoints[(i + 1) % 4], Colors.Blue);

            for (int i = 0; i < 4; i++)
                AddThickLine(group, bottomPoints[i], topPoints[i], Colors.Green);

            return new ModelVisual3D { Content = group };
        }

        private static void AddThickLine(Model3DGroup group, Point3D start, Point3D end, Color color)
        {
            var line = new ThickLine3D { Color = color, Thickness = 0.1 };
            line.SetPoints(start, end);
            group.Children.Add((GeometryModel3D)line.Content);
        }

        private static Point3D[] CreateBaseVertices(double size, double y)
        {
            double half = size / 2;
            return new Point3D[]
            {
                new Point3D(-half, y, -half),
                new Point3D(half, y, -half),
                new Point3D(half, y, half),
                new Point3D(-half, y, half)
            };
        }
    }
}