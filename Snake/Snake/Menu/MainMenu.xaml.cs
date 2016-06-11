using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Snake
{

    public partial class MainMenu : UserControl, ISwitchable
    {

        public MainMenu()
        {
            InitializeComponent();

        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void startGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new SnakeGame());
        }

        private void optionsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new Options());
        }
        #endregion

    }
}