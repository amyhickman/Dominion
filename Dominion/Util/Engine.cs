using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

using Dominion.Interfaces;
using Dominion.Model;
using Dominion.ServiceModel;


namespace Dominion.Util
{
    public static class Engine
    {
        private static readonly object _lck = new object();
        private static bool _isInited = false;

        public static void Init()
        {
            lock (_lck)
            {
                if (_isInited)
                    return;

                _isInited = true;

                Mapper.CreateMap<ICardMetadata, CardResponse>();
            }
        }
    }
}
