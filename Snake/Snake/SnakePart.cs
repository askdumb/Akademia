using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

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
