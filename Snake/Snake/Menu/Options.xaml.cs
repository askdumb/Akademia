﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Snake
{
    public partial class Options : UserControl, ISwitchable
    {
        public Options()
        {
            InitializeComponent();
        }


        private void SnakeSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SnakeSize.SelectedIndex)
            {
                case 0:
                    Settings.snakeSize = (int)size.small;
                    break;

                case 1:
                    Settings.snakeSize = (int)size.medium;
                    break;

                case 2:
                    Settings.snakeSize = (int)size.big;
                    break;

                case 3:
                    Settings.snakeSize = (int)size.large;
                    break;
            }
        }



        private void FoodSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (FoodSize.SelectedIndex)
            {
                case 0:
                    Settings.snakeSize = (int)size.small;
                    break;

                case 1:
                    Settings.snakeSize = (int)size.medium;
                    break;

                case 2:
                    Settings.snakeSize = (int)size.big;
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TimerSpeed.SelectedIndex)
            {
                case 0:
                    Settings.timerSpeed = (int)timeSpan.damnslow;
                    break;

                case 1:
                    Settings.timerSpeed = (int)timeSpan.slow;
                    break;

                case 2:
                    Settings.timerSpeed = (int)timeSpan.moderate;
                    break;

                case 3:
                    Settings.timerSpeed = (int)timeSpan.fast;
                    break;
            }
        }


        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
        #endregion


    }
}