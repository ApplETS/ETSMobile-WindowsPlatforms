﻿using Ets.Mobile.Business.Entities.Results.Signets.Converters;
using Ets.Mobile.Business.Entities.Results.Signets.Interfaces;
using Ets.Mobile.Business.Entities.Signets;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Results.Signets
{
    [JsonConverter(typeof(GenericConverter))]
    public class TeammatesResult : ResultBase, ITeammates
    {
        [JsonProperty("liste")]
        public List<Teammate> Teammates { get; set; }
    }
}