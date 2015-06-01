using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Constants;
using Dominion.Interfaces;
using Dominion.Util;

namespace Dominion.OldModel
{
    public class PlayContext
    {
        private SuppliesManager Supplies { get; set; }
        public Turn Turn { get; set; }
        public bool IsPossessed { get { return Turn.Possessor != null; } }
        public Player Possessor { get { return Turn.Possessor; } }
        public Player Owner { get { return Turn.Owner; } }
        public Player Actor { get { return Turn.Possessor ?? Turn.Owner; } }

        private PendingEventsManager _pendingManager;
        private Game _game;

        private IPlayCardResults _playResults = new PlayCardResults();

        public PlayContext(Game game, Turn turn, SuppliesManager suppliesManager, PendingEventsManager pendingManager)
        {
            _game = game;
            Turn = turn;
            _pendingManager = pendingManager;
        }

        public IPlayCardResults GetResults()
        {
            return _playResults;
        }

        /// <summary>
        /// Adds a pending event
        /// </summary>
        /// <param name="pending"></param>
        public void AddPendingEvent(PendingEvent pending)
        {
            _pendingManager.AddPendingEvent(pending);
        }

        #region Iterating across players
        public void ForEveryPlayer(Action<Player> act)
        {
            foreach (var p in _game.GetPlayers())
            {
                act(p);
            }
        }

        public void ForEachOtherPlayer(Action<Player> act)
        {
            foreach (var p in _game.GetPlayers())
            {
                if (p.Equals(Actor))
                    continue;
                act(p);
            }
        }
        #endregion

        #region DrawCards
        /// <summary>
        /// Current player, from his deck
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<Card> DrawCards(int count = 1)
        {
            if (IsPossessed)
                return DrawCards(Turn.Possessor, Turn.Owner.Deck, count);
            else
                return DrawCards(Turn.Owner, Turn.Owner.Deck, count);
        }

        /// <summary>
        /// Indicated player draws cards from his own deck
        /// </summary>
        /// <param name="p"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<Card> DrawCards(Player drawer, int count = 1)
        {
            return DrawCards(drawer, drawer.Deck, count);
        }


        public IList<Card> DrawCards(Player drawer, CardContainer deck, int count = 1)
        {
            IList<Card> retval = deck.Draw(count);
            _game.NotifyPlayers(
                p => p.OnDrawCards(drawer, retval), 
                p => p.OnDrawCardsNotVisible(drawer, count));
            return retval;
        }
        #endregion

        #region Gain action/treasure/buy
        /// <summary>
        /// Current player gains actions
        /// </summary>
        /// <param name="count"></param>
        public void GainAction(int count = 1)
        {
            _game.NotifyPlayers(p => p.OnActionGain(count));
            Turn.ActionsRemaining += count;
        }

        /// <summary>
        /// Current player gains treasures
        /// </summary>
        /// <param name="count"></param>
        public void GainTreasure(int count = 1)
        {
            _game.NotifyPlayers(p => p.OnTreasureGain(count));
            Turn.TreasureRemaining += count;
        }

        /// <summary>
        /// Current player gains buys
        /// </summary>
        /// <param name="count"></param>
        public void GainBuy(int count = 1)
        {
            _game.NotifyPlayers(p => p.OnBuyGain(count));
            Turn.BuysRemaining += count;
        }
        #endregion

        #region RevealCard
        /// <summary>
        /// The current player reveals a card
        /// </summary>
        /// <param name="card">The card being revealed</param>
        public void RevealCard(Card card)
        {
            RevealCard(Owner, card);
        }

        /// <summary>
        /// The targeted player reveals a card
        /// </summary>
        /// <param name="revealer">The player revealing the card</param>
        /// <param name="card">The card being revealed</param>
        public void RevealCard(Player revealer, Card card)
        {
            _game.NotifyPlayers(p => p.OnRevealCard(revealer, card));
        }
        #endregion

        #region GainCard
        /// <summary>
        /// The current player gains a card
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Card GainCard(CardCode code)
        {
            return GainCard(Actor, code);
        }

        /// <summary>
        /// The targeted player gains a card
        /// </summary>
        /// <param name="target">The player gaining the card</param>
        /// <param name="code">The card being gained</param>
        /// <returns></returns>
        public Card GainCard(Player target, CardCode code)
        {
            if (!Supplies.HasSupply(code))
                throw new ArgumentOutOfRangeException("Supply piles do not include " + code.ToString());

            Card retval = null;
            CardContainer pile = Supplies[code];

            if (pile.Count > 0)
            {
                retval = pile.Draw();
            }

            _game.NotifyPlayers(p => p.OnGainCard(target, code));
            return retval;
        }
        #endregion

        #region PutCardOnDeck
        /// <summary>
        /// The current player puts a card on top of his deck
        /// </summary>
        /// <param name="card"></param>
        public void PutCardOnDeck(Card card)
        {
            PutCardOnDeck(Owner, card);
        }

        /// <summary>
        /// The targeted player puts a card on top of his deck
        /// </summary>
        /// <param name="target"></param>
        /// <param name="card"></param>
        public void PutCardOnDeck(Player target, Card card)
        {
            _game.NotifyPlayers(
                p => p.OnPutCardOnDeck(target, card),
                p => p.OnPutCardOnDeckNotVisible(target));

            target.Deck.AddToTop(card);
        }
        #endregion

        #region RevealHand
        /// <summary>
        /// The current player reveals his hand
        /// </summary>
        public void RevealHand()
        {
            RevealHand(Owner);
        }

        /// <summary>
        /// The targeted player reveals his hand
        /// </summary>
        /// <param name="target">The hand revealer</param>
        public void RevealHand(Player target)
        {
            _game.NotifyPlayers(p=> p.OnRevealHand(target, target.Hand.ToList()));
        }
        #endregion

        #region ShuffleDeck
        /// <summary>
        /// The current player shuffles his deck
        /// </summary>
        public void ShuffleDeck()
        {
            ShuffleDeck(Owner);
        }

        /// <summary>
        /// The targeted player shuffles his deck
        /// </summary>
        /// <param name="target">The deck shuffler</param>
        public void ShuffleDeck(Player target)
        {
            target.Deck.Shuffle();
            _game.NotifyPlayers(p => p.OnShuffleDeck(target));
        }
        #endregion

    }
}
