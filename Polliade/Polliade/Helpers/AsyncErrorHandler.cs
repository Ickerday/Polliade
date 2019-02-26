using System;
using System.Diagnostics;

namespace Polliade.Helpers
{
    public static class AsyncErrorHandler
    {
        public static void HandleException(Exception exception) =>
            Debug.WriteLine(exception);
    }
}
