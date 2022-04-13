using Les1.DAL;
using Les1.Interface;
using FluentValidation;

namespace Les1.Validations
{
    public class DebetCardUpdateValidations : FluentValidationService<DebetCardRequest>, IDebetCardUpdateValidation
    {
        public DebetCardUpdateValidations()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage("Номер не может быть пуст")
                .WithErrorCode("BRL-100.1");

            RuleFor(x => x.Holder)
                .NotEmpty()
                .WithMessage("Держатель не может быть пуст")
                .WithErrorCode("BRL-100.2");

            RuleFor(x => x.ExpireMonth)
                .NotEmpty()
                .WithMessage("Месяц не может быть пуст").WithErrorCode("BRL-100.31")
                .GreaterThan(0).WithMessage("Месяц не может быть меньше 0").WithErrorCode("BRL-100.32")
                .LessThan(12).WithMessage("Месяц не может быть больше 0").WithErrorCode("BRL-100.33");

            RuleFor(x => x.ExpireYear)
                .NotEmpty().WithMessage("Год не может быть пуст").WithErrorCode("BRL-100.41")
                .GreaterThan(2000).WithMessage("Год не может быть меньше 2000").WithErrorCode("BRL-100.42")
                .LessThan(2100).WithMessage("Год не может быть больше 2100").WithErrorCode("BRL-100.43");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id не должен быть пустой").WithErrorCode("BRL-100.51")
                .GreaterThan(0).WithMessage("Id не может быть меньше 0").WithErrorCode("BRL-100.52");
        }
    }
}
