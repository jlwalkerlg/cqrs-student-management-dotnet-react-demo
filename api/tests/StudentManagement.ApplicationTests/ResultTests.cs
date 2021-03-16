using System;
using StudentManagement.Application;
using Xunit;

namespace StudentManagement.ApplicationTests
{
    public class ResultTests
    {
        [Fact]
        public void Ok_Result()
        {
            var result = Result.Ok();

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Fail_Result()
        {
            var error = new DummyError();
            var result = Result.Fail(error);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Same(error, result.Error);
            Assert.Equal(error.Value, ((DummyError)result.Error).Value);
        }

        [Fact]
        public void Fail_Result_Error_Cant_Be_Empty()
        {
            Assert.Throws<InvalidOperationException>(() => Result.Fail(null));
        }

        [Fact]
        public void Generic_Ok_Result_With_Value()
        {
            var value = new DummyValue();
            var result = Result.Ok<DummyValue>(value);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Null(result.Error);
            Assert.Same(result.Value, value);
        }

        [Fact]
        public void Generic_Ok_Result_Without_Value()
        {
            var result = Result.Ok<DummyValue>();

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Null(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Generic_Ok_Result_Can_Have_Null_Value()
        {
            var result = Result.Ok<DummyValue>(null);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Null(result.Value);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Generic_Fail_Result()
        {
            var error = new DummyError();
            var result = Result.Fail<DummyValue>(error);

            Assert.IsType<Result<DummyValue>>(result);
            Assert.Null(result.Value);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.Null(result.Value);
            Assert.Same(error, result.Error);
            Assert.Equal(error.Value, ((DummyError)result.Error).Value);
        }

        [Fact]
        public void Can_Cast_Generic_Result_To_Non_Generic_Result()
        {
            var generic = Result.Ok<object>();
            var nonGeneric = (Result)generic;
        }

        private class DummyError : Error
        {
            public DummyError() : base("Duh!")
            {
            }

            public int Value => 1;
        }

        private class DummyValue
        {
        }
    }
}
