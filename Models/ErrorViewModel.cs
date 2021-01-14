using System;

namespace LojaVirtual.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool showRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
