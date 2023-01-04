using FluentValidation;
using FluentValidation.Results;

namespace NorthWind.UseCases.Common.Validators
{
	public static class Validator<Model>
	{
		public static Task<List<ValidationFailure>> Validate(Model model,
			IEnumerable<IValidator<Model>> validators, bool causesException = true)
		{
			var Failures = validators
				.Select(v => v.Validate(model))
				.SelectMany(r => r.Errors)
				.Where(f => f != null)
				.ToList();

			if (Failures.Any() && causesException)
			{
				throw new ValidationException(Failures);
			}
			return Task.FromResult(Failures);
		}
	}
}
