using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace Eum.gRPC.Server.ServiceDesk.Modules.CaseModule.Domains
{
    public class Case
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 삭제 여부
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 종료일시
        /// </summary>
        public DateTime FinishDate { get; set; }

        /// <summary>
        /// 접수일시
        /// </summary>
        public DateTime WhenReceipted { get; set; }

        /// <summary>
        /// 제품 아이디
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 계약 아이디
        /// </summary>
        public Guid ContractId { get; set; }

        /// <summary>
        /// 상태: 접수, 검토, 실행, 완료
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 생성일시
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public string CreatedId { get; set; }

        /// <summary>
        /// 수정일시
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// 수정자
        /// </summary>
        public string UpdatedId { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 중요도
        /// </summary>
        public string Importance { get; set; }

        /// <summary>
        /// 증상
        /// </summary>
        public string Behavior { get; set; }

        /// <summary>
        /// 재연 절차
        /// </summary>
        public string Reproduce { get; set; }

        /// <summary>
        /// 기대하는 동작
        /// </summary>
        public string Expected { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Additional { get; set; }
    }
}
