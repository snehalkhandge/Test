using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Common.Encryption
{
    public class EncryptPassword : IEncryptPassword
    {

        public string HashPassword(string input)
        {
            var salt = "$2a$10$aTsUwsyogePIsBO8qRqkj73rfaOI.Eq2r4G/76Wv39MzSX262hupznUOJfkEGSmYRfnkrPO";
            return DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(input, salt);
        }

        public string HashPassword(string input, string salt)
        {
            return DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(input, salt);
        }

    }
}
