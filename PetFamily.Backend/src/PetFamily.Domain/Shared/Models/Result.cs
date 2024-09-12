using OneOf;

namespace PetFamily.Domain.Shared.Models
{
    public class Result<T> : OneOfBase<T, Error>
    {
        public bool IsSuccess => IsT0;
        public bool IsFailure => IsT1;

        public new T Value => AsT0;
        public Error Error => AsT1;

        protected Result(T input) : base(input) { }
        protected Result(Error input) : base(input) { }


        public static implicit operator Result<T>(T t)
        {
            return new Result<T>(t);
        }

        public static implicit operator Result<T>(Error t)
        {
            return new Result<T>(t);
        }
    }
}
