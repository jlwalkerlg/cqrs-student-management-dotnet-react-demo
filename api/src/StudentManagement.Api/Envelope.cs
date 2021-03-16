using System.Collections.Generic;

namespace StudentManagement.Api
{
    public static class Envelope
    {
        public static ErrorEnvelope Error(string message)
        {
            return new ErrorEnvelope(message);
        }

        public static InvalidEnvelope Invalid(Dictionary<string, List<string>> errors)
        {
            return new InvalidEnvelope(errors);
        }

        public static SuccessEnvelope Ok(object data)
        {
            return new SuccessEnvelope(data);
        }

        public class ErrorEnvelope
        {
            public ErrorEnvelope(string message)
            {
                Error = message;
            }

            public string Error { get; }
        }

        public class InvalidEnvelope : ErrorEnvelope
        {
            public Dictionary<string, List<string>> Errors { get; }

            public InvalidEnvelope(Dictionary<string, List<string>> errors) : base("The request was invalid.")
            {
                Errors = errors;
            }
        }

        public class SuccessEnvelope
        {
            public object Data { get; }

            public SuccessEnvelope(object data)
            {
                Data = data;
            }
        }
    }
}
