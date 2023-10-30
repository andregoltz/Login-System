﻿using Login_System.Core.AccountContext.ValueObjects;
using Login_System.Core.Contexts.AccountContext.ValueObjects;
using Login_System.Core.Contexts.SharedContext.Entities;

namespace Login_System.Core.Contexts.AccountContext.Entities
{
    public class User : Entity
    {
        protected User() { }
        public User(string email, string? password = null)
        {
            Email = email;
            Password = new Password(password);
        }

        public User(string name, Email email, Password password)
        {
            Email = email;
            Password = password;
            Name = name;
        }
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; private set; } = string.Empty;

        public void UpdatePassword(string plainTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("0x07 - Código de restauração inválido");

            var password = new Password(plainTextPassword);
            Password = password;
        }

        public void UpdateEmail(Email email)
        {
            Email = email;
        }

        public void ChangePassword(string plainTextPassword)
        {
            var password = new Password(plainTextPassword);
            Password = password;
        }
    }
}
