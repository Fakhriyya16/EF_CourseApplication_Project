using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string msg): base(msg) { }
    }
}
