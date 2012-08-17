using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldTheAllergen.Backend.Core.Attributes
{
    /// <summary>
    ///     Filter by IP address
    /// </summary>
    public class FilterIPAttribute : AuthorizeAttribute
    {
        public string[] AllowedIPs { get; set; }

        /// <summary>
        ///     Determines whether access to the core framework is authorized.
        /// </summary>
        /// <param name="httpContext"> The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request. </param>
        /// <returns> true if access is authorized; otherwise, false. </returns>
        /// <exception cref="T:System.ArgumentNullException">The
        ///     <paramref name="httpContext" />
        ///     parameter is null.</exception>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var userIpAddress = httpContext.Request.UserHostAddress;

            // Check that the IP is allowed to access
            var ipAllowed = AllowedIPs.Any(x => x == userIpAddress);
            return ipAllowed;
        }
    }
}