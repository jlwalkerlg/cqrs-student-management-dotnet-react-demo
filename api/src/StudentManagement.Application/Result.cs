using System;

namespace StudentManagement.Application
{
    public abstract class AbstractResult
    {
        protected AbstractResult()
        {
        }

        protected AbstractResult(Error error)
        {
            if (error == null)
                throw new InvalidOperationException();

            Error = error;
        }

        public bool IsSuccess => Error == null;
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
    }

    public class Result : AbstractResult
    {
        protected Result() : base()
        {
        }

        protected Result(Error error) : base(error)
        {
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result Fail(Error error)
        {
            return new Result(error);
        }

        public static Result<T> Ok<T>()
        {
            return Result<T>.Ok();
        }

        public static Result<T> Ok<T>(T value)
        {
            return Result<T>.Ok(value);
        }

        public static Result<T> Fail<T>(Error error)
        {
            return Result<T>.Fail(error);
        }
    }

    public class Result<T> : AbstractResult
    {
        protected Result() : base()
        {
        }

        protected Result(Error error) : base(error)
        {
        }

        protected Result(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public static Result<T> Ok()
        {
            return new Result<T>();
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Fail(Error error)
        {
            return new Result<T>(error);
        }

        public static implicit operator Result<T>(Result r) => r.IsSuccess ? Ok() : Fail(r.Error);
        public static implicit operator Result(Result<T> r) => r.IsSuccess ? Result.Ok() : Result.Fail(r.Error);
    }
}
