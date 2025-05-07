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
            set
            {
                if (value >= 0 && value <= 360)
                {
                    SetField(ref _rotateX, value);
                }
                else
                {
                    System.Windows.MessageBox.Show("The rotation angle must be greater than 0 and not more than 360");
                }
            }
        }

        private double _rotateY;
        public double RotateY
        {
            get => _rotateY;
            set
            {
                if (value >= 0 && value <= 360)
                {
                    SetField(ref _rotateY, value);
                }
                else
                {
                    System.Windows.MessageBox.Show("The rotation angle must be greater than 0 and not more than 360");
                }
            }
        }

        private double _rotateZ;
        public double RotateZ
        {
            get => _rotateZ;
            set
            {
                if (value >= 0 && value <= 360)
                {
                    SetField(ref _rotateZ, value);
                }
                else
                {
                    System.Windows.MessageBox.Show("The rotation angle must be greater than 0 and not more than 360");
                }
            }
        }

        private double _scale = 1.0;
        public double Scale
        {
            get => _scale;
            set
            {
                if (value > 0 && value <= 10)
                {
                    SetField(ref _scale, value);
                }
                else
                {
                    System.Windows.MessageBox.Show("The scale must be greater than 0 and not greater than 10");
                }
            }
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