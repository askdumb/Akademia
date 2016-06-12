using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO;
using System.Net;


namespace Snake
{
    public partial class SnakeGame : UserControl, ISwitchable
    {
        private TimeSpan timerSpeed = new TimeSpan(Settings.timerSpeed);
        private List<Element> foodList = new List<Element>();
        private List<SnakePart> snakeBody = new List<SnakePart>();
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
            foodList.Clear();
            myCanvas.Children.Clear();
            new Settings();
            currentPosition = Settings.head;
            Timer();
            Paint();
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
            DrawSnake(currentPosition);

            for (int i = 0; i < 5; i++)
            {
                foodList.Insert(i, new MinusFood(NewFoodPosition(myCanvas)));
                myCanvas.Children.Insert(i, foodList[i].FeedBack());
            }

            for (int i = 5; i < 10; i++)
            {
                foodList.Insert(i, new PlusFood(NewFoodPosition(myCanvas)));
                myCanvas.Children.Insert(i, foodList[i].FeedBack());
            }
        }


        private void DrawSnake(Point position)
        {
            snakeBody.Add(new SnakePart(position));
            myCanvas.Children.Add(snakeBody[snakeBody.Count - 1].FeedBack());

            int elementsCount = myCanvas.Children.Count;

            if (elementsCount >= Settings.snakeLength)
            {
                myCanvas.Children.RemoveAt(elementsCount - Settings.snakeLength + foodList.Count);
                snakeBody.RemoveAt(elementsCount - Settings.snakeLength);
            }


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


            if ((currentPosition.X < 0) || (currentPosition.X > myCanvas.ActualWidth - 10) ||
                (currentPosition.Y < 0) || (currentPosition.Y > myCanvas.ActualHeight - 10))
            {
                Settings.gameOver = true;
            }


            foreach (var foodPiece in foodList)
            {
                if ((Math.Abs(foodPiece.position.X - currentPosition.X) < Settings.snakeSize) &&
                    (Math.Abs(foodPiece.position.Y - currentPosition.Y) < Settings.snakeSize))
                {
                    Settings.snakeLength += Settings.snakeSize;
                    foodPiece.myScore();

                    int numberOfChild = myCanvas.Children.IndexOf(foodPiece.FeedBack());
                    int numberOfFoodPiece = foodList.IndexOf(foodPiece);
                    myCanvas.Children.RemoveAt(numberOfChild);
                    foodList.RemoveAt(numberOfFoodPiece);

                    bool whoAreYou = foodPiece.WhoAreYou();

                    if (whoAreYou) foodList.Insert(numberOfFoodPiece, new PlusFood(NewFoodPosition(myCanvas)));
                    else foodList.Insert(numberOfFoodPiece, new MinusFood(NewFoodPosition(myCanvas)));
                    myCanvas.Children.Insert(numberOfChild, foodList[numberOfFoodPiece].FeedBack());

                    break;
                }
            }


            for (int a = 0; a < (snakeBody.Count - 2 * Settings.snakeSize); a++)
            {
                if ((Math.Abs(snakeBody[a].position.X - currentPosition.X) < (Settings.snakeSize)) &&
                    (Math.Abs(snakeBody[a].position.Y - currentPosition.Y) < (Settings.snakeSize)))
                {
                    Settings.gameOver = true;
                }
            }
        }


        private void SaveFile()
        {
            string path = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Snake.txt");
            var date = DateTime.Now;
            StreamWriter save;

            if(!File.Exists(path)){
                save = File.CreateText(path);
                save.Close();
            }

            if(File.Exists(path))
            {
                save = File.AppendText(path);
                save.WriteLine(date + "         " + Settings.score);
                save.Close();
            }
           
        }


        private void GameOver()
        {
            SaveFile();

            MessageBox.Show("You Lose! Your score is " + Settings.score.ToString() +
                "\nPress enter to play again.", "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            StartGame();
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