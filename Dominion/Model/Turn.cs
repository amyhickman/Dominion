using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public Phases CurrentPhase { get; set; }
        public bool IsPossessed { get { return Possessor != null; } }
        public List<Effect> PendingEffects { get; private set; }

        public Turn(Player owner) 
        {
            Owner = owner;
            Possessor = null;
            IsRepeatable = true;
            ActionsRemaining = BuysRemaining = 1;
            TreasureRemaining = 0;
            CurrentPhase = Phases.Action;
            PendingEffects = new List<Effect>();
        }

        public Turn(Player owner, Player possessor, bool isRepeatable = false) 
            :this(owner)
        {
            Possessor = possessor;
            IsRepeatable = isRepeatable;
        }
    }
}
