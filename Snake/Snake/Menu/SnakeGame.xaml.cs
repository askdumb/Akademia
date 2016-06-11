using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Snake
{

    public partial class SnakeGame : UserControl, ISwitchable
    {

        private TimeSpan timerSpeed = new TimeSpan(Settings.timerSpeed);
        private List<PlusFood> plusFood = new List<PlusFood>();
        private List<MinusFood> minusFood = new List<MinusFood>();
        private List<Point> snakeBody = new List<Point>();
        private Random random = new Random();
        private DispatcherTimer gameTimer;
        private Point currentPosition;


        public SnakeGame()
        {
            InitializeComponent();
            StartGame();
        }


        private void StartGame()
        {
            snakeBody.Clear();
            plusFood.Clear();
            minusFood.Clear();
            myCanvas.Children.Clear();
            new Settings();
            Timer();
            Paint();
            currentPosition = Settings.head;
        }


        private void Timer()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Tick += UpdateWindow;
            gameTimer.Interval = timerSpeed;
            gameTimer.Start();

        }


        private void Paint()
        {
            DrawSnake(Settings.head);

            // Generate food
            for (int i = 0; i < 5; i++)
            {
                plusFood.Insert(i, new PlusFood(NewFoodPosition(myCanvas)));
                myCanvas.Children.Insert(i, plusFood[i].FeedBack());

                minusFood.Insert(i, new MinusFood(NewFoodPosition(myCanvas)));
                myCanvas.Children.Insert(i, minusFood[i].FeedBack());
            }
        }


        private void DrawSnake(Point position)
        {
            SnakePart bodyPart = new SnakePart(position);
            myCanvas.Children.Add(bodyPart.FeedBack());
            snakeBody.Add(position);

            int elementsCount = myCanvas.Children.Count;

            if (elementsCount >= Settings.snakeLength)
            {
                myCanvas.Children.RemoveAt(elementsCount - Settings.snakeLength + plusFood.Count + minusFood.Count);
                snakeBody.RemoveAt(elementsCount - Settings.snakeLength);
            }
            
            // Update score
            if (Settings.score < 0) Settings.score = 0;
            lblScore.Content = "Score: " + Settings.score.ToString();
        }     


        private Point NewFoodPosition(Canvas screen)
        {
            Point randFoodPosition = new Point();

            randFoodPosition.X = random.Next(5, (int)screen.Width - 50);
            randFoodPosition.Y = random.Next(5, (int)screen.Height - 50);

            return randFoodPosition;
        }


        private void UpdateWindow(object sender, EventArgs e)
        {
            if (Settings.gameOver)
            {
                gameTimer.Stop();
                GameOver();
            }

            else
            {
                if (Input.Pressed(Key.Down) && Settings.direction != moveDirection.up)
                    Settings.direction = moveDirection.down;

                else if (Input.Pressed(Key.Up) && Settings.direction != moveDirection.down)
                    Settings.direction = moveDirection.up;

                else if (Input.Pressed(Key.Right) && Settings.direction != moveDirection.left)
                    Settings.direction = moveDirection.right;

                else if (Input.Pressed(Key.Left) && Settings.direction != moveDirection.right)
                    Settings.direction = moveDirection.left;

                UpdateSnake();
            }
        }


        private void UpdateSnake()
        {
            switch (Settings.direction)
            {
                case moveDirection.down:
                    currentPosition.Y++;
                    DrawSnake(currentPosition);
                    break;
                case moveDirection.up:
                    currentPosition.Y--;
                    DrawSnake(currentPosition);
                    break;
                case moveDirection.right:
                    currentPosition.X++;
                    DrawSnake(currentPosition);
                    break;
                case moveDirection.left:
                    currentPosition.X--;
                    DrawSnake(currentPosition);
                    break;
            }


            // Restrict the canvas dimensions
            if ((currentPosition.X < 0) || (currentPosition.X > myCanvas.ActualWidth - 10) ||
                (currentPosition.Y < 0) || (currentPosition.Y > myCanvas.ActualHeight - 10))
            {
                Settings.gameOver = true;
            }


            // Collisions with food
            int n = 0, x = 0;

            foreach (var foodPiece in plusFood)
            {
                if ((Math.Abs(foodPiece.position.X - currentPosition.X) < Settings.snakeSize) &&
                    (Math.Abs(foodPiece.position.Y - currentPosition.Y) < Settings.snakeSize))
                {
                    Settings.snakeLength += Settings.snakeSize;
                    foodPiece.myScore();

                    x = myCanvas.Children.IndexOf(foodPiece.FeedBack());
                    n = plusFood.IndexOf(foodPiece);
                    myCanvas.Children.RemoveAt(x);
                    plusFood.RemoveAt(n);
                    plusFood.Insert(n, new PlusFood(NewFoodPosition(myCanvas)));
                    myCanvas.Children.Insert(n, plusFood[n].FeedBack());

                    break;
                }
            }


            // Snake's body collision
            foreach (var foodPiece in minusFood)
            {
                if ((Math.Abs(foodPiece.position.X - currentPosition.X) < Settings.snakeSize) &&
                    (Math.Abs(foodPiece.position.Y - currentPosition.Y) < Settings.snakeSize))
                {
                    Settings.snakeLength += Settings.snakeSize;
                    foodPiece.myScore();

                    x = myCanvas.Children.IndexOf(foodPiece.FeedBack());
                    n = minusFood.IndexOf(foodPiece);
                    myCanvas.Children.RemoveAt(x);
                    minusFood.RemoveAt(n);
                    minusFood.Insert(n, new MinusFood(NewFoodPosition(myCanvas)));
                    myCanvas.Children.Insert(n, minusFood[n].FeedBack());

                    break;
                }
            }


            for (int a = 0; a < (snakeBody.Count - 2 * Settings.snakeSize); a++)
            {
                if ((Math.Abs(snakeBody[a].X - currentPosition.X) < (Settings.snakeSize)) &&
                    (Math.Abs(snakeBody[a].Y - currentPosition.Y) < (Settings.snakeSize)))
                {
                    Settings.gameOver = true;
                }
            }
        }

        //private void FoodColision(List<Food> food, int numberOfChild, int numberOfFoodPiece)
        //{
        //    foreach (var foodPiece in food)
        //    {
        //        if ((Math.Abs(foodPiece.position.X - currentPosition.X) < Settings.snakeSize) &&
        //            (Math.Abs(foodPiece.position.Y - currentPosition.Y) < Settings.snakeSize))
        //        {
        //            Settings.snakeLength += Settings.snakeSize;
        //            foodPiece.myScore();

        //            numberOfFoodPiece = myCanvas.Children.IndexOf(foodPiece.FeedBack());
        //            numberOfChild = food.IndexOf(foodPiece);
        //            myCanvas.Children.RemoveAt(numberOfFoodPiece);
        //            food.RemoveAt(numberOfChild);
        //            food.Insert(n, new PlusFood(NewFoodPosition(myCanvas)));
        //            myCanvas.Children.Insert(numberOfChild, food[numberOfChild].FeedBack());

        //            break;
        //        }
        //    }

        //}


        private void GameOver()
        {
            if (Settings.score < 0) Settings.score = 0;
            //SaveScore();

            MessageBox.Show("You Lose! Your score is " + Settings.score.ToString() + 
                "\nPress enter to play again.", "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            StartGame();
        }


        private void KeyPressed(object sender, KeyEventArgs e)
        {
            this.Focus();
            Input.StateOfKey(e.Key, true);
        }

        private void KeyReleased(object sender, KeyEventArgs e)
        {
            this.Focus();
            Input.StateOfKey(e.Key, false);
        }



        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            gameTimer.Stop();
            Switcher.Switch(new MainMenu());
        }
        #endregion
    }
}