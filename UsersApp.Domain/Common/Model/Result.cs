

using UsersApp.Domain.Common.Validation;

namespace UsersApp.Domain.Common.Model
{
    public class Result<Tvalue>
    {
        public Tvalue Value { get;private set; }
        public ValidationResult ValidationResult { get; private set; }
        public Result(Tvalue value, ValidationResult validationResult)
        {
            Value = value;
            ValidationResult = validationResult;
        }
    }
}
