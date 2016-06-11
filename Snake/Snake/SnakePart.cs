using System.Windows;
using System.Windows.Shapes;


namespace Snake
{
    class SnakePart : Element
    {
        public SnakePart(Point position)
        {
            this.element = new Rectangle();
            this.position = position;
            this.Init(Settings.snakeColor, Settings.snakeSize);
        }
        
    }
}
