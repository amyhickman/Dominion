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
            Player me = new Player(Thread.CurrentPrincipal);
            Player you = new Player(Thread.CurrentPrincipal);
            Game game = new Game(new Player[] { me, you }, CardFactory.GetCardsInSet(CardSet.Base));
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
