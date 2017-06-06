using System.Collections.Generic;
using System.Text;

namespace SharpPusher.Services
{
    public class Response<T> : ErrorCollection
    {
        public T Result { get; set; }
    }

    public class ErrorCollection
    {
        public bool HasErrors => ErrorList.Count > 0;

        private readonly List<string> ErrorList = new List<string>();

        /// <summary>
        /// Adds one error to the list of errors
        /// </summary>
        /// <param name="errorMessage">Error string to add.</param>
        internal void AddError(string errorMessage)
        {
            ErrorList.Add(errorMessage);
        }

        /// <summary>
        /// Adds multiple errors to the list of errors
        /// </summary>
        /// <param name="multiError">List of errors to add.</param>
        internal void AddError(IEnumerable<string> multiError)
        {
            ErrorList.AddRange(multiError);
        }

        /// <summary>
        /// Clears all errors
        /// </summary>
        internal void ClearErrors()
        {
            ErrorList.Clear();
        }

        /// <summary>
        /// Returns a formated string of all the errors.
        /// </summary>
        /// <returns>Formatted string of all the errors</returns>
        public string GetErrors()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in ErrorList)
            {
                sb.AppendLine("- " + item);
            }
            return sb.ToString();
        }
    }
}
