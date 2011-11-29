using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Interfaces;
using Dominion.Constants;
using Dominion.Model;

namespace Dominion.Util
{
    public class DummyObserver : IGameObserver
    {
        public void OnTurnStart(Player activePlayer) { }

        public void OnTurnEnd(Player activePlayer) { }

        public void OnActionGain(int count) { }

        public void OnBuyGain(int count) { }

        public void OnTreasureGain(int count) { }

        public void OnRevealCard(Player revealingPlayer, Card revealedCard) { }

        public void OnRevealHand(Player revealingPlayer, IList<Card> revealedCards) { }

        public void OnDrawCards(Player drawingPlayer, IList<Card> drawnCards) { }

        public void OnGainCard(Player gainingPlayer, CardCode gainedCardCode) { }

        public void OnCardPlayed(Card playedCard) { }

        public void OnPutCardOnDeck(Player player, Card cardOnDeck) { }

        public void OnShuffleDeck(Player shuffler) { }

        public void OnPossessedTurnStart(Player possessingPlayer, Player possessedPlayer) { }

        public void OnDrawCardsNotVisible(Player drawingPlayer, int count) { }

        public void OnPutCardOnDeckNotVisible(Player player) { }

        public void OnCardSelectionRequested(PendingCardSelection pendingSelection) { }

        public void OnChoiceRequested(PendingChoice choice) { }
    }
}
