using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Abc.MvcWebUI.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }  //Rollerimiz için açıklama oluşturmak istedik 


        public ApplicationRole()
        {

        }
        public ApplicationRole(string roleName, string description)
        {
            this.Description = description;
        }
    }
}