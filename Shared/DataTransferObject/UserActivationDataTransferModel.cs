﻿using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class UserActivationDataTransferModel : IDataTransferObject
    {
        [JsonPropertyName("activation_code")]
        public string ActivationCode { get; set; }
    }
}
