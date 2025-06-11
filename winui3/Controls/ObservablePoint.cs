using CommunityToolkit.Mvvm.ComponentModel;

namespace CSharpBlueprint.WinUI3.Controls
{
    public partial class ObservablePoint(double x, double y) : ObservableObject
    {
        [ObservableProperty]
        public partial double X { get; set; } = x;

        [ObservableProperty]
        public partial double Y { get; set; } = y;

        public static ObservablePoint operator +(ObservablePoint left, ObservablePoint right)
        {
            return new ObservablePoint(left.X + right.X, left.Y + right.Y);
        }

        public static ObservablePoint operator -(ObservablePoint value)
        {
            return new ObservablePoint(-value.X, -value.Y);
        }

        public static ObservablePoint operator -(ObservablePoint left, ObservablePoint right)
        {
            return new ObservablePoint(left.X - right.X, left.Y - right.Y);
        }

        public static ObservablePoint operator *(float left, ObservablePoint right)
        {
            return new ObservablePoint(left * right.X, left * right.Y);
        }

        public static ObservablePoint operator /(ObservablePoint value1, float value2)
        {
            return new ObservablePoint(value1.X / value2, value1.Y / value2);
        }


    }
}
