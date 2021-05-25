using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("İsim")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("Fiyat")]
        public double Price { get; set; }

        [DisplayName("Stok")]
        public int Stock { get; set; }

        [DisplayName("Fotoğraf")]
        public string İmage { get; set; }

        [DisplayName("Ana Sayfa")]
        public bool IsHome { get; set; }
        [DisplayName("Onay")]
        public bool IsApproved { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CommentId{ get; set; }
        public List<Comment> Comments{ get; set; }
    }
}