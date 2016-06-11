using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Snake
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            Settings.snakeSize = (int)size.big;
            Settings.foodSize = (int)size.big;
            Settings.timerSpeed = (int)timeSpan.moderate;
            InitializeComponent();
            Switcher.mainWindow = this;
            Switcher.Switch(new MainMenu());
        }


        public void Navigate(UserControl anotherPage)
        {
            this.Content = anotherPage;
        }


        public void Navigate(UserControl anotherPage, object sender)
        {
            this.Content = anotherPage;
            ISwitchable s = anotherPage as ISwitchable;

            if (s != null)
                s.UtilizeState(sender);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + anotherPage.Name.ToString());
        }


        public void KeyPressed(object sender, KeyEventArgs e)
        {
            this.Focus();
            Input.StateOfKey(e.Key, true);
        }


        public void KeyReleased(object sender, KeyEventArgs e)
        {
            this.Focus();
            Input.StateOfKey(e.Key, false);
        }

    }
}
