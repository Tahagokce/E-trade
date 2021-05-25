using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Identity
{
    public class Register
    {
        [Required]
        [DisplayName("İsim")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyad")]
        public string SurName { get; set; }

        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "E-posta adresinizi kontrol ediniz")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required]                                                          // doldurulması zorunlu alan olduğunu belirtir
        [DisplayName("Şifre tekrar")]                                       //sayfada gözükecek olan adını burada belirtiyoruz
        [Compare("Password", ErrorMessage = "Şifreleriniz Uyuşmuyor")]// Girilen passwordün eşleşip eşleşmediğini inceler
        public string RePassword { get; set; }
    }
}