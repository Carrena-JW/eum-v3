using System.Runtime.Serialization;

namespace Eum.gRPC.Server.ServiceDesk.Shared.Domains
{
    public class User
    {
        /// <summary>
        /// 사용자 고유 식별자
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 사용자 형식, E: 필라넷 임/직원, C: 고객
        /// </summary>
        public char Type { get; set; }

        /// <summary>
        /// 회사 식별자
        /// </summary>
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// 회사 이름
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 직책 식별자
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 직책 이름
        /// </summary>
        public string TitleName { get; set; } = string.Empty;

        /// <summary>
        /// 부서 식별자
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 부서 이름
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 메일 주소 (로그인에 사용됨)
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 사용자 로그인 암호
        /// </summary>
        public string Password { get; set; } = string.Empty;

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
