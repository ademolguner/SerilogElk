using FluentValidation;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class GetRandomValueForRandomMaxValueQueryValidator : AbstractValidator<GetRandomValueForRandomMaxValueQuery>
{
    public GetRandomValueForRandomMaxValueQueryValidator()
    {
        RuleFor(x => x.MaxValue)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("MaxValue alanı zorunludur");
    }
}