using TestCase_Sputnik.Models;
using TestCase_Sputnik.ViewModels.Base;
using TestCase_Sputnik.ViewModels.Commands;
using TestCase_Sputnik.Services.Interfaces;

namespace TestCase_Sputnik.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly I3DRenderService _renderService;

        private Transformations _transformations;
        public Transformations Transformations
        {
            get => _transformations;
            set => Set(ref _transformations, value);
        }

        private ProjectionType _selectedProjection;
        public ProjectionType SelectedProjection
        {
            get => _selectedProjection;
            set
            {
                Set(ref _selectedProjection, value);
                _renderService.SetProjection(value);
            }
        }

        public RelayCommand TranslateCommand { get; }
        public RelayCommand RotateXCommand { get; }
        public RelayCommand RotateYCommand { get; }
        public RelayCommand RotateZCommand { get; }
        public RelayCommand ScaleCommand { get; }
        public RelayCommand MirrorXYCommand { get; }
        public RelayCommand MirrorXZCommand { get; }
        public RelayCommand MirrorYZCommand { get; }
        public RelayCommand ResetCommand { get; }

        public MainViewModel(I3DRenderService renderService)
        {
            _renderService = renderService;
            Transformations = new Transformations();

            TranslateCommand = new RelayCommand(ExecuteTranslate);
            RotateXCommand = new RelayCommand(ExecuteRotateX);
            RotateYCommand = new RelayCommand(ExecuteRotateY);
            RotateZCommand = new RelayCommand(ExecuteRotateZ);
            ScaleCommand = new RelayCommand(ExecuteScale);
            MirrorXYCommand = new RelayCommand(() => _renderService.Mirror(MirrorType.XY));
            MirrorXZCommand = new RelayCommand(() => _renderService.Mirror(MirrorType.XZ));
            MirrorYZCommand = new RelayCommand(() => _renderService.Mirror(MirrorType.YZ));
            ResetCommand = new RelayCommand(ExecuteReset);

            _renderService.InitializeScene();
            _renderService.Reset();
        }

        private void ExecuteTranslate()
        {
            _renderService.Translate(
                Transformations.TranslateX,
                Transformations.TranslateY,
                Transformations.TranslateZ);
        }

        private void ExecuteRotateX()
        {
            if (Transformations.RotateX >= 0 && Transformations.RotateX <= 360)
            {
                _renderService.Rotate(RotationAxis.X, Transformations.RotateX);
            }
            else
            {
                System.Windows.MessageBox.Show("Enter a valid angle value (0 ≤ scale ≤ 360)");
                Transformations.RotateX = 0;
            }
        }

        private void ExecuteRotateY()
        {
            if (Transformations.RotateY >= 0 && Transformations.RotateY <= 360)
            {
                _renderService.Rotate(RotationAxis.Y, Transformations.RotateY);
            }
            else
            {
                System.Windows.MessageBox.Show("Enter a valid angle value (0 ≤ scale ≤ 360)");
                Transformations.RotateY = 0;
            }
        }

        private void ExecuteRotateZ()
        {
            if (Transformations.RotateZ >= 0 && Transformations.RotateZ <= 360)
            {
                _renderService.Rotate(RotationAxis.Z, Transformations.RotateZ);
            }
            else
            {
                System.Windows.MessageBox.Show("Enter a valid angle value (0 ≤ scale ≤ 360)");
                Transformations.RotateZ = 0;
            }
        }

        private void ExecuteScale()
        {
            if (Transformations.Scale > 0 && Transformations.Scale <= 10)
            {
                _renderService.Scale(Transformations.Scale);
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter a valid scale value (0 < scale ≤ 10)");
            }
        }

        private void ExecuteReset()
        {
            Transformations = new Transformations();
            SelectedProjection = ProjectionType.Perspective;
            _renderService.Reset();
        }
    }
}