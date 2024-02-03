using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _2_paras
{
    public partial class MainWindow : Window
    {
        private Button[,] buttons;
        Random random = new Random();
        private int currentPlayerIndex = 0;
        private readonly char[] Players = [ 'X', 'O' ];
        private int currentHumanIndex;
        private bool firstGame = true;
        private int playerWinsCount = 0;
        private int computerWinsCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            Height = 400;
            Width = 500;
            buttons = new Button[,]
            {
                { _1, _2, _3 },
                { _4, _5, _6 },
                { _7, _8, _9 } 
            };
            DisableButtons();
            firstGame = true;
            currentHumanIndex = random.Next(2);
        }
        private void NewPlay(object sender, RoutedEventArgs e)
        {
            playerWins.Text = $"Player Wins: {playerWinsCount}";
            TextInfo.Text = "    Tic Tac Toe";
            foreach (Button button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
                button.Background = Brushes.Transparent;
            }            
            if (!firstGame) 
            {
                currentHumanIndex = 1 - currentHumanIndex;
            }
            if(currentHumanIndex == 1) 
            {
                currentPlayerIndex = 1 - currentHumanIndex;
                PerformComputerMove();
                SwitchPlayer();
            }
            else
            {
                currentPlayerIndex = currentHumanIndex;
            }
        }
        private void SuperClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;     
            
            clickedButton.Content = Players[currentPlayerIndex];
            if (Players[currentPlayerIndex] == 'X')
            {
                clickedButton.Background = Brushes.Purple;
                
            }
            else if (Players[currentPlayerIndex] == 'O')
            {
                clickedButton.Background = Brushes.Orange;
                
            }
            if (IsWinner()== true)
            {
                TextInfo.Text=$"Player {Players[currentPlayerIndex]} wins!";
                if (Players[currentPlayerIndex] == 'X' || Players[currentPlayerIndex] == 'O')
                {
                    playerWinsCount++;
                    playerWins.Text = $"Player Wins: {playerWinsCount}";
                    SwitchPlayer();
                }
               
                DisableButtons();
            }
            else if (IsBoardFull()) 
            {
                TextInfo.Text = "         Draw!";
                DisableButtons();
            }
            else
            {
                SwitchPlayer();
                PerformComputerMove();
                if (IsWinner())
                {
                    TextInfo.Text = $"Player {Players[currentPlayerIndex]} won!";
                    DisableButtons();
                }
                else if (IsBoardFull())
                {
                    TextInfo.Text = " Hooray draw!";
                    DisableButtons();
                }                    
            }
            SwitchPlayer();
        }
        private void SwitchPlayer()
        {
            currentPlayerIndex = 1 - currentPlayerIndex; 
        }
        private void DisableButtons()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
            firstGame = false;
        }
        private void PerformComputerMove()
        {
            int i, j;
            do
            {
                i = random.Next(0, 3);
                j = random.Next(0, 3);
            } while (buttons[i, j].Content.ToString() != "");

            buttons[i, j].Content = Players[currentPlayerIndex];
            buttons[i, j].IsEnabled = false;

            SolidColorBrush brush = Players[1-currentPlayerIndex] == 'X' ? Brushes.Purple : Brushes.Orange;
            buttons[i, j].Background = brush;
        }     
        public bool IsWinner()
        {
            // Проверка по горизонтали
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Content.ToString() == buttons[i, 1].Content.ToString() &&
                    buttons[i, 1].Content.ToString() == buttons[i, 2].Content.ToString() &&
                    buttons[i, 0].Content.ToString() != "")
                {
                    return true;
                }
            }
            // Проверка по вертикали
            for (int j = 0; j < 3; j++)
            {
                if (buttons[0, j].Content.ToString() == buttons[1, j].Content.ToString() && 
                    buttons[1, j].Content.ToString() == buttons[2, j].Content.ToString() && 
                    buttons[0, j].Content.ToString() != "")
                {
                    return true;
                }
            }
            // Проверка по диагонали (левая верхняя - правая нижняя)
            if (buttons[0, 0].Content.ToString() == buttons[1, 1].Content.ToString() && 
                buttons[1, 1].Content.ToString() == buttons[2, 2].Content.ToString() && 
                buttons[0, 0].Content.ToString() != "")
            {
                return true;
            }
            // Проверка по диагонали (правая верхняя - левая нижняя)
            if (buttons[0, 2].Content.ToString() == buttons[1, 1].Content.ToString() && 
                buttons[1, 1].Content.ToString() == buttons[2, 0].Content.ToString() && 
                buttons[0, 2].Content.ToString() != "")
            {
                return true;
            }
             return false;                  
        }
        private bool IsBoardFull()
        {
            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Content?.ToString()))
                {
                    return false;
                }
            }
            return true;         
        }
    }
}