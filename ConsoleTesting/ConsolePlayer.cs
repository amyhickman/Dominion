using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;
using Dominion.Constants;

namespace ConsoleTesting
{
    class ConsolePlayer : Player
    {
        public string Nickname { get; set; }

        public override void OnTurnStart(Player obj)
        {
            Console.WriteLine("It is now {0}'s turn", ((ConsolePlayer)obj).Nickname);
        }

        public override void OnTurnEnd(Player obj)
        {
            Console.WriteLine("Ending turn");
        }

        public override void OnTreasureGain(int obj)
        {
            Console.WriteLine("Gained {0} treasure", obj);
        }

        public override void OnShuffleDeck(Player obj)
        {
            Console.WriteLine("{0} shuffles his deck", ((ConsolePlayer)obj).Nickname);
        }

        public override void OnRevealHand(Player arg1, IList<Card> arg2)
        {
            Console.WriteLine("{0} reveals his hand: {1}", ((ConsolePlayer)arg1).Nickname, String.Join(", ", arg2.Select(c => c.Name).ToArray()));
        }

        public override void OnRevealCard(Player arg1, Card arg2)
        {
            Console.WriteLine("{0} reveals the card {1}", ((ConsolePlayer)arg1).Nickname, arg2.Name);
        }

        public override void OnPutCardOnDeck(Player arg1, Card arg2)
        {
            Console.WriteLine("{0} puts a card ({1}) on his deck", arg2.Name);
        }

        public override void OnPossessedTurnStart(Player arg1, Player arg2)
        {
            Console.WriteLine("{0} is possessing {1}'s turn!", ((ConsolePlayer)arg1).Nickname, ((ConsolePlayer)arg2).Nickname);
        }

        public override void OnGainCard(Player arg1, CardCode arg2)
        {
            Console.WriteLine("{0} gains a {1}", ((ConsolePlayer)arg1).Nickname, arg2);
        }

        public override void OnDrawCards(Player arg1, IList<Card> arg2)
        {
            Console.WriteLine("{0} draws some cards:");
            foreach (var c in arg2)
            {
                Console.WriteLine("        #{0}: {1}", c.Id, c.Name);
            }
        }

        public override void OnCardPlayed(Card obj)
        {
            Console.WriteLine("Plays a {0}", obj.Name);
        }

        public override void OnBuyGain(int obj)
        {
            Console.WriteLine("Gains {0} buys", obj);
        }

        public override void OnActionGain(int obj)
        {
            Console.WriteLine("Gains {0} actions", obj);
        }

        public override void OnDrawCardsNotVisible(Player drawingPlayer, int count)
        {
            Console.WriteLine("{0} draws {1} cards", ((ConsolePlayer)drawingPlayer).Nickname, count);
        }

        public override void OnPutCardOnDeckNotVisible(Player player)
        {
            Console.WriteLine("{0} put a card on his deck", ((ConsolePlayer)player).Nickname);
        }

        public override void OnCardSelectionRequested(PendingCardSelection pendingSelection)
        {
            throw new NotImplementedException();
        }


        public override void OnChoiceRequested(PendingChoice choice)
        {
            throw new NotImplementedException();
        }
    }
}
