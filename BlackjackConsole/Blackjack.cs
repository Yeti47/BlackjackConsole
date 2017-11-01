using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackConsole {

    public class Blackjack {

        #region Fields

        #endregion

        #region Events

        public event EventHandler PlayerWon;
        public event EventHandler Push;
        public event EventHandler PlayerLost;
                
        #endregion

        #region Properties

        public Deck Deck { get; private set; } = new Deck();
        public Deck DiscardPile { get; private set; } = new Deck(true);

        public Player Player { get; private set; }
        public Dealer Dealer { get; private set; }

        public int MinBet { get; set; } = 1;
        public int MaxBet { get; set; } = 500;

        public GameStates GameState { get; private set; }

        public bool IsHitAllowed => GameState == GameStates.PlayersTurn;

        public bool IsDealAllowed {

            get {

                return (GameState == GameStates.Started || GameState == GameStates.Ended) && !IsGameOver;

            }

        }

        public bool IsDoubleAllowed => GameState == GameStates.PlayersTurn && Player != null && Player.CanDouble;

        public bool IsStandAllowed => GameState == GameStates.PlayersTurn;

        public bool IsGameOver => Player?.Balance < MinBet;

        #endregion

        #region Constructor

        public Blackjack (int minBet, int maxBet) : this() {

            MinBet = minBet;
            MaxBet = maxBet;

        }

        public Blackjack() {

            Player = new Player();
            Dealer = new Dealer(this);
            GameState = GameStates.Initial;

        }

        #endregion

        #region Methods

        public void StartGame(int initialBalance) {
            
            Deck.Initialize();
            DiscardPile.Clear();

            Deck.Shuffle();

            Player.Hand.DiscardAll();
            Player.CurrentBet = 0;
            Player.Balance = initialBalance;

            Dealer.Hand.DiscardAll();

            GameState = GameStates.Started;

        }

        public bool Deal() {

            if (!IsDealAllowed)
                return false;

            Player.HasDoubled = false;
            
            // Put all cards currently in the player's and the dealer's hand (if any) on the discard pile before the next round starts
            IEnumerable<Card> playerDiscards = Player.Hand.DiscardAll();
            IEnumerable<Card> dealerDiscards = Dealer.Hand.DiscardAll();

            DiscardPile.Push(playerDiscards);
            DiscardPile.Push(dealerDiscards);

            // If the deck became empty during the last round, repopulate and shuffle it and clear the discard pile
            if (Deck.IsEmpty) {

                Deck.Initialize();
                DiscardPile.Clear();

                Deck.Shuffle();

            }

            Dealer.Hand.AddCard(DealNextCard(true));
            Player.Hand.AddCard(DealNextCard(false));

            Dealer.Hand.AddCard(DealNextCard(false));
            Player.Hand.AddCard(DealNextCard(false));

            if (Player.Hand.IsBlackjack && Dealer.Hand.IsBlackjack)
                HandlePush();
            else if (Player.Hand.IsBlackjack)
                HandleWon();
            else if (Dealer.Hand.IsBlackjack)
                HandleLost();
            else
                GameState = GameStates.PlayersTurn;

            return true;
            
        }

        public bool Hit() {

            if (!IsHitAllowed)
                return false;

            Card card = DealNextCard(false);
            Player.Hand.AddCard(card);

            if(Player.Hand.IsBust)
                HandleLost();

            return true;

        }

        public bool Double() {

            if (!IsDoubleAllowed)
                return false;

            Player.HasDoubled = true;

            Hit();
            Stand();

            return true;

        }

        public bool Stand() {

            if (!IsStandAllowed)
                return false;

            Dealer.PlayTurn();

            if(Dealer.Hand.IsBust || Dealer.Hand.Value < Player.Hand.Value)
                HandleWon();
            else if(Dealer.Hand.Value == Player.Hand.Value)
                HandlePush();
            else 
                HandleLost();

            return true;

        }

        public Card DealNextCard(bool hideCard) {

            Card cardDrawn = null;

            if (!Deck.IsEmpty)
                cardDrawn = Deck.Draw();
            else {

                DiscardPile.Shuffle();
                cardDrawn = DiscardPile.Draw();

            }

            // Safety precaution
            if (cardDrawn == null)
                throw new InvalidOperationException("There are no cards to draw.");

            cardDrawn.IsHidden = hideCard;

            return cardDrawn;

        }

        private void HandlePush() {

            Dealer.Hand.Show();

            GameState = GameStates.Ended;

            Push?.Invoke(this, EventArgs.Empty);

        }

        private void HandleWon() {

            Dealer.Hand.Show();

            int prize = Player.CurrentBet;

            if (Player.Hand.IsBlackjack)
                prize = (int)Math.Ceiling(prize * 1.5f);
            else if (Player.HasDoubled)
                prize *= 2;

            Player.Balance += prize;

            GameState = GameStates.Ended;

            PlayerWon?.Invoke(this, EventArgs.Empty);

        }

        private void HandleLost() {

            Dealer.Hand.Show();
            Player.Balance -= Player.CurrentBet;
            Player.CurrentBet = MathUtility.Clamp(Player.CurrentBet, 0, Player.Balance); // TODO: maybe put this in setter of Player.Balance

            GameState = GameStates.Ended;

            PlayerLost?.Invoke(this, EventArgs.Empty);

        }

        #endregion

    }

}
