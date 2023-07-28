using System.Windows;
using System.Windows.Controls;

namespace CubesGameGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int MValue;
        private int KValue;
        private Player Player = new Player("Player");
        private Player Computer = new Player("Computer");
        private Player CurrentPlayer = new Player();

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            Choise1Btn.IsEnabled = false;
            Choise2Btn.IsEnabled = false;
            ChoiseKBtn.IsEnabled = false;
            MCubesText.IsEnabled = true;
            KCubesText.IsEnabled = true;
            PlayGameButton.IsEnabled = true;
        }

        private void PlayGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckPlayerInput())
            {
                CurrentPlayer = Computer;
                PlayGameButton.IsEnabled = false;
                MCubesText.IsEnabled = false;
                KCubesText.IsEnabled = false;
                Choise1Btn.IsEnabled = true;
                Choise2Btn.IsEnabled = true;
                ChoiseKBtn.IsEnabled = true;
                ChoiseKBtn.Content = KValue;
                getComputerMove();
            }
        }

        private bool CheckPlayerInput()
        {
            string M = MCubesText.Text;
            string K = KCubesText.Text;
            if (!int.TryParse(M, out MValue))
            {
                MessageBox.Show("Εδωσες Λανθασμενη Τιμή Αριθμός Κύβων", "Error");
                return false;
            }
            else if (!int.TryParse(K, out KValue))
            {
                MessageBox.Show("Εδωσες Λανθασμενη Τιμή Αριθμό Κύβων που μπορεις να σηκώσεις", "Error");
                return false;
            }
            else if ((MValue <= 2 || KValue <= 2) || KValue > MValue)
            {
                MessageBox.Show("Εδωσες Λανθασμενη Τιμή Αριθμός Κύβων < Αριθμό Κύβων που μπορεις να σηκώσεις", "Error");
                return false;
            }
            return true;
        }

        private void CheckChoise(object sender, RoutedEventArgs e)
        {
            Button myButton = (Button)sender;
            string choise = myButton.Content.ToString()!;
            int PlayerChoise = int.Parse(choise);
            MValue -= PlayerChoise;
            MessageBox.Show($"Ο {CurrentPlayer.PlayerName} επέλεξε {choise} κύβους");
            UpdateCubesLabel();
            if (KValue > MValue)
            {
                ChoiseKBtn.IsEnabled = false;
            }
            if (MValue == 0)
            {
                CheckIfWin();
            }
            getComputerMove();
        }

        private void getComputerMove()
        {
            CurrentPlayer = Computer;
            MiniMax minimax = new MiniMax();
            int ComputerChoise = minimax.FindBestMove(MValue, 5, KValue, true);
            MValue -= ComputerChoise;
            MessageBox.Show($"Ο {CurrentPlayer.PlayerName} επέλεξε {ComputerChoise} κύβους");
            UpdateCubesLabel();
            if (KValue > MValue)
            {
                ChoiseKBtn.IsEnabled = false;
            }
            if (MValue == 0)
            {
                CheckIfWin();
            }
            CurrentPlayer = Player;
        }

        private void CheckIfWin()
        {
            MessageBox.Show($"Νίκησε ο {CurrentPlayer.PlayerName}");
            CurrentPlayer.PlayerScore++;
            UpdateScoreLabel();
            InitializeGame();
        }

        private void UpdateCubesLabel()
        {
            label1.Content = $"Υπάρχουν {MValue} Κύβοι στο Τραπέζι";
        }

        private void UpdateScoreLabel()
        {
            ScoresLabel.Content = $"Player VS Computer : {Player.PlayerScore} - {Computer.PlayerScore}";
        }

        private void InfoGameButton_Click(object sender, RoutedEventArgs e)
        {
            string message = "Cubes Game with MiniMax\n";
            message += "Δημιουργήθηκε με την c# και wpf\n";
            message += "Όνομα Προγραμματιστή : Ψαλτάκης Νικόλαος (npsalt)\n";
            message += "(C) 7/2023";
            MessageBox.Show(message, "Πληροφορίες");
        }
    }
}