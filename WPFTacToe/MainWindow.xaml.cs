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
using MahApps.Metro.Controls;

namespace WPFTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            GameStart();
        }

        int currentPlayer = 1;
        private string playerMark = "?";
        private int roundsLeft = 9;
        private List<Button> myButtons = new List<Button>();
        public void ListButtons()
        {
            myButtons.Add(Button0);
            myButtons.Add(Button1);
            myButtons.Add(Button2);
            myButtons.Add(Button3);
            myButtons.Add(Button4);
            myButtons.Add(Button5);
            myButtons.Add(Button6);
            myButtons.Add(Button7);
            myButtons.Add(Button8);
        }

        public int RoundCheck()
        {
            return 1;
        }

        public void PlayerSwitch()
        {
            if (currentPlayer == 1)
            {
                playerMark = "X";
                currentPlayer = 2;
            }
            else if (currentPlayer == 2)
            {
                playerMark = "O";
                currentPlayer = 1;
            }
        }

        public void PlayMove()
        {
            foreach (var button in myButtons)
            {
                button.Click += Button_Click;
            }
        }

        public void GameStart()
        {
            ListButtons();

            while (roundsLeft >= 0)
            {
                PlayMove();

                if (RoundCheck() == 0)
                {
                    continue;
                }

                roundsLeft--;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
        }
    }
}
