﻿namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message)
        {

        }
        public InternalServerException(string message, string details) : base(message)
        {
            Detail = details;
        }
        public string? Detail { get; }  
    }
}
