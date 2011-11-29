using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using Dominion.Util;

namespace Dominion.ServiceModel
{
    class CardService : DominionGetServiceBase<CardRequest>
    {
        public override object Get(CardRequest request)
        {
            var meta = CardDirectory.GetCardMeta(request.Code);
            var response = new CardResponse();
            AutoMapper.Mapper.Map(meta, response);
            return response;
        }
    }
}
