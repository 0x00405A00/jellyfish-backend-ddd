﻿using Shared.DataTransferObject.Abstraction;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class FriendshipRequestDTO : AbstractDTO
    {
        [JsonPropertyName("requester_user")]
        public string? RequesterUserName { get; set; }
        [JsonPropertyName("target_user")]
        public string? TargetUserName { get; set; }
        [JsonPropertyName("target_user_request_message")]
        public string? TargetUserRequestMessage { get; set; }
        [JsonPropertyName("requester_user_id")]
        public Guid RequestUserForeignKey { get; set; }
        [JsonPropertyName("target_user_id")]
        public Guid TargetUserForeignKey { get; set; }
    }
}