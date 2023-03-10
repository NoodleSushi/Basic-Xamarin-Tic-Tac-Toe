using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tic_Tac_Toe
{
    public partial class MainPage : ContentPage
    {
        private TicTacToeGame Game = new TicTacToeGame();
        private Button[] BoxList;

        public MainPage()
        {
            InitializeComponent();
            BoxList = new Button[] {Box0, Box1, Box2, Box3, Box4, Box5, Box6, Box7, Box8};
            Game.BoxChangeHandler = new PlayerXHandler(BoxChangeNotified);
            Game.PlayerTurnHandler = new PlayerHandler(PlayerTurnNotified);
            Game.WinnerHandler = new PlayerHandler(WinnerNotified);
            Game.Reset();
        }

        private void OnBoxClicked(object sender, EventArgs e)
        {
            Game.PlacePiece(int.Parse((sender as Button).ClassId));
        }

        private void ResetGame(object sender, EventArgs e)
        {
            Game.Reset();
        }

        void BoxChangeNotified(int x, int player)
        {
            BoxList[x].Text = (player == -1) ? "-" : (player == 0) ? "X" : "O"; 
            BoxList[x].BackgroundColor = (player == -1) ? Color.Gray : (player == 0) ? Color.Red : Color.Blue;
        }

        void PlayerTurnNotified(int player)
        {
            XStatus.Text = "X " + ((player == 0) ? "player's turn" : "wait");
            OStatus.Text = "O " + ((player == 1) ? "player's turn" : "wait");
        }

        void WinnerNotified(int player)
        {
            XStatus.Text = "X player " + ((player == 0) ? "won" : "lost");
            OStatus.Text = "O player " + ((player == 1) ? "won" : "lost");
        }
    }
}
