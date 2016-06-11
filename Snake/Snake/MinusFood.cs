using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;


namespace Snake
{
    class MinusFood : Element, IFood
    {

        public MinusFood(Point position)
        {
            this.element = new Rectangle();
            this.position = position;
            this.Init(Settings.minusFoodColor, Settings.foodSize);
        }

        public override bool WhoAreYou()
        {
            return false;
        }

        public override void myScore()
        {
            Settings.score -= Settings.scorePoints;
        }

    }
}
