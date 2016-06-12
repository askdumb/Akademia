using System.Windows;
using System.Windows.Shapes;


namespace Snake
{
    class MinusFood : Element, IFood
    {
        public MinusFood(Point position)
        {
            element = new Rectangle();
            this.position = position;
            Init(Settings.minusFoodColor, Settings.foodSize);
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
