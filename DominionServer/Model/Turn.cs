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
        /// For things like Possession, the turn owner might not be the person who owns the deck
        /// </summary>
        public Player Actor { get; set; }

        /// <summary>
        /// When this turn is complete, do I re-enqueue the turn, or just remove it?
        /// </summary>
        public bool IsRepeatable { get; set; }

        public int ActionsRemaining { get; set; }
        public int BuysRemaining { get; set; }
        public int TreasureRemaining { get; set; }
        public Phases CurrentPhase { get; set; }
        public bool IsPossessed { get; set; }
        public List<Effect> PendingEffects { get; private set; }

        public Turn(Player owner, bool isRepeatable = true, bool isPossessed = false) 
            : this(owner, owner, isRepeatable, isPossessed)
        {}

        public Turn(Player owner, Player actor, bool isRepeatable = false, bool isPossessed = false) 
        {
            Owner = owner;
            Actor = actor;
            IsRepeatable = isRepeatable;
            ActionsRemaining = BuysRemaining = 1;
            TreasureRemaining = 0;
            CurrentPhase = Phases.Action;
            IsPossessed = isPossessed;
            PendingEffects = new List<Effect>();
        }

        public void GainAction(int count = 1) { }
        public void GainBuy(int count = 1) { }
        public void DrawCard(int count = 1) { }
        public Card GainCard(CardCode code) { throw new NotImplementedException(); }

        public void GainCoin(int count = 1)
        {
            throw new NotImplementedException();
        }


    }
}
