﻿using System.Runtime.Serialization;

namespace Domain.Entities.Chats.Exceptions
{
    public class ChatAlreadyDeletedException : System.Exception
    {
        public ChatAlreadyDeletedException()
        {
        }

        public ChatAlreadyDeletedException(string? message) : base(message)
        {
        }

        public ChatAlreadyDeletedException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

    }
}