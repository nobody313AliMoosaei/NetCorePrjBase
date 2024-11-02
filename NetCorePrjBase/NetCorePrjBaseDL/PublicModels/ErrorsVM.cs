using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.DL.PublicModels
{
    public class ErrorsVM
    {
        public bool IsValid { get; set; } = false;
        public Guid? GUIDKey { get; set; } = Guid.NewGuid();
        public string? Title { get; set; } = "توجه";
        public long? Key { get; set; }
        public string? Message { get; set; }
        public List<string> LstErrors { get; set; } = new List<string>();

        public ErrorsVM()
        {

        }
        public ErrorsVM(string Message)
        {
            this.Message = Message;
        }
        public ErrorsVM(string message, bool isValid)
        {
            this.Message = message;
            this.IsValid = isValid;
        }


    }
}
