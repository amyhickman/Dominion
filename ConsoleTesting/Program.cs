using System;
using Dominion.Constants;
using Dominion.Model;
using Dominion.Interfaces;
using Dominion.Cards;
using Dominion.ServiceModel;
using System.Linq.Expressions;
using AutoMapper;
using Dominion.Util;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsolePlayer me = new ConsolePlayer();
            ConsolePlayer you = new ConsolePlayer();
            me.Nickname = "Jason";
            you.Nickname = "Jennifer";

            GameSetup setup = new GameSetup();
            setup.AddPlayer(me);
            setup.AddPlayer(you);

            foreach (var c in new CardCode[] 
            { 
                CardCode.Bureaucrat, CardCode.Cellar, 
                CardCode.Chancellor, CardCode.Market, 
                CardCode.Militia, CardCode.Village, 
                CardCode.Witch, CardCode.Woodcutter ,
                CardCode.ThroneRoom, CardCode.Chapel
            })
            {
                setup.DesiredSupplies.Add(c);
            }

            Game g = setup.CreateGame();
            
        }

    }
}
