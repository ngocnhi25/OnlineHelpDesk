namespace SharedKernel
{
    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        internal ValidationResult(Error[] error)
            : base(default, false, "Error Validation", IValidationResult.ValidationError)
            => ValidationsErrors = error;

        public Error[] ValidationsErrors { get; }

        public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    }
}
