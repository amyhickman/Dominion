using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;
using System.Linq.Expressions;
using System.Reflection;
using System.IO;
using Dominion;
using Dominion.Model;
using Dominion.Util;
using System.Threading;

namespace ConsoleTesting
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var events = typeof(Game).GetEvents();

            Player me = new Player(Thread.CurrentPrincipal);
            Player you = new Player(Thread.CurrentPrincipal);
            me.Nickname = "Jason";
            you.Nickname = "Jennifer";

            Game game = new Game();
            game.AddPlayer(me);
            game.AddPlayer(you);

            foreach (var c in new CardCode[] 
            { 
                CardCode.Bureaucrat, CardCode.Cellar, 
                CardCode.Chancellor, CardCode.Market, 
                CardCode.Militia, CardCode.Village, 
                CardCode.Witch, CardCode.Woodcutter ,
                CardCode.ThroneRoom, CardCode.Chapel
            })
                game.AddSupply(c);

            game.OnActionGain += new Action<int>(game_OnActionGain);
            game.OnBuyGain += new Action<int>(game_OnBuyGain);
            game.OnCardPlayed += new Action<Card>(game_OnCardPlayed);
            game.OnDrawCards += new Action<Player, IList<Card>>(game_OnDrawCards);
            game.OnGainCard += new Action<Player, CardCode>(game_OnGainCard);
            game.OnPossessedTurnStart += new Action<Player, Player>(game_OnPossessedTurnStart);
            game.OnPutCardOnDeck += new Action<Player, Card>(game_OnPutCardOnDeck);
            game.OnRevealCard += new Action<Player, Card>(game_OnRevealCard);
            game.OnRevealHand += new Action<Player, List<Card>>(game_OnRevealHand);
            game.OnShuffleDeck += new Action<Player>(game_OnShuffleDeck);
            game.OnTreasureGain += new Action<int>(game_OnTreasureGain);
            game.OnTurnEnd += new Action<Player>(game_OnTurnEnd);
            game.OnTurnStart += new Action<Player>(game_OnTurnStart);

            game.StartGame();

            
        }

        static void game_OnTurnStart(Player obj)
        {
            Console.WriteLine("It is now {0}'s turn", obj.Nickname);
        }

        static void game_OnTurnEnd(Player obj)
        {
            Console.WriteLine("Ending turn");
        }

        static void game_OnTreasureGain(int obj)
        {
            Console.WriteLine("Gained {0} treasure", obj);
        }

        static void game_OnShuffleDeck(Player obj)
        {
            Console.WriteLine("{0} shuffles his deck", obj.Nickname);
        }

        static void game_OnRevealHand(Player arg1, List<Card> arg2)
        {
            Console.WriteLine("{0} reveals his hand: {1}", arg1.Nickname, String.Join(", ", arg2.Select(c => c.Name).ToArray()));
        }

        static void game_OnRevealCard(Player arg1, Card arg2)
        {
            Console.WriteLine("{0} reveals the card {1}", arg1.Nickname, arg2.Name);
        }

        static void game_OnPutCardOnDeck(Player arg1, Card arg2)
        {
            Console.WriteLine("{0} puts a card ({1}) on his deck", arg2.Name);
        }

        static void game_OnPossessedTurnStart(Player arg1, Player arg2)
        {
            Console.WriteLine("{0} is possessing {1}'s turn!", arg1.Nickname, arg2.Nickname);
        }

        static void game_OnGainCard(Player arg1, CardCode arg2)
        {
            Console.WriteLine("{0} gains a {1}", arg1.Nickname, arg2);
        }

        static void game_OnDrawCards(Player arg1, IList<Card> arg2)
        {
            Console.WriteLine("{0} draws some cards:");
            foreach (var c in arg2)
            {
                Console.WriteLine("        #{0}: {1}", c.Id, c.Name);
            }
        }

        static void game_OnCardPlayed(Card obj)
        {
            Console.WriteLine("Plays a {0}", obj.Name);
        }

        static void game_OnBuyGain(int obj)
        {
            Console.WriteLine("Gains {0} buys", obj);
        }

        static void game_OnActionGain(int obj)
        {
            Console.WriteLine("Gains {0} actions", obj);
        }
    }

    //class Wooo : System.ServiceModel.Dispatcher.IClientMessageInspector
    //{

    //    public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
    //    {
    //    }

    //    public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
    //    {
    //        MessageHeader<string> header = new MessageHeader<string>("wark");
    //        MessageHeader untyped = header.GetUntypedHeader("headername", "namespace");
    //        request.Headers.Add(untyped);
    //        return null;
    //    }
    //}

    //class Woooo2 : IEndpointBehavior
    //{
    //    public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    //    {
    //    }

    //    public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
    //    {
    //        clientRuntime.MessageInspectors.Add(new Wooo());
    //    }

    //    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
    //    {
    //    }

    //    public void Validate(ServiceEndpoint endpoint)
    //    {
    //    }
    //}

    //class Extendy : BehaviorExtensionElement
    //{
    //    public override Type BehaviorType
    //    {
    //        get { return typeof(Woooo2); }
    //    }

    //    protected override object CreateBehavior()
    //    {
    //        return new Woooo2();
    //    }
    //}


    //class Bar : ServiceReference1.IDominionCallback, ServiceReference1.IDominion
    //{
    //    ServiceReference1.DominionClient client;
    //    public Bar()
    //    {
    //        InstanceContext context = new InstanceContext(this);
    //        DuplexChannelFactory<ServiceReference1.IDominionChannel> factory = new DuplexChannelFactory<ServiceReference1.IDominionChannel>(context);
            
    //        client = new ServiceReference1.DominionClient(context);
    //        client.ClientCredentials.UserName.UserName = "jason";
    //        Console.WriteLine("Opening connection");
    //        client.Open();
    //        Console.WriteLine("Connection open");

    //    }
    //    public void OnNotification(string message)
    //    {
    //        Console.WriteLine(message);
    //    }

    //    public void Chat(int roomId, string text)
    //    {
    //        client.Chat(roomId, text);
    //    }

    //    public void Login()
    //    {
    //        client.Login();
    //    }

    //    public void Logout()
    //    {
    //        client.Logout();
    //    }
    //}

}
