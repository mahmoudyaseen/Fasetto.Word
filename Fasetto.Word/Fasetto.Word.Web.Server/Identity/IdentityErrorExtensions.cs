using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fasetto.Word.Web.Server
{
    /// <summary>
    /// Extensions for <see cref="IdentityError"/> class
    /// </summary>
    public static class IdentityErrorExtensions
    {
        /// <summary>
        /// Combines all error into single string 
        /// </summary>
        /// <param name="errors">The errors to aggregate</param>
        /// <returns>Returns single string with each error separated by new line</returns>
        public static string AggregateErrors(this IEnumerable<IdentityError> errors)
        {
            // Get all errors into a list
            return errors?.ToList()
                          // Grab their description
                          .Select(f => f.Description)
                          // And combine them with a newline seprator
                          .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
        }
    }
}
