using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class BlackjackConsoleUi {

        #region Constants

        public const string GAME_TITLE = "Poor Man's Blackjack";
        public const string AUTHOR = "Alexander Herrfurth";

        private const int BET_STEP = 10;
        private const int BET_STEP_SMALL = 1;
        private const int MAX_NAME_LENGTH = 12;

        public const int WINDOW_WIDTH = 100;
        public const int WINDOW_HEIGHT = 30;

        private const int START_BALANCE = 500;
        private const int MIN_BET = 10;
        private const int MAX_BET = 500;

        #endregion

        #region Fields

        private Blackjack _blackjack;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public BlackjackConsoleUi() {

            Console.OutputEncoding = Encoding.Unicode;
            Console.Title = GAME_TITLE + " by " + AUTHOR;

            _blackjack = new Blackjack(MIN_BET, MAX_BET);

            _blackjack.PlayerWon += OnPlayerWon;
            _blackjack.PlayerLost += OnPlayerLost;
            _blackjack.Push += OnPush;

        }

        #endregion

        #region Methods

        public void Run() {

            Console.CursorVisible = false;

            ConsoleUtility.ForceSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            ShowTitleScreen();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            _blackjack.StartGame(START_BALANCE);

            ConsoleUtility.ForceSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            DrawGui();

            bool hasCanceled = false;

            while (!hasCanceled) {

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                ConsoleKey key = keyInfo.Key;

                ConsoleUtility.ForceSize(WINDOW_WIDTH, WINDOW_HEIGHT);

                switch (key) {

                    case ConsoleKey.Escape:
                        hasCanceled = true;
                        break;

                    case ConsoleKey.Enter:
                        // Stand
                        if(_blackjack.IsStandAllowed) {
                            _blackjack.Stand();
                            DrawGui();
                        }
                        // Deal
                        else if(_blackjack.IsDealAllowed) {

                            // Clear dealer's win/lose/push message
                            ConsoleUtility.ClearArea(0, 1, 16, 2);

                            // Clear players's win/lose/push message
                            ConsoleUtility.ClearArea(0, 17, 16, 2);

                            _blackjack.Deal();
                            DrawGui();
                        }
                        // Continue
                        else if(_blackjack.IsGameOver) {

                            // Clear dealer's win/lose/push message
                            ConsoleUtility.ClearArea(0, 1, 16, 2);

                            // Clear players's win/lose/push message
                            ConsoleUtility.ClearArea(0, 17, 16, 2);

                            _blackjack.StartGame(START_BALANCE);
                            DrawGui();
                        }
                        break;

                    case ConsoleKey.Spacebar:
                        // Hit
                        if(_blackjack.IsHitAllowed) {
                            _blackjack.Hit();
                            DrawGui();
                        }
                        break;

                    case ConsoleKey.Tab:
                        // Double
                        if(_blackjack.IsDoubleAllowed) {
                            _blackjack.Double();
                            DrawGui();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        // Increase Bet
                        if(_blackjack.IsDealAllowed) {
                            _blackjack.Player.CurrentBet += BET_STEP;
                            DrawGui();
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        // Decrease Bet
                        if (_blackjack.IsDealAllowed) {
                            _blackjack.Player.CurrentBet -= BET_STEP;
                            DrawGui();
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        // Increase Bet
                        if (_blackjack.IsDealAllowed) {
                            _blackjack.Player.CurrentBet += BET_STEP_SMALL;
                            DrawGui();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        // Decrease Bet
                        if (_blackjack.IsDealAllowed) {
                            _blackjack.Player.CurrentBet -= BET_STEP_SMALL;
                            DrawGui();
                        }
                        break;

                    default:
                        break;

                }

                // Flush keyboard buffer | TUTORIAL NOTE: Demonstrate what happens without this!
                ConsoleUtility.FlushKeyboardBuffer();

            }

        }

        private void OnPush(object sender, EventArgs e) {

            ConsoleColor originalColor = Console.ForegroundColor;

            Console.SetCursorPosition(1, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("► Push!");

            Console.SetCursorPosition(1, 17);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("► Push!");

            Console.ForegroundColor = originalColor;

        }

        private void OnPlayerLost(object sender, EventArgs e) {

            ConsoleColor originalColor = Console.ForegroundColor;

            Console.SetCursorPosition(1, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("► Dealer wins!");

            Console.SetCursorPosition(1, 17);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("► You lose!");

            if (_blackjack.IsGameOver)
                Console.WriteLine(" ► GAME OVER!");

            Console.ForegroundColor = originalColor;

        }

        private void OnPlayerWon(object sender, EventArgs e) {

            ConsoleColor originalColor = Console.ForegroundColor;

            Console.SetCursorPosition(1, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("► Dealer loses!");

            Console.SetCursorPosition(1, 17);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("► You win!");

            Console.ForegroundColor = originalColor;

        }

        

        private void DrawGui() {

            if (_blackjack == null)
                return;

            Console.SetCursorPosition(0, 0);

            string dealerValueMessage = "Dealer: " + _blackjack.Dealer.Hand.GetVisibleValue(1) + (_blackjack.Dealer.Hand.IsBlackjack ? " BLACKJACK" : "");
            Console.WriteLine(dealerValueMessage.PadRight(22));

            ConsoleUtility.DrawAsciiArt(_blackjack.Deck, 0, 3, Console.ForegroundColor);
            string deckLabel = "•Deck: " + _blackjack.Deck.NumberRemainingCards;
            Console.WriteLine(deckLabel.PadRight(11));

            ConsoleUtility.DrawAsciiArt(_blackjack.DiscardPile, 12, 3, Console.ForegroundColor);
            string discardLabel = "•Discards: " + _blackjack.DiscardPile.NumberRemainingCards;
            Console.WriteLine(discardLabel.PadRight(20));

            ConsoleUtility.WriteNewLines(3); // 
            string playerValueMessage = _blackjack.Player.Name + ": " + _blackjack.Player.Hand.Value + (_blackjack.Player.Hand.IsBlackjack ? " BLACKJACK " : "");
            Console.WriteLine(playerValueMessage.PadRight(27));
            ConsoleUtility.WriteNewLines(3);

            string balanceLabel = "Balance: $" + _blackjack.Player.Balance;
            Console.WriteLine(balanceLabel.PadRight(16));

            string betLabel = "Bet: $" + _blackjack.Player.CurrentBet;
            Console.WriteLine(betLabel.PadRight(16));
            ConsoleUtility.WriteNewLines(3);

            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;

            if (_blackjack.GameState == GameStates.Ended) {

                Console.WriteLine("Round over! ");

            }
            else if (_blackjack.GameState == GameStates.PlayersTurn) {

                Console.WriteLine("Your turn!  ");

            }
            else {

                // Clear "Round over"/"Your turn"
                Console.WriteLine("            ");

            }

            Console.ForegroundColor = originalColor;

            if (_blackjack.IsDealAllowed)
                Console.WriteLine("Place your bet!");
            else
                Console.WriteLine("               ");

            string optionLine = "";

            if (_blackjack.IsStandAllowed)
                optionLine += "[Enter] Stand  ";
            else if (_blackjack.IsDealAllowed)
                optionLine += "[Enter] Deal  ";
            else if (_blackjack.IsGameOver)
                optionLine += "[Enter] New Game  ";

            if (_blackjack.IsHitAllowed)
                optionLine += "[Space] Hit  ";

            if (_blackjack.IsDoubleAllowed)
                optionLine += "[Tab] Double  ";

            if (_blackjack.IsDealAllowed) {

                // Arrow symbols: alt + 24, alt + 25, alt + 26, alt + 27
                optionLine += $"[↑] Bet +{BET_STEP}  ";
                optionLine += $"[↓] Bet -{BET_STEP}  ";
                optionLine += $"[→] Bet +{BET_STEP_SMALL}  ";
                optionLine += $"[←] Bet -{BET_STEP_SMALL}  ";

            }

            optionLine += "[Esc] Quit";

            Console.SetCursorPosition(0, 28);
            Console.Write(optionLine.PadRight(WINDOW_WIDTH));

            // Clear dealer's hand area
            ConsoleUtility.ClearArea(27, 1, 70, 9);

            // Clear players's hand area
            ConsoleUtility.ClearArea(27, 16, 70, 9);

            DrawHandCards(_blackjack.Dealer.Hand, 27, 1);
            DrawHandCards(_blackjack.Player.Hand, 27, 16);

            // Write table rules (min bet, max bet, dealer stand value)
            Console.SetCursorPosition(34, 12);
            ConsoleColor origColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Dealer must stand on {Dealer.STAND_VALUE} and draw to {Dealer.STAND_VALUE - 1}");
            Console.SetCursorPosition(38, 13);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"min. bet: ${MIN_BET} | max. bet: ${MAX_BET}");
            Console.ForegroundColor = origColor;

            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

        }

        private string NameEnter() {

            string playerName = Console.ReadLine();

            playerName = playerName.Trim();

            if (playerName.Length > MAX_NAME_LENGTH)
                playerName = playerName.Substring(0, MAX_NAME_LENGTH);

            return playerName;

        }

        public void DrawHandCards(Hand hand, int posX, int posY) {

            if (hand == null)
                return;

            IEnumerable<Card> cards = hand.Cards;

            foreach(Card card in cards) {

                ConsoleUtility.DrawAsciiArt(card, posX, posY, Console.ForegroundColor);
                posX += 7;

            }

        }

        private void ShowTitleScreen() {

            Console.BackgroundColor = ConsoleColor.Black;

            Console.Clear();

            string titleArt = "\n" +
                "  ____                    __  __             _     \n" +
                " |  _ \\ ___   ___  _ __  |  \\/  | __ _ _ __ ( )___ \n" +
                " | |_) / _ \\ / _ \\| '__| | |\\/| |/ _` | '_ \\|// __|\n" +
                " |  __/ (_) | (_) | |    | |  | | (_| | | | | \\__ \\\n" +
                " |_|__ \\___/ \\___/|_|_   |_|  |_|\\__,_|_| |_| |___/\n" +
                " | __ )| | __ _  ___| | __(_) __ _  ___| | __      \n" +
                " |  _ \\| |/ _` |/ __| |/ /| |/ _` |/ __| |/ /      \n" +
                " | |_) | | (_| | (__|   < | | (_| | (__|   <       \n" +
                " |____/|_|\\__,_|\\___|_|\\_\\/ |\\__,_|\\___|_|\\_\\      \n" +
                "                        |__/                       \n";

            ConsoleUtility.DrawAsciiArt(titleArt, 24, 2, ConsoleColor.Green);

            Console.SetCursorPosition(38, 15);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Recommended settings:");

            Console.SetCursorPosition(33, 16);
            Console.Write("Font: Consolas ♠ Size: 16 - 20");

            Console.SetCursorPosition(40, 17);
            Console.Write("Disable Word Wrap");

            Console.SetCursorPosition(34, 28);
            Console.Write("© 2017 " + AUTHOR);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(27, 20);

            Console.Write($"Please enter your name (max {MAX_NAME_LENGTH} characters)");
            Console.SetCursorPosition(36, 21);
            Console.Write("and confirm to start:");
            
            Console.SetCursorPosition(40, 22);
            Console.Write("┌------------┐");
            Console.SetCursorPosition(40, 24);
            Console.Write("└------------┘");

            Console.SetCursorPosition(41, 23);

            Console.CursorVisible = true;

            _blackjack.Player.Name = NameEnter();

            Console.CursorVisible = false;

        }

        #endregion

    }

}
