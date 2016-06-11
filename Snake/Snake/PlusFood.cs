using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;


namespace Snake
{
    class PlusFood : Element, IFood
    {

        public PlusFood(Point position)
        {
            this.element = new Ellipse();
            this.position = position;
            this.Init(Settings.plusFoodColor, Settings.foodSize);
        }


        public void myScore()
        {
            Settings.score += Settings.scorePoints;
        }

    }
}
