using System;
using System.Windows;
using System.Windows.Media;


namespace Snake
{
    public class Settings
    {
        public static moveDirection direction;
        public static int snakeSize;
        public static int foodSize;
        public static int timerSpeed;
        public static int snakeLength { get; set; }
        public static int score { get; set; }
        public static int scorePoints { get; set; }
        public static bool gameOver { get; set; }
        public static Point head { get; set; }
        public static Brush snakeColor { get; set; }
        public static Brush plusFoodColor { get; set; }
        public static Brush minusFoodColor { get; set; }



        public Settings()
        {
            direction = moveDirection.motionless;
            gameOver = false;
            snakeLength = 100;
            score = 0;
            scorePoints = 10;
            snakeColor = Brushes.DarkGreen;
            plusFoodColor = Brushes.DarkRed;
            minusFoodColor = Brushes.DarkSlateGray;
            head = new Point(100, 100);
        }
    }
}
