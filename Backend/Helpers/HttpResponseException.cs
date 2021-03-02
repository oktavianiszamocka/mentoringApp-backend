using System;

namespace MentorApp.Helpers
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;
        public string Value { get; set; }

        public HttpResponseException(string Value)
        {
            this.Value = Value;

        }
    }
}
