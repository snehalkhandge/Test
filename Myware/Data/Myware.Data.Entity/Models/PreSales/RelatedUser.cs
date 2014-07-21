using Myware.Data.Entity.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Data.Entity.Models.PreSales
{
    public class RelatedUser
    {
        public int Id { get; set; }

        public int UserId { get; set;}

        public User User { get; set; }

        public int ContactEnquiryId { get; set; }

        public ContactEnquiry ContactEnquiry { get; set; }

    }
}
