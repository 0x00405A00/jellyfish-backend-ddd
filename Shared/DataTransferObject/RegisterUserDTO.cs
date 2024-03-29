﻿using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class RegisterUserDTO : AbstractUserDTO
    {
        [JsonPropertyName("user_name")]
        public string? UserName { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("password_repeat")]
        public string? PasswordRepeat { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("picture_base64")]
        public string? PictureBase64 { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }


        public RegisterUserDTO()
        {

        }
    }
}
