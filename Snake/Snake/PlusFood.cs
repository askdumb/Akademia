using System.Windows;
using System.Windows.Shapes;


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

        public override bool WhoAreYou()
        {
            return base.WhoAreYou();
        }

        public override void myScore()
        {
            base.myScore();
        }

    }
}
