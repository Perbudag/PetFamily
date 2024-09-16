using OneOf;

namespace PetFamily.Domain.Shared.Models
{
    public class Result<T> : OneOfBase<T, List<Error>>
    {
        public bool IsSuccess => IsT0;
        public bool IsFailure => IsT1;

        public new T Value => AsT0;
        public List<Error> Errors => AsT1;

        protected Result(T input) : base(input) { }
        protected Result(List<Error> input) : base(input) { }


        public static implicit operator Result<T>(T input)
        {
            return new Result<T>(input);
        }

        public static implicit operator Result<T>(List<Error> input)
        {
            return new Result<T>(input);
        }
    }
}
