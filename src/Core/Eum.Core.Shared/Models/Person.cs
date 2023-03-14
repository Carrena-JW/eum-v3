using System.Runtime.Serialization;

namespace Eum.Core.Shared.Models
{
    [DataContract]
    public class Person
    {
        [DataMember(Order = 1)]
        public string PersonCode { get; set; }
        [DataMember(Order = 2)]
        public string Email { get; set; }
        [DataMember(Order = 3)]
        public string PersonId { get; set; }
        [DataMember(Order = 4)]
        public string DisplayName { get; set; }
        [DataMember(Order = 5)]
        public string EmployeeNumber { get; set; }
        [DataMember(Order = 6)]
        public string ComCode { get; set; }
        [DataMember(Order = 7)]
        public string DeptCode { get; set; }
        [DataMember(Order = 8)]
        public string DeptName { get; set; }
        [DataMember(Order = 9)]
        public string TitleCode { get; set; }
        [DataMember(Order = 10)]
        public string TitleName { get; set; }
        [DataMember(Order = 11)]
        public string Password { get; set; }
    }
}
