//Tiago Zanaga Da Costa
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// observable collections
using System.Collections.ObjectModel;

// debug output
using System.Diagnostics;

// timer, sleep
using System.Threading;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

// hi res timer
using PrecisionTimers;
using System.Threading;

// Rectangle
// Must update References manually
using System.Drawing;

// INotifyPropertyChanged
using System.ComponentModel;

namespace BouncingBall
{
    public partial class Model : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Brick> BrickCollection;
        private static UInt32 _numBalls = 1;
        private UInt32[] _buttonPresses = new UInt32[_numBalls];
        private static UInt32 _colBricks = 15;
        private static UInt32 _rowBricks = 5;
        private static Thread _gameTime;
        Random _randomNumber = new Random();
        private TimerQueueTimer.WaitOrTimerDelegate _ballTimerCallbackDelegate;
        private TimerQueueTimer.WaitOrTimerDelegate _paddleTimerCallbackDelegate;
        private TimerQueueTimer _ballHiResTimer;
        private TimerQueueTimer _paddleHiResTimer;
        private double _ballXMove = 1;
        private double _ballYMove = 1;
        System.Drawing.Rectangle _ballRectangle;
        System.Drawing.Rectangle _paddleRectangle;
        bool _movepaddleLeft = false;
        bool _movepaddleRight = false;

        private bool _moveBall = false;
        public bool MoveBall
        {
            get { return _moveBall; }
            set { _moveBall = value; }
        }

        private double _windowHeight = 100;
        public double WindowHeight
        {
            get { return _windowHeight; }
            set
            {
                _windowHeight = value;
                OnPropertyChanged("WindowHeight");
            }
        }

        private double _windowWidth = 100;
        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value;
                OnPropertyChanged("WindowWidth");
            }
        }

        private int _timeElapsed = 0;
        public int timeElapsed
        {
            get { return _timeElapsed; }
            set
            {
                _timeElapsed = value;
                OnPropertyChanged("timeElapsed");
            }
        }

        private int _score;
        public int score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("score");
            }
        }

        private bool _timerBool = false;
        public bool timerBool
        {
            get { return _timerBool; }
            set
            {
                _timerBool = value;
                OnPropertyChanged("timerBool");
            }
        }

        /// <summary>
        /// Model constructor
        /// </summary>
        /// <returns></returns>
        public Model()
        {
          
        }

        public void InitModel()
        {
            // this delegate is needed for the multi media timer defined 
            // in the TimerQueueTimer class.
            _ballTimerCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(BallMMTimerCallback);
            _paddleTimerCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(paddleMMTimerCallback);

            // create our multi-media timers
            _ballHiResTimer = new TimerQueueTimer();
            try
            {
                // create a Multi Media Hi Res timer.
                _ballHiResTimer.Create(1, 2, _ballTimerCallbackDelegate);
            }
            catch (QueueTimerException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Failed to create Ball timer. Error from GetLastError = {0}", ex.Error);
            }

            _paddleHiResTimer = new TimerQueueTimer();
            try
            {
                // create a Multi Media Hi Res timer.
                _paddleHiResTimer.Create(2, 2, _paddleTimerCallbackDelegate);
            }
            catch (QueueTimerException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Failed to create paddle timer. Error from GetLastError = {0}", ex.Error);
            }

            int count = 0;
            BrickCollection = new ObservableCollection<Brick>();
            for (int i = 0; i < _colBricks; i++)
            {
                for (int j = 0; j < _rowBricks; j++)
                {
                    BrickCollection.Add(new Brick()
                    {
                        BrickHeight = 25,
                        BrickWidth = 50,
                        BrickCanvasLeft = (i*50),
                        BrickCanvasTop = (j*25),
                        BrickName = (count).ToString(),
                        BrickFill = "#ff5185",
                        BrickVisible = System.Windows.Visibility.Visible
                    });
                }
            }
            UpdateRects();
        }

        private void UpdateRects()
        {
            for (int i = 0; i < (_colBricks * _rowBricks); i++)
            {
                BrickCollection[i].BrickRectangle = new System.Drawing.Rectangle((int)BrickCollection[i].BrickCanvasLeft,
                    (int)BrickCollection[i].BrickCanvasTop, (int)BrickCollection[i].BrickWidth, (int)BrickCollection[i].BrickHeight);
            }
        }

        public void CleanUp()
        {
            _ballHiResTimer.Delete();
            _paddleHiResTimer.Delete();
            _gameTime.Abort();
        }


        public void SetStartPosition()
        {
            
            BallHeight = 30;
            BallWidth = 30;
            paddleWidth = 120;
            paddleHeight = 10;

            score = 0;
            timeElapsed = 0;

            ballCanvasLeft = _windowWidth/2 - BallWidth/2;
            ballCanvasTop = _windowHeight/5 + (_colBricks*_windowHeight/100);

            _moveBall = false;
            _timeElapsed = 0;

            _gameTime = new Thread(new ThreadStart(gameTime));
            _gameTime.Start();

            paddleCanvasLeft = _windowWidth / 2 - paddleWidth / 2;
            paddleCanvasTop = _windowHeight - paddleHeight;
            _paddleRectangle = new System.Drawing.Rectangle((int)paddleCanvasLeft, (int)paddleCanvasTop, (int)paddleWidth, (int)paddleHeight);
            for(int i = 0; i < (_colBricks * _rowBricks); i++)
            {
                BrickCollection[i].BrickVisible = Visibility.Visible;
            }
        }

        private void gameTime()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    if (MoveBall)
                    {
                        timeElapsed += 1;
                    }
                }
            }
            catch(ThreadAbortException)
            {
                Debug.Write("GameTime Thread Aborted\n");
            }
        }

        public void MoveLeft(bool move)
        {
            _movepaddleLeft = move;
        }

        public void MoveRight(bool move)
        {
            _movepaddleRight = move;
        }


        private void BallMMTimerCallback(IntPtr pWhat, bool success)
        {

            if (!_moveBall)
                return;

            if (!_ballHiResTimer.ExecutingCallback())
            {
                Console.WriteLine("Aborting timer callback.");
                return;
            }

            ballCanvasLeft += _ballXMove;
            ballCanvasTop += _ballYMove;

            // check to see if ball has it the left or right side of the drawing element
            if ((ballCanvasLeft + BallWidth >= _windowWidth) ||
                (ballCanvasLeft <= 0))
                _ballXMove = -_ballXMove;


            // check to see if ball has it the top of the drawing element
            if ( ballCanvasTop <= 0) 
                _ballYMove = -_ballYMove;

            if (ballCanvasTop + BallWidth >= _windowHeight)
            {
                // we hit bottom. stop moving the ball
                _moveBall = false;
            }

            // see if we hit the paddle
            _ballRectangle = new System.Drawing.Rectangle((int)ballCanvasLeft, (int)ballCanvasTop, (int)BallWidth, (int)BallHeight);
            if (_ballRectangle.IntersectsWith(_paddleRectangle))
            {
                // hit paddle. reverse direction in Y direction
                _ballYMove = -_ballYMove;

                // move the ball away from the paddle so we don't intersect next time around and
                // get stick in a loop where the ball is bouncing repeatedly on the paddle
                ballCanvasTop += 2*_ballYMove;

                // add move the ball in X some small random value so that ball is not traveling in the same 
                // pattern
                ballCanvasLeft += _randomNumber.Next(5);
            }

            for (int i = 0; i < (_colBricks * _rowBricks); i++)
            {
                if(BrickCollection[i].BrickVisible == Visibility.Hidden)
                {
                    continue;
                }
                else if (_ballRectangle.IntersectsWith(BrickCollection[i].BrickRectangle))
                {
                    if (_ballYMove < 0)
                    { _ballYMove = -_ballYMove; }
                    BrickCollection[i].BrickVisible = System.Windows.Visibility.Hidden;
                    score += 10;
                }
            }
            // done in callback. OK to delete timer
            _ballHiResTimer.DoneExecutingCallback();
        }

        private void paddleMMTimerCallback(IntPtr pWhat, bool success)
        {

            if (!_paddleHiResTimer.ExecutingCallback())
            {
                Console.WriteLine("Aborting timer callback.");
                return;
            }

            if (_movepaddleLeft && paddleCanvasLeft > 0)
                paddleCanvasLeft -= 2;
            else if (_movepaddleRight && paddleCanvasLeft < _windowWidth - paddleWidth)
                paddleCanvasLeft += 2;
            
            _paddleRectangle = new System.Drawing.Rectangle((int)paddleCanvasLeft, (int)paddleCanvasTop, (int)paddleWidth, (int)paddleHeight);


            // done in callback. OK to delete timer
            _paddleHiResTimer.DoneExecutingCallback();
        }
          
    }
}
