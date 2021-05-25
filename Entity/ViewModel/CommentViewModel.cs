using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Abc.MvcWebUI.Identity;

namespace Abc.MvcWebUI.Entity.ViewModel
{
    public class CommentViewModel
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public int ProductId { get; set; }
        public int Star { get; set; }
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateTime { get; set; }

        public Product Product { get; set; }
        public string UserName { get; set; }




    }
}