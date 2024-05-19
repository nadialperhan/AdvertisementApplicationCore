using AdvertisementApp.UI.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.ValidationRules
{
    public class UserCreateModelValidator:AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez");
            RuleFor(x => x.Password).MinimumLength(3).WithMessage("Parola minimum 3 karakter olmalı");
            RuleFor(x => x.Password).Equal(x=>x.ConfirmPassword).WithMessage("Şifre eşleşmedi");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş geçilemez");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Soyad boş geçilemez");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı boş geçilemez");

            RuleFor(x => x.UserName).MinimumLength(3);
            RuleFor(x => new {
                x.UserName,
                x.FirstName
            }).Must(x=>CannotFirstName(x.UserName,x.FirstName)).WithMessage("kullanıcı adı adınızı içermemeli").When(x=>x.UserName!=null&&x.FirstName!=null);
            RuleFor(x => x.GenderID).NotEmpty().WithMessage("Cinsiyet Seçiniz");
           


        }

        private bool CannotFirstName(string username,string firstname)
        {
            return !username.Contains(firstname);
        }
    }
}
