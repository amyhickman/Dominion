using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Util;
using log4net;
using System.Diagnostics;
using Dominion.Interfaces;
using Dominion.Constants;

namespace Dominion.Model
{
    public class Game
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Game));

        private readonly List<Player> _players = new List<Player>();
        private readonly bool _started = false;
        private readonly CardManager CardMan;
        private readonly SuppliesManager SupplyMan;
        private readonly TurnManager TurnMan;
        private readonly Dictionary<Guid, PendingEvent> _pendingselections = new Dictionary<Guid, PendingEvent>();

        private Turn CurrentTurn { get { return TurnMan.Current; } }
                
        #region Constructors
        internal Game(IList<Player> players, IList<CardCode> supplies)
        {
            _players.AddRange(players);

            CardMan = new CardManager(this);
            SupplyMan = new SuppliesManager(this, CardMan, supplies);
            TurnMan = new TurnManager(this, true);

            foreach (var p in _players)
            {
                p.Deck.AddRange(CardMan.CreateCards(CardCode.Copper, 7));
                p.Deck.AddRange(CardMan.CreateCards(CardCode.Estate, 3));
                p.Deck.Shuffle();
                DrawCards(p, 5);
            }

            _started = true;
        }
        #endregion
        
        #region Player management
        public IList<Player> GetPlayers() { return new List<Player>(_players); }
        
        private Player CurrentPossessor { get { return CurrentTurn.Possessor; } }
        public Player CurrentPlayer { get { return CurrentTurn.Owner; } }

        internal void ForEachOtherPlayer(Action<Player> action)
        {
            foreach (var p in _players)
            {
                if (p.Equals(CurrentPlayer))
                    continue;
                action(p);
            }
        }
        #endregion

        #region Game event management
        internal void AddPendingEvent(PendingEvent pending)
        {
            _pendingselections.Add(pending.Id, pending);
        }

        private void ReceivePendingEventResponse(PendingEventResponse response)
        {
            PendingEvent request = _pendingselections[response.PendingEventId];

            if (request.IsSatisfiedByResponse(response))
                request.OnResponse(response);
            else if (request is PendingCardSelection)
            {
                SendPendingRequest((PendingCardSelection)request);
            }
            else if (request is PendingChoice)
            {
                SendPendingRequest((PendingChoice)request);
            }
        }

        private void SendPendingRequest(PendingCardSelection pending)
        {
            pending.Player.OnCardSelectionRequested(pending);
        }

        private void SendPendingRequest(PendingChoice pending)
        {
            pending.Player.OnChoiceRequested(pending);
        }

        #endregion

        #region Game activity - All of these should be internal
        internal void RevealCard(Player revealer, Card card)
        {
            foreach (var p in _players)
            {
                p.OnRevealCard(revealer, card);
            }
        }

        internal void GainAction(int count = 1)
        {
            _players.ForEach(p=> p.OnActionGain(count));            
            CurrentTurn.ActionsRemaining += count;
        }

        internal void GainTreasure(int count = 1)
        {
            _players.ForEach(p => p.OnTreasureGain(count));
            CurrentTurn.TreasureRemaining += count;
        }

        internal IList<Card> DrawCards(int count)
        {
            return DrawCards(CurrentPlayer, count);
        }

        internal IList<Card> DrawCards(Player target, int count)
        {
            IList<Card> retval = target.Deck.Draw(count);

            foreach (var p in _players)
            {
                if (p.Equals(target))
                    p.OnDrawCards(target, retval);
                else
                    p.OnDrawCardsNotVisible(target, count);
            }
            return retval;
        }

        internal Card GainCard(CardCode code)
        {
            return GainCard(CurrentPlayer, code);           
        }

        internal Card GainCard(Player target, CardCode code)
        {
            if (!SupplyMan.HasSupply(code)) 
                throw new ArgumentOutOfRangeException("Supply piles do not include " + code.ToString());

            Card retval = null;
            CardContainer pile = SupplyMan[code];

            if (pile.Count > 0)
            {
                retval = pile.Draw();
            }

            _players.ForEach(p => p.OnGainCard(target, code));
            return retval;
        }

        internal void GainBuy(int count = 1)
        {
            _players.ForEach(p => p.OnBuyGain(count));
            CurrentTurn.BuysRemaining += count;
        }

        internal void PlayCard(Guid cardId)
        {
            Card c = CardMan[cardId];

            //
            // Make sure the action can be played.  i.e. it needs to be in the players hand
            //
            if (!c.Container.Equals(CurrentTurn.Owner.Hand))
                return;

            _players.ForEach(p => p.OnCardPlayed(c));
            c.OnPlay();
        }

        internal void PutCardOnDeck(Card card)
        {
            foreach (var p in _players)
            {
                if (p.Equals(CurrentPlayer))
                    p.OnPutCardOnDeck(CurrentPlayer, card);
                else
                    p.OnPutCardOnDeckNotVisible(CurrentPlayer);
            }
            
            CurrentPlayer.Deck.AddToTop(card);
        }

        internal void RevealHand(Player target)
        {
            _players.ForEach(p=> p.OnRevealHand(target, target.Hand.ToList()));
        }

        internal void ShuffleDeck(Player target)
        {
            target.Deck.Shuffle();
            _players.ForEach(p=> p.OnShuffleDeck(target));
        }
        #endregion


    }
}