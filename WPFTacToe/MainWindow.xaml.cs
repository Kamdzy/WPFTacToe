using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using WPFTacToe.Business_Logic;

namespace WPFTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Fields

        protected Button[][] playerInputs;
        private TicTacToeBase game;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            playerInputs = new Button[3][]
            {
                new [] {Button0, Button1, Button2 },
                new [] {Button3, Button4, Button5 },
                new [] {Button6, Button7, Button8 },
            };

            GetChangelog();
            game = new TicTacToeBase();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                var button = (Button)sender;
                button.IsEnabled = false;


                if (game.CurrentPlayer == Token.Player1)
                {
                    button.Content = "x";
                    Turn_Label.Content = "Turn: Player 2 ";
                }
                else if (game.CurrentPlayer == Token.Player2)
                {
                    button.Content = "O";
                    Turn_Label.Content = "Turn: Player 1 ";
                }

                switch (button.Name)
                {
                    case "Button0":
                        game[0, 0] = game.CurrentPlayer;
                        break;
                    case "Button1":
                        game[0, 1] = game.CurrentPlayer;
                        break;
                    case "Button2":
                        game[0, 2] = game.CurrentPlayer;
                        break;
                    case "Button3":
                        game[1, 0] = game.CurrentPlayer;
                        break;
                    case "Button4":
                        game[1, 1] = game.CurrentPlayer;
                        break;
                    case "Button5":
                        game[1, 2] = game.CurrentPlayer;
                        break;
                    case "Button6":
                        game[2, 0] = game.CurrentPlayer;
                        break;
                    case "Button7":
                        game[2, 1] = game.CurrentPlayer;
                        break;
                    case "Button8":
                        game[2, 2] = game.CurrentPlayer;
                        break;
                    default:
                        throw new InvalidOperationException("Uknown button was pressed.");
                }

                if (game.CheckWinCondition(Token.Player1))
                {
                    Turn_Label.Content = "Player 1 won.";
                    PlayFieldGrid.IsEnabled = false;
                }
                else if (game.CheckWinCondition(Token.Player2))
                {
                    Turn_Label.Content = "Player 2 won.";
                    PlayFieldGrid.IsEnabled = false;
                }
                else
                {
                    if (game.RoundsLeft <= 0)
                    {
                        Turn_Label.Content = "It's a draw.";
                        PlayFieldGrid.IsEnabled = false;
                    }
                }

            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            game.Reset();
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
