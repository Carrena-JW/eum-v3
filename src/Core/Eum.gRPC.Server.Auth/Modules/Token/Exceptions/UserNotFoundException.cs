﻿namespace Eum.gRPC.Server.Auth.Modules.Token.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username, string message = null) : base(message ?? username)
        {
            this.UserName = username;
        }

        public string UserName { get; private set; }
    }
}
