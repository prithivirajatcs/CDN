using System;

namespace EUJIT.Models
{
    public class ErrorLog : IDisposable
    {
        private string _errorDescription;
        public bool HasError { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorDescription
        {
            get
            {
                if (HasError)
                {
                    return _errorDescription;
                }
                else
                    return null;
            }

            set
            {
                _errorDescription = value;
                if (_errorDescription == null || _errorDescription.Length == 0)
                    HasError = false;
                else
                    HasError = true;
            }
        }

        public ErrorLog() { HasError = false; ErrorDescription = null; }

        public void Dispose()
        {

        }
    }
}
