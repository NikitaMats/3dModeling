using System.Windows.Controls;
using TestCase_Sputnik.Models;

namespace TestCase_Sputnik.Services.Interfaces
{
    public interface I3DRenderService
    {
        void InitializeScene();
        void SetProjection(ProjectionType projectionType);
        void Translate(double x, double y, double z);
        void Rotate(RotationAxis axis, double angle);
        void Scale(double factor);
        void Mirror(MirrorType mirrorType);
        void Reset();

        Viewport3D Viewport { get; }
    }
}