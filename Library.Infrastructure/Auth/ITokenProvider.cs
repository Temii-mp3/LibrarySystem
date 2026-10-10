using LibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Auth
{
    public interface ITokenProvider
    {
        public string Create(Account account);
    }
}
