﻿using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class PasswordResetRequestDTO
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}