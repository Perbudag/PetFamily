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


        public static implicit operator Result<T>(T input) =>
            new Result<T>(input);

        public static implicit operator Result<T>(List<Error> input) =>
            new Result<T>(input);

        public static implicit operator Result<T>(Error input) =>
            new Result<T>([input]);
    }

    public class Result : OneOfBase<object, List<Error>>
    {
        public bool IsSuccess => IsT0;
        public bool IsFailure => IsT1;

        public new object Value => AsT0;
        public List<Error> Errors => AsT1;

        protected Result() : base(true) { }
        protected Result(List<Error> input) : base(input) { }


        public static Result Success() =>
            new Result();

        public static Result Failure(List<Error> input) =>
            new Result(input);

        public static Result Failure(Error input) =>
            new Result([input]);


        public static implicit operator Result(List<Error> input) =>
            new Result(input);

        public static implicit operator Result(Error input) =>
            new Result([input]);
    }
}
