using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System.ComponentModel;

namespace CSharpBlueprint.WinUI3.Controls
{

    /// <summary>
    /// 
    /// </summary>
    public partial class BindableBezierPath : Path
    {
        private readonly PathFigure _PathFigure = new();
        private readonly LineSegment _bezierSegment = new();

        public BindableBezierPath(ObservablePoint point1, ObservablePoint point2)
        {
            //Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 204, 204, 255));
            Stroke = new SolidColorBrush(Colors.White);
            StrokeThickness = 2;
            Point1 = point1;
            Point2 = point2;
            _PathFigure.StartPoint = new(point1.X, point1.Y);
            _PathFigure.IsClosed = false;
            _PathFigure.Segments.Add(_bezierSegment);
            Data = new GeometryGroup()
            {
                Children =
                [
                    new PathGeometry
                    {
                        Figures =
                        [
                            _PathFigure
                        ]
                    }
                ]
            };
        }

        public ObservablePoint Point1
        {
            get => field;
            set
            {
                if (field != value)
                {
                    field = value;
                    field.PropertyChanged += OnPoint1Changed;
                    _PathFigure.StartPoint = new(field.X, field.Y);
                }
            }
        }

        public ObservablePoint Point2
        {
            get => field;
            set
            {
                if (field != value)
                {
                    field = value;
                    field.PropertyChanged += OnPoint2Changed;
                    _bezierSegment.Point = new(field.X, field.Y);
                }
            }
        }

        private void OnPoint1Changed(object? sender, PropertyChangedEventArgs e)
        {
            _PathFigure.StartPoint = new(Point1.X, Point1.Y);
        }

        private void OnPoint2Changed(object? sender, PropertyChangedEventArgs e)
        {
            _bezierSegment.Point = new(Point2.X, Point2.Y);
        }
    }
}
