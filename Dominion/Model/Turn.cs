using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Constants;

namespace Dominion.Model
{
    public class Turn
    {
        public Game Game { get; private set; }

        /// <summary>
        /// The owner of the deck and discard pile.  Not necessarily the active player
        /// </summary>
        public Player Owner { get; set; }

        /// <summary>
        /// For things like Possession, the turn owner might not be the person who calls the shots
        /// </summary>
        public Player Possessor { get; set; }

        /// <summary>
        /// When this turn is complete, do I re-enqueue the turn, or just remove it?
        /// </summary>
        public bool IsRepeatable { get; set; }

        public int ActionsRemaining { get; set; }
        public int BuysRemaining { get; set; }
        public int TreasureRemaining { get; set; }
        public Phases Phase { get; set; }
        public IList<Effect> PendingEffects { get; private set; }

        /// <summary>
        /// Represents non-actual cards played, i.e. Throne Room lets you play an action 3 times.  The action would be in this list 3 times, and in the CardsPlayed list once.  
        /// This is is cleared at the end of each turn.
        /// </summary>
        public IList<Card> CardsInPlay { get; private set; }

        /// <summary>
        /// Represents actual cards played, one instance per card.  
        /// At the end of the turn, these cards get moved to the discard pile.
        /// </summary>
        public IList<Card> CardsPlayed { get; private set; }
        public int TurnNumber { get; set; }

        public Turn(Player owner) 
        {
            Owner = owner;
            Possessor = null;
            IsRepeatable = true;
            ActionsRemaining = BuysRemaining = 1;
            TreasureRemaining = 0;
            Phase = Phases.Action;
            PendingEffects = new List<Effect>();
            CardsInPlay = new List<Card>();
            CardsPlayed = new List<Card>();
            TurnNumber = 1;
        }

        public Turn(Player owner, Player possessor, bool isRepeatable = false) 
            :this(owner)
        {
            Possessor = possessor;
            IsRepeatable = isRepeatable;
        }

        public void EndTurn()
        {
            CardsInPlay.Clear();
            Owner.DiscardPile.AddRange(CardsPlayed);
        }
    }
}
