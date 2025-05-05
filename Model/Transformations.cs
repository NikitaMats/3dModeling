using System;
using System.Windows.Media.Media3D;

namespace TestCase_Sputnik
{
    /// <summary>
    /// Статический класс для геометрических преобразований
    /// </summary>
    public static class Transformations
    {
        /// <summary>
        /// Перенос модели вдоль осей
        /// </summary>
        public static void Translate(CustomModel3D model, double dx, double dy, double dz)
        {
            for (int i = 0; i < model.Vertices.Count; i++)
            {
                model.Vertices[i] = new Point3D(
                    model.Vertices[i].X + dx,
                    model.Vertices[i].Y + dy,
                    model.Vertices[i].Z + dz);
            }
        }

        /// <summary>
        /// Масштабирование модели
        /// </summary>
        public static void Scale(CustomModel3D model, double scaleFactor)
        {
            for (int i = 0; i < model.Vertices.Count; i++)
            {
                model.Vertices[i] = new Point3D(
                    model.Vertices[i].X * scaleFactor,
                    model.Vertices[i].Y * scaleFactor,
                    model.Vertices[i].Z * scaleFactor);
            }
        }

        /// <summary>
        /// Вращение вокруг оси X
        /// </summary>
        public static void RotateX(CustomModel3D model, double angleDegrees)
        {
            double angleRad = angleDegrees * Math.PI / 180;
            double cos = Math.Cos(angleRad);
            double sin = Math.Sin(angleRad);

            for (int i = 0; i < model.Vertices.Count; i++)
            {
                double y = model.Vertices[i].Y * cos - model.Vertices[i].Z * sin;
                double z = model.Vertices[i].Y * sin + model.Vertices[i].Z * cos;
                model.Vertices[i] = new Point3D(model.Vertices[i].X, y, z);
            }
        }

        /// <summary>
        /// Вращение вокруг оси Y
        /// </summary>
        public static void RotateY(CustomModel3D model, double angleDegrees)
        {
            double angleRad = angleDegrees * Math.PI / 180;
            double cos = Math.Cos(angleRad);
            double sin = Math.Sin(angleRad);

            for (int i = 0; i < model.Vertices.Count; i++)
            {
                double x = model.Vertices[i].Z * sin + model.Vertices[i].X * cos;
                double z = model.Vertices[i].Z * cos - model.Vertices[i].X * sin;
                model.Vertices[i] = new Point3D(x, model.Vertices[i].Y, z);
            }
        }

        /// <summary>
        /// Вращение вокруг оси Z
        /// </summary>
        public static void RotateZ(CustomModel3D model, double angleDegrees)
        {
            double angleRad = angleDegrees * Math.PI / 180;
            double cos = Math.Cos(angleRad);
            double sin = Math.Sin(angleRad);

            for (int i = 0; i < model.Vertices.Count; i++)
            {
                double x = model.Vertices[i].X * cos - model.Vertices[i].Y * sin;
                double y = model.Vertices[i].X * sin + model.Vertices[i].Y * cos;
                model.Vertices[i] = new Point3D(x, y, model.Vertices[i].Z);
            }
        }
    }
}