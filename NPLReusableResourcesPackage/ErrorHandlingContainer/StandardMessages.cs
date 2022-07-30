using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLReusableResourcesPackage.ErrorHandlingContainer
{
    public class StandardMessages
    {
        public static OutputHandler getExceptionMessage(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return new OutputHandler { IsErrorOccured = true, Message = $"Error Occured:{ex.Message}" };
            }
            else
            {
                return new OutputHandler { IsErrorOccured = true, Message = $"Error Occured:{ex.InnerException}" };

            }
        }

        public static string GetSuccessfulMessage()
        {
            string message = $"The Proccess Completed Successfully";
            return message;
        }

        public static string GetGeneralErrorMessage()
        {
            string message = $"Something went wrong, please contact IT on 01234567";
            return message;
        }

        public static string GetDuplicateMessage(string regionName)
        {
            string message = $"{regionName} already exist in the database";
            return message;
        }
    }
}
