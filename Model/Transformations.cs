using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik.Model
{
    public class Transformations
    {
        public static void Translate(Cube model, double dx, double dy, double dz)
        {
            for (int i = 0; i < model.Vertices.Count; i++)
            {
                model.Vertices[i] = new Point3D(
                    model.Vertices[i].X + dx,
                    model.Vertices[i].Y + dy,
                    model.Vertices[i].Z + dz);
            }
        }

        public static void Scale(Cube model, double sx, double sy, double sz)
        {
            for (int i = 0; i < model.Vertices.Count; i++)
            {
                model.Vertices[i] = new Point3D(
                    model.Vertices[i].X * sx,
                    model.Vertices[i].Y * sy,
                    model.Vertices[i].Z * sz);
            }
        }

        public static void RotateX(Cube model, double angle)
        {
            double rad = angle * Math.PI / 180;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            for (int i = 0; i < model.Vertices.Count; i++)
            {
                double y = model.Vertices[i].Y * cos - model.Vertices[i].Z * sin;
                double z = model.Vertices[i].Y * sin + model.Vertices[i].Z * cos;
                model.Vertices[i] = new Point3D(model.Vertices[i].X, y, z);
            }
        }

        // Аналогично для RotateY и RotateZ
    }
}
