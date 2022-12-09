using FluentValidation;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class
    GetRandomValueForRandomMinMaxValueQueryValidator : AbstractValidator<GetRandomValueForRandomMinMaxValueQuery>
{
    public GetRandomValueForRandomMinMaxValueQueryValidator()
    {
        RuleFor(x => x.MinValue)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("MinValue alanı zorunludur")
            .GreaterThan(0)
            .WithMessage("MinValue değeri 0 dan büyük olmalıdır");

        RuleFor(x => x.MaxValue)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("MaxValue alanı zorunludur")
            .GreaterThan(0)
            .WithMessage("MaxValue değeri 0 dan büyük olmalıdır");
    }
}