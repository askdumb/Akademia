using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Net;


namespace Snake
{
    public partial class Scores : UserControl, ISwitchable
    {
        public Scores()
        {
            InitializeComponent();
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            string path = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Snake.txt");
            var scoresBlock = sender as TextBlock;
            List<string> highScores = new List<string>();

            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException();

                string score;
                StreamReader file = null;
                file = new StreamReader(path);

                while ((score = file.ReadLine()) != null)
                {
                    highScores.Add(score);
                }


                if (highScores.Count > 10)
                {
                    for (int i = highScores.Count - 10; i < highScores.Count; i++)
                    {
                        scoresBlock.Text += (highScores.ElementAt(i) + "\n");
                    }
                }
                else
                {
                    for (int i = 0; i < highScores.Count; i++)
                    {
                        scoresBlock.Text += (highScores.ElementAt(i) + "\n");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
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
