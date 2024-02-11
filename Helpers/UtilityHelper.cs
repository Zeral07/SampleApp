namespace SampleApp.Helpers
{
    public static class UtilityHelper
    {
        public static string GetExceptionMessage(Exception ex)
        {
            string msg = "";

            while (ex != null)
            {
                if (ex.Message != "")
                    msg = ex.Message;

                ex = ex.InnerException;
            }

            return msg;
        }
    }
}