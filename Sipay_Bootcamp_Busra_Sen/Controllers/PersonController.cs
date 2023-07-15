using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sipay_Bootcamp_Busra_Sen.Models;


namespace Sipay_Bootcamp_Busra_Sen.Controllers;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {

        RuleFor(person => person.Name).NotNull().WithMessage("Staff person name").MinimumLength(5).MaximumLength(100);
        RuleFor(person => person.Lastname).NotNull().WithMessage("Staff person lastname").MinimumLength(5).MaximumLength(100);
        RuleFor(person => person.Phone).NotNull().WithMessage("Staff person phone number");
        RuleFor(person => person.AccessLevel).NotNull().WithMessage("Staff person access level to system").InclusiveBetween(1, 5);
        RuleFor(person => person.Salary).NotNull().WithMessage("Staff person name").InclusiveBetween(5000, 50000);
    }
}

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    public PersonController() { }

    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        PersonValidator validator = new PersonValidator();
        var result = validator.Validate(person);

        if (!result.IsValid)
        {
            var error = result.Errors.Select(e => e.ErrorMessage);
            return BadRequest(error);
        }
        return Ok(person);

    }
}
