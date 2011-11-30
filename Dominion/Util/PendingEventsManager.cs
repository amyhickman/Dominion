using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Model;

namespace Dominion.Util
{
    public class PendingEventsManager
    {
        private readonly Dictionary<Guid, PendingEvent> _pending = new Dictionary<Guid, PendingEvent>();
        public Game Game { get; private set; }

        public PendingEventsManager(Game g)
        {
            Game = g;
        }

        public void AddPendingEvent(PendingEvent pending)
        {
            lock (_pending)
            {
                _pending.Add(pending.Id, pending);
            }
        }

        public void ReceivePendingEventResponse(PendingEventResponse response)
        {
            PendingEvent request;
            lock (_pending)
            {
                request = _pending[response.PendingEventId];
            }

            if (request.IsSatisfiedByResponse(response))
                request.OnResponse(response);
            else if (request is PendingCardSelection)
            {
                SendPendingRequest((PendingCardSelection)request);
            }
            else if (request is PendingChoice)
            {
                SendPendingRequest((PendingChoice)request);
            }
        }

        public void SendPendingRequest(PendingCardSelection pending)
        {
            pending.Player.OnCardSelectionRequested(pending);
        }

        public void SendPendingRequest(PendingChoice pending)
        {
            pending.Player.OnChoiceRequested(pending);
        }
    }
}
