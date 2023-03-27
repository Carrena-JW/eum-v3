using System.Runtime.Serialization;

namespace Eum.gRPC.Server.ServiceDesk.Modules.ClientModule.Domains
{
    public class Client
    {
        /// <summary>
        /// 사용자 식별자
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 메일 주소 (로그인에 사용됨)
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 회사 식별자
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 직책
        /// </summary>
        public string TitleName { get; set; } = string.Empty;

        /// <summary>
        /// 부서
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 로그인 가능 여부
        /// </summary>
        public bool CanLogin { get; set; } = false;

        /// <summary>
        /// 연락처
        /// </summary>
        public string Contact { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public string CreatedId { get; set; } = string.Empty;

        public DateTime UpdatedDate { get; set; }

        public string UpdatedId { get; set; } = string.Empty;
    }
}
