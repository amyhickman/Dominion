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

        public bool AwaitingResponses
        {
            get { return _pending.Count > 0; }
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

            // TODO
            //if (request.IsSatisfiedByResponse(response))
            //    request.OnFulfillment(response);
            //else if (request is PendingCardSelection)
            //{
            //    SendPendingRequest((PendingCardSelection)request);
            //}
            //else if (request is PendingDecision)
            //{
            //    SendPendingRequest((PendingDecision)request);
            //}
        }

        public void SendPendingRequest(PendingCardSelection pending)
        {
            pending.Target.OnCardSelectionRequested(pending);
        }

        public void SendPendingRequest(PendingDecision pending)
        {
            pending.Target.OnChoiceRequested(pending);
        }
    }
}
