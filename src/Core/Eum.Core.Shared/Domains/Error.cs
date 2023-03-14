using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Shared.Domains
{
    public class Error
    {
        public long Id { get; set; }
        public string MachineName { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string CreatedId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
