using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;
using Dominion.OldModel;

namespace Dominion.Util
{
    public class PlayOption
    {
        public Player Player { get; set; }
        public bool MayDecline { get; set; }
        public PlayOptionCode Code { get; set; }

        public PlayOption(Player p, PlayOptionCode c, bool mayDecline)
        {
            Player = p;
            MayDecline = mayDecline;
            Code = c;
        }
    }

    public enum PlayOptionCode
    {
        EndTurn,
        PlayAction,
        BuyCard,
        ChooseCard
    }
    

    public class PlayOptionsManager
    {
        public Stack<List<PlayOption>> _stack = new Stack<List<PlayOption>>();


        public void SetupNewTurn(Turn t)
        {
            lock (_stack)
            {
                _stack.Clear();
                var lst = new List<PlayOption>();
                _stack.Push(lst);
                lst.Add(new PlayOption(t.Possessor ?? t.Owner, PlayOptionCode.PlayAction, true));
                lst.Add(new PlayOption(t.Possessor ?? t.Owner, PlayOptionCode.BuyCard, true));
                lst.Add(new PlayOption(t.Possessor ?? t.Owner, PlayOptionCode.EndTurn, true));
            }
        }

        public bool IsTurnEndable()
        {
            if (_stack.Count <= 1)
                return true;

            foreach (var x in _stack)
            {
                if (x.Any(x2 => !x2.MayDecline))
                    return false;
            }

            return true;
        }

        public void AddActionableItem(PlayOption po)
        {
            if (_stack.Count > 0)
                _stack.Peek().Add(po);
            else
            {
                _stack.Push(new List<PlayOption>(new PlayOption[] { po }));
            }
        }

        public IList<PlayOption> GetOptions()
        {
            if (_stack.Count > 0)
                return new List<PlayOption>(_stack.Peek());
            else
                return new List<PlayOption>();
        }
    }

    
}
