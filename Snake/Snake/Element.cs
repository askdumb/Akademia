using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Snake
{

    class Element
    {
        public Shape element { get; set; }
        public Point position { get; set; }


        protected void Init(Brush color, int elementSize)
        {
            this.element.Width = elementSize;
            this.element.Height = elementSize;
            this.element.Fill = color;
            this.DrawElement();
        }


        private void DrawElement()
        {
            Canvas.SetLeft(this.element, this.position.X);
            Canvas.SetTop(this.element, this.position.Y);
        }


        public UIElement FeedBack()
        {
            return element;
        }


    }

}