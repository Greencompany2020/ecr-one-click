using EcrOneClick.UseCases.Request;
using FluentValidation;

namespace EcrOneClick.Presentation.ViewModels.Validators;

public class SaveConfigurationValuesRequestValidator : AbstractValidator<SaveConfigurationValuesRequest>
{
    public SaveConfigurationValuesRequestValidator()
    {
        RuleFor(config => config.Store)
            .NotEmpty()
            .WithMessage("Tienda no puede estar vacio")
            .Matches("[A-Z]{2}-[A-Z]+[0-9]?[1-9]")
            .WithMessage("Tienda no tiene el formato correcto. Ej. TA-M01, TA-ACA1");
        
        RuleFor(config => config.CashRegister)
            .NotEmpty()
            .WithMessage("Caja no puede estar vacio")
            .Matches("[0-9][1-9]")
            .WithMessage("Caja de ser numerico. Ej. 01, 02, etc.");

        RuleFor(config => config.DockerUser)
            .NotEmpty()
            .WithMessage("{PropertyName} no puede estar vacio");

        RuleFor(config => config.DockerPass)
            .NotEmpty()
            .WithMessage("{PropertyName} no puede estar vacio");

        RuleFor(config => config.DopplerToken)
            .NotEmpty()
            .WithMessage("{PropertyName} no puede estar vacio");
    }
}