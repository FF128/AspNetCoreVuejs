using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class CustomMessage
    {
        public string Message { get; set; }
        public bool HasError { get; set; } = false;
    }
    public static class CustomMessageHandler
    {
        public static CustomMessage Success(string message)
        {
            return new CustomMessage { Message = message };
        }

        public static CustomMessage Error(string message)
        {
            return new CustomMessage { Message = message, HasError = true };
        }
        public static CustomMessage RecordAdded()
        {
            return new CustomMessage { Message = "You have successfully added a record" };
        }
        public static CustomMessage RecordUpdated()
        {
            return new CustomMessage { Message = "You have successfully updated a record" };
        }
        public static CustomMessage RecordDeleted()
        {
            return new CustomMessage { Message = "You have successfully deleted a record" };
        }
        public static CustomMessage Custom(string message, bool hasError)
        {
            return new CustomMessage { Message = message, HasError = hasError };
        }
    }
}
