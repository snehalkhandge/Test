using Myware.Data.Entity.Models.PostSales.Booking;
using Myware.Web.Models.PostSales.Booking;
using Myware.Web.Models.PostSalesUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myware.Web.Models.PostSales
{
    public class DocumentManagementViewModel
    {
        public int Id { get; set; }

        
        public string FileUrl { get; set; }

        
        public string DocumentName { get; set; }

        
        public string Remark { get; set; }


        public int ProjectId { get; set; }

        public ProjectBaseViewModel Project { get; set; }

        public int BookingDetailId { get; set; }

        public BookingDetailViewModel BookingDetail { get; set; }


    }
}