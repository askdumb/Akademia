using System.Windows;
using System.Windows.Controls;


namespace Snake
{
    public static class Switcher
    {
        public static MainWindow mainWindow;

        public static void Switch(UserControl newPage)
        {
            mainWindow.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            mainWindow.Navigate(newPage, state);
        }
    }
}
