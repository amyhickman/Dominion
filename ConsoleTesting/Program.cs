using System;
using Dominion.Constants;
using Dominion.Model;
using Dominion.Interfaces;
using Dominion.Cards;
using System.Linq.Expressions;
using AutoMapper;
using Dominion.Util;
using System.Collections.Generic;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConsolePlayer me = new ConsolePlayer();
            //ConsolePlayer you = new ConsolePlayer();
            //me.Nickname = "Jason";
            //you.Nickname = "Jennifer";

            //GameFactory setup = new GameFactory();
            //setup.AddPlayer(me);
            //setup.AddPlayer(you);

            //foreach (var c in new CardCode[] 
            //{ 
            //    CardCode.Bureaucrat, CardCode.Cellar, 
            //    CardCode.Chancellor, CardCode.Market, 
            //    CardCode.Militia, CardCode.Village, 
            //    CardCode.Witch, CardCode.Woodcutter ,
            //    CardCode.ThroneRoom, CardCode.Chapel
            //})
            //{
            //    setup.DesiredSupplies.Add(c);
            //}

            //Game g = setup.CreateGame();

            PlayOptionsManager man = new PlayOptionsManager();
            man.SetupNewTurn(new Turn(new ConsolePlayer()));
            man._stack.Push(new List<PlayOption>());
            //man._stack.Peek().Add(new PlayOption(null, PlayOptionCode.EndTurn, false));
            Console.WriteLine(man.CanEndTurn());
        }

    }
}
