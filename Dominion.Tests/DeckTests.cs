using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dominion.Model;
using Dominion.Cards;

namespace Dominion.Tests
{
    [TestFixture]
    public class DeckTests
    {
        [Test]
        public void Tests()
        {
            Card village = new Village();
            Card copper = new Copper();
            Card gold = new Gold();
            Card chancellor = new Chancellor();

            IList<Card> cards = new List<Card>() { village, copper, gold, chancellor };

            Deck deck = new Deck(cards);

            Assert.That(deck.Count, Is.EqualTo(4));
            Card c = deck.Draw();

            Assert.That(deck.Count, Is.EqualTo(3));
            Assert.That(deck[0], Is.EqualTo(village));
            Assert.That(deck[1], Is.EqualTo(copper));
            Assert.That(deck[2], Is.EqualTo(gold));
            Assert.That(c, Is.EqualTo(chancellor));

            deck.AddToBottom(chancellor);
            Assert.That(deck.Count, Is.EqualTo(4));
            Assert.That(deck[0], Is.EqualTo(chancellor));
        }
    }
}
