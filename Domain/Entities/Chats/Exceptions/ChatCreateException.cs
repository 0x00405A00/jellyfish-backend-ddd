﻿using System.Runtime.Serialization;

namespace Domain.Entities.Chats.Exceptions
{
    public class ChatCreateException : System.Exception
    {
        public ChatCreateException()
        {
        }

        public ChatCreateException(string? message) : base(message)
        {
        }

        public ChatCreateException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

    }
}