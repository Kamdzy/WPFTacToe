using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace WPFTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Fields

        private Button[][] playerInputs;

        private string playerMark = "X";
        private int roundsLeft = 9;
        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();


            playerInputs = new Button[3][]
            {
                new [] { Button0, Button1, Button2 },
                new [] { Button3, Button4, Button5 },
                new [] { Button6, Button7, Button8 },
            };

            GetChangelog();
        }

        #endregion

        private void GetChangelog()
        {
            var url = "https://raw.githubusercontent.com/Kamdzy/WPFTacToe/master/CHANGELOG.md";

            using (var client = new WebClient())
            {
                Changelog.Text = client.DownloadString(url);
            }

        }

        public bool CheckWinCondition(string player)
        {
            if (CheckRow(player) || CheckColumn(player) || CheckDiagonal(player))
            {
                return true;
            }

            return false;


        }

        public bool CheckRow(string player)
        {
            for (int y = 0; y < playerInputs.Length; y++)
            {
                bool wholeRow = true;

                for (int x = 0; x < playerInputs[y].Length && wholeRow; x++)
                {
                    if (playerInputs[x][y].Content as string != player)
                    {
                        wholeRow = false;
                    }
                }

                if (wholeRow)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckColumn(string player)
        {
            for (int x = 0; x < playerInputs.Length; x++)
            {
                bool wholeColumn = true;

                for (int y = 0; y < playerInputs[x].Length && wholeColumn; y++)
                {
                    if (playerInputs[x][y].Content as string != player)
                    {
                        wholeColumn = false;
                    }
                }

                if (wholeColumn)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckDiagonal(string player)
        {
            bool wholeDiaognal = true;

            for (int x = 0; x < playerInputs[0].Length && wholeDiaognal; x++)
            {
                if ((playerInputs[x][x].Content as string != player))
                {
                    wholeDiaognal = false;
                }
            }

            if (wholeDiaognal)
            {
                return true;
            }

            wholeDiaognal = true;

            for (int x = 0; x < playerInputs[0].Length && wholeDiaognal; x++)
            {
                if (playerInputs[x][playerInputs.Length - 1 - x].Content as string != player)
                {
                    wholeDiaognal = false;
                }
            }
            
            if (wholeDiaognal)
            {
                return true;
            }

            return false;
        }

        public void PlayerSwitch()
        {
            if (playerMark == "O")
            {
                Turn_Label.Content = "Turn: Player 1 ";
                playerMark = "X";
            }
            else if (playerMark == "X")
            {
                Turn_Label.Content = "Turn: Player 2 ";
                playerMark = "O";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                roundsLeft--;
                var button = (Button)sender;


                button.Content = playerMark;
                button.IsEnabled = false;

                if (CheckWinCondition("X"))
                {
                    Turn_Label.Content = "Player 1 won.";
                    PlayFieldGrid.IsEnabled = false;
                }
                else if (CheckWinCondition("O"))
                {
                    Turn_Label.Content = "Player 2 won.";
                    PlayFieldGrid.IsEnabled = false;
                }
                else
                {
                    if (roundsLeft <= 0)
                    {
                        Turn_Label.Content = "It's a draw.";
                        PlayFieldGrid.IsEnabled = false;
                    }
                    else
                    {
                        PlayerSwitch();
                    }
                }
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            playerMark = "X";
            roundsLeft = 9;
            Turn_Label.Content = "Turn: Player 1";

            PlayFieldGrid.IsEnabled = true;

            foreach (var btn in playerInputs.SelectMany(x => x))
            {
                btn.IsEnabled = true;
                btn.Content = "";
            }
        }

        #region Window commands

        private void Main_Click(object sender, RoutedEventArgs e)
        {

            this.SelectionGrid.Visibility = Visibility.Visible;
            this.GameGrid.Visibility = Visibility.Collapsed;
            this.AboutGrid.Visibility = Visibility.Collapsed;
        }

        private void Game_Click(object sender, RoutedEventArgs e)
        {

            this.SelectionGrid.Visibility = Visibility.Collapsed;
            this.GameGrid.Visibility = Visibility.Visible;
            this.AboutGrid.Visibility = Visibility.Collapsed;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

            this.SelectionGrid.Visibility = Visibility.Collapsed;
            this.GameGrid.Visibility = Visibility.Collapsed;
            this.AboutGrid.Visibility = Visibility.Visible;
        }


        private void PvPL_Click(object sender, RoutedEventArgs e)
        {

            this.SelectionGrid.Visibility = Visibility.Collapsed;
            this.GameGrid.Visibility = Visibility.Visible;
            this.AboutGrid.Visibility = Visibility.Collapsed;

            GameButton.IsEnabled = true;
        }

        #endregion
        
    }
}
