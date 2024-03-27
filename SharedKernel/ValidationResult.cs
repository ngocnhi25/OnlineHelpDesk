namespace SharedKernel
{
    public sealed class ValidationResult : Result, IValidationResult
    {
        private ValidationResult(Error[] errors)
            : base(false, "Validation Error", IValidationResult.ValidationError) => ValidationsErrors = errors;

        public Error[] ValidationsErrors { get; }

        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }
}
