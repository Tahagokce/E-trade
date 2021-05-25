using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class GetCategoryViewModel
    { 
        public int Id{ get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public int Count { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}