using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;

namespace Dominion.Interfaces
{
    public interface IGameObserver
    {
        /// <summary>
        /// Signals when a new turn starts
        /// </summary>
        /// <param name="activePlayer">The player who's turn it now is</param>
        void OnTurnStart(Player activePlayer);

        /// <summary>
        /// Signals when a turn ends.
        /// </summary>
        /// <param name="activePlayer">The player who's turn just ended</param>
        void OnTurnEnd(Player activePlayer);

        /// <summary>
        /// Signals when the active player gains additional actions
        /// </summary>
        /// <param name="count">How many actions the player gained</param>
        void OnActionGain(int count);

        /// <summary>
        /// Signals when the active player gains additional buys
        /// </summary>
        /// <param name="count">How many buys the player gained</param>
        void OnBuyGain(int count);

        /// <summary>
        /// Signals when the active player gains additional coin (not treasure cards)
        /// </summary>
        /// <param name="count">The number of coin gained</param>
        void OnTreasureGain(int count);

        /// <summary>
        /// Signals when a player reveals a card
        /// </summary>
        /// <param name="revealingPlayer">The revealing player</param>
        /// <param name="revealedCard">The card revealed</param>
        void OnRevealCard(Player revealingPlayer, Card revealedCard);

        /// <summary>
        /// Signals when a player reveals his/her hand
        /// </summary>
        /// <param name="revealingPlayer">The revealing player</param>
        /// <param name="revealedCards">The revealed cards</param>
        void OnRevealHand(Player revealingPlayer, IList<Card> revealedCards);

        /// <summary>
        /// Signals when a player has drawn some cards
        /// </summary>
        /// <param name="drawingPlayer">The player drawing the cards</param>
        /// <param name="drawnCards">The cards drawn</param>
        void OnDrawCards(Player drawingPlayer, IList<Card> drawnCards);

        /// <summary>
        /// Signals when a player gains a card
        /// </summary>
        /// <param name="gainingPlayer">The player gaining the card</param>
        /// <param name="gainedCardCode">The card code gained</param>
        void OnGainCard(Player gainingPlayer, CardCode gainedCardCode);

        /// <summary>
        /// Signals when a card was played
        /// </summary>
        /// <param name="playedCard">The card played</param>
        void OnCardPlayed(Card playedCard);

        /// <summary>
        /// Signals when a player puts a card on his/her deck
        /// </summary>
        /// <param name="player">The player moving the card to his deck</param>
        /// <param name="cardOnDeck">The card being moved</param>
        void OnPutCardOnDeck(Player player, Card cardOnDeck);

        /// <summary>
        /// Signals when a player shuffles his deck
        /// </summary>
        /// <param name="shuffler">The player doing the shuffling</param>
        void OnShuffleDeck(Player shuffler);

        /// <summary>
        /// Signals when a Possession turn starts
        /// </summary>
        /// <param name="possessingPlayer">The player who is doing the possessing</param>
        /// <param name="possessedPlayer">The possessed</param>
        void OnPossessedTurnStart(Player possessingPlayer, Player possessedPlayer);
    }
}
