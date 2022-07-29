using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Lütfen Yazar Adı Soyadı kısmını boş geçmeyiniz!");
            RuleFor(x => x.WriterEmail).NotEmpty().WithMessage("Lütfen Mail Adresi kısmını boş geçmeyiniz!");
            RuleFor(x => x.WriterEmail).EmailAddress().WithMessage("Lütfen Mail Adresinizi example@example.com şeklinde giriniz!");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre boş geçilemez!");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakterlik veri girişi yapınız!");
            RuleFor(x => x.WriterName).MaximumLength(50).WithMessage("Lütfen en fazla 50 karakterlik veri girişi yapınız!");
            RuleFor(x => x.WriterPassword).Must(CheckPassword).WithMessage("Lütfen daha güvenli bir şifre giriniz!");
        }

        private bool CheckPassword(string password)
        {
            var passwordPattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            return passwordPattern.IsMatch(password);
        }
    }
}
