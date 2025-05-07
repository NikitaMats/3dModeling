using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestCase_Sputnik.Models
{
    public class Transformations : INotifyPropertyChanged
    {
        private double _translateX;
        public double TranslateX
        {
            get => _translateX;
            set => SetField(ref _translateX, value);
        }

        private double _translateY;
        public double TranslateY
        {
            get => _translateY;
            set => SetField(ref _translateY, value);
        }

        private double _translateZ;
        public double TranslateZ
        {
            get => _translateZ;
            set => SetField(ref _translateZ, value);
        }

        private double _rotateX;
        public double RotateX
        {
            get => _rotateX;
            set => SetField(ref _rotateX, value);
        }

        private double _rotateY;
        public double RotateY
        {
            get => _rotateY;
            set => SetField(ref _rotateY, value);
        }

        private double _rotateZ;
        public double RotateZ
        {
            get => _rotateZ;
            set => SetField(ref _rotateZ, value);
        }

        private double _scale = 1.0;
        public double Scale
        {
            get => _scale;
            set => SetField(ref _scale, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}