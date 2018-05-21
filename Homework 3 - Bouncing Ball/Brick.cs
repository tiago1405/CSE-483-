//Tiago Zanaga Da Costa
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Drawing;

namespace BouncingBall
{
    public class Brick : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _brickName;
        public string BrickName
        {
            get { return _brickName; }
            set
            {
                _brickName = value;
                OnPropertyChanged("BrickName");
            }
        }

        private double _brickHeight;
        public double BrickHeight
        {
            get { return _brickHeight; }
            set
            {
                _brickHeight= value;
                OnPropertyChanged("BrickHeight");
            }
        }

        private double _brickWidth;
        public double BrickWidth
        {
            get { return _brickWidth; }
            set
            {
                _brickWidth = value;
                OnPropertyChanged("BrickWidth");
            }
        }

        private double _brickCanvasTop;
        public double BrickCanvasTop
        {
            get { return _brickCanvasTop; }
            set
            {
                _brickCanvasTop = value;
                OnPropertyChanged("BrickCanvasTop");
            }
        }

        private double _brickCanvasLeft;
        public double BrickCanvasLeft
        {
            get { return _brickCanvasLeft; }
            set
            {
                _brickCanvasLeft = value;
                OnPropertyChanged("BrickCanvasLeft");
            }
        }

        private Rectangle _brickRectangle;
        public Rectangle BrickRectangle
        {
            get { return _brickRectangle; }
            set { _brickRectangle = value; }
        }

        private string _brickFill;
        public string BrickFill
        {
            get { return _brickFill; }
            set
            {
                _brickFill = value;
                OnPropertyChanged("BrickFill");
            }
        }

        private System.Windows.Visibility _brickVisible;
        public System.Windows.Visibility BrickVisible
        {
            get { return _brickVisible; }
            set
            {
                _brickVisible = value;
                OnPropertyChanged("BrickVisible");
            }
        }
    }
}
