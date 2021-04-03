using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace wpfLoginForm
{
    public class User
    {
        public SecureString Password {private get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhysAddress { get; set; }
        public string UserName { get; set; }
        public User()
        {

        }
    }
}
