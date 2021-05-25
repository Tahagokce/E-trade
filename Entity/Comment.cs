using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Abc.MvcWebUI.Entity.ViewModel;
using Abc.MvcWebUI.Identity;

namespace Abc.MvcWebUI.Entity
{
    public class Comment
    {


        [DisplayName("Id")]

        public int Id { get; set; }


        [Required]
        [DisplayName("Yorum")]

        public string Description { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Verilen değer 0-5 arasında olmalı.")]

        [DisplayName("Puan")]

        public int Star { get; set; }
        [Required]
        public string UserId { get; set; }
        [DisplayName("Kullanıcı Adı")]

        public string UserName { get; set; }
        [DisplayName("Ürün Id")]

        [Required]
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Oluşturulma Tarihi")]

        public DateTime CreateTime { get; set; }

        [DisplayName("Onay")]

        public bool IsApproved { get; set; }


        public Product Product { get; set; }

    }
}








