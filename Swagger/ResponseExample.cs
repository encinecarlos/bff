using POC.Shared.Responses;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Swagger
{
    /// <summary>
    ///     Response success example class.
    /// </summary>
    /// <seealso cref="IExamplesProvider" />
    public class ResponseSuccessExample : IExamplesProvider
    {
        /// <summary>
        ///     Return values from success request.
        /// </summary>
        /// <returns>Model</returns>
        public object GetExamples()
        {
            return new Response(new
            { Id = Guid.NewGuid(), Description = new Dictionary<string, string> { { "en-US", "Same Description" } } });
        }
    }

    /// <summary>
    ///     Response not found example class.
    /// </summary>
    /// <seealso cref="IExamplesProvider" />
    public class ResponseFail404Example : IExamplesProvider
    {
        /// <summary>
        ///     Return values from fail request.
        /// </summary>
        /// <returns>Notifications</returns>
        public object GetExamples()
        {
            return new Response(new { Id = Guid.NewGuid() }, false, "Entity not found!");
        }
    }

    /// <summary>
    ///     Response bad request example class.
    /// </summary>
    /// <seealso cref="IExamplesProvider" />
    public class ResponseFail400Example : IExamplesProvider
    {
        /// <summary>
        ///     Return values from fail request.
        /// </summary>
        /// <returns>Notifications</returns>
        public object GetExamples()
        {
            return new Response(new { Id = Guid.NewGuid() }, false,
                new NotificationResponse("Id", "'Id' must not be empty."));
        }
    }
}