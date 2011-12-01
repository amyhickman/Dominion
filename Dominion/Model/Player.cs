using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominion.Interfaces;
using Dominion.Constants;

namespace Dominion.Model
{
    public abstract class Player
    {
        internal Game Game { get; set; }
        public CardContainer Hand { get; private set; }
        public CardContainer Deck { get; private set; }
        public CardContainer DiscardPile { get; private set; }
        public int Id { get; set; }
        public int VictoryPoints { get; set; }

        public Player()
        {
            Hand = new CardContainer(this);
            Deck = new CardContainer(this);
            DiscardPile = new CardContainer(this);
        }

        public void PlayCard(Card c)
        {
            Game.PlayCard(this, c);
        }

        public void ChooseCards(PendingCardSelectionResponse response)
        {
            Game.ReceivePendingEventResponse(response);
        }

        public void MakeDecision(PendingDecisionResponse response)
        {
            Game.ReceivePendingEventResponse(response);
        }

        /// <summary>
        /// Signals when a new turn starts
        /// </summary>
        /// <param name="activePlayer">The player who's turn it now is</param>
        public abstract void OnTurnStart(Player activePlayer);

        /// <summary>
        /// Signals when a turn ends.
        /// </summary>
        /// <param name="activePlayer">The player who's turn just ended</param>
        public abstract void OnTurnEnd(Player activePlayer);

        /// <summary>
        /// Signals when the active player gains additional actions
        /// </summary>
        /// <param name="count">How many actions the player gained</param>
        public abstract void OnActionGain(int count);

        /// <summary>
        /// Signals when the active player gains additional buys
        /// </summary>
        /// <param name="count">How many buys the player gained</param>
        public abstract void OnBuyGain(int count);

        /// <summary>
        /// Signals when the active player gains additional coin (not treasure cards)
        /// </summary>
        /// <param name="count">The number of coin gained</param>
        public abstract void OnTreasureGain(int count);

        /// <summary>
        /// Signals when a player reveals a card
        /// </summary>
        /// <param name="revealingPlayer">The revealing player</param>
        /// <param name="revealedCard">The card revealed</param>
        public abstract void OnRevealCard(Player revealingPlayer, Card revealedCard);

        /// <summary>
        /// Signals when a player reveals his/her hand
        /// </summary>
        /// <param name="revealingPlayer">The revealing player</param>
        /// <param name="revealedCards">The revealed cards</param>
        public abstract void OnRevealHand(Player revealingPlayer, IList<Card> revealedCards);

        /// <summary>
        /// Signals when a player has drawn some cards
        /// </summary>
        /// <param name="drawingPlayer">The player drawing the cards</param>
        /// <param name="drawnCards">The cards drawn</param>
        public abstract void OnDrawCards(Player drawingPlayer, IList<Card> drawnCards);

        /// <summary>
        /// Signals when a player has drawn some cards, but those cards are not visible
        /// </summary>
        /// <param name="drawingPlayer"></param>
        /// <param name="count"></param>
        public abstract void OnDrawCardsNotVisible(Player drawingPlayer, int count);

        /// <summary>
        /// Signals when a player gains a card
        /// </summary>
        /// <param name="gainingPlayer">The player gaining the card</param>
        /// <param name="gainedCardCode">The card code gained</param>
        public abstract void OnGainCard(Player gainingPlayer, CardCode gainedCardCode);

        /// <summary>
        /// Signals when a card was played
        /// </summary>
        /// <param name="playedCard">The card played</param>
        public abstract void OnCardPlayed(Card playedCard);

        /// <summary>
        /// Signals when a player puts a card on his/her deck
        /// </summary>
        /// <param name="player">The player moving the card to his deck</param>
        /// <param name="cardOnDeck">The card being moved</param>
        public abstract void OnPutCardOnDeck(Player player, Card cardOnDeck);

        /// <summary>
        /// Signals when a player puts a card on his/her deck but that card is not visible
        /// </summary>
        /// <param name="player"></param>
        public abstract void OnPutCardOnDeckNotVisible(Player player);

        /// <summary>
        /// Signals when a player shuffles his deck
        /// </summary>
        /// <param name="shuffler">The player doing the shuffling</param>
        public abstract void OnShuffleDeck(Player shuffler);

        /// <summary>
        /// Signals when a Possession turn starts
        /// </summary>
        /// <param name="possessingPlayer">The player who is doing the possessing</param>
        /// <param name="possessedPlayer">The possessed</param>
        public abstract void OnPossessedTurnStart(Player possessingPlayer, Player possessedPlayer);

        /// <summary>
        /// Signals when a player needs to select some cards from a list of options
        /// </summary>
        /// <param name="pendingSelection">The cards available to choose from and how many need to be chosen</param>
        public abstract void OnCardSelectionRequested(PendingCardSelection pendingSelection);

        /// <summary>
        /// Signals when a player needs to make a yes/no decision
        /// </summary>
        /// <param name="choosingPlayer">The player making the decision</param>
        /// <param name="choice">The description of the choice being made</param>
        public abstract void OnChoiceRequested(PendingDecision choice);

    }
}