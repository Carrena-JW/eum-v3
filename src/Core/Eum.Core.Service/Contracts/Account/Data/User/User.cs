using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Service.Contracts.Account.Data.User
{
    [DataContract]
    public class User
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// 부서 코드
        /// </summary>
        [DataMember(Order = 2)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 상위 부서 코드
        /// </summary>
        [DataMember(Order = 3)]
        public string ParentDeptCode { get; set; }

        /// <summary>
        /// 부서명(한글)
        /// </summary>
        [DataMember(Order = 4)]
        public string DeptName { get; set; }

        /// <summary>
        /// 부서명(영문)
        /// </summary>
        [DataMember(Order = 5)]
        public string DeptNameEn { get; set; }

        /// <summary>
        /// 직원 코드
        /// </summary>
        [DataMember(Order = 6)]
        public string PersonCode { get; set; }

        [DataMember(Order = 7)]
        public string PersonId { get; set; }

        [DataMember(Order = 8)]
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// 회사 코드
        /// </summary>
        [DataMember(Order = 9)]
        public string ComCode { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        [DataMember(Order = 10)]
        public string FirstName { get; set; }

        /// <summary>
        /// 성
        /// </summary>
        [DataMember(Order = 11)]
        public string LastName { get; set; }

        /// <summary>
        /// 전체 이름
        /// </summary>
        [DataMember(Order = 12)]
        public string DisplayName { get; set; }
        /// <summary>
        /// 전체 이름 영문
        /// </summary>
        [DataMember(Order = 13)]
        public string DisplayNameEng { get; set; }

        /// <summary>
        /// 생일
        /// </summary>
        [DataMember(Order = 14)]
        public string BirthDate { get; set; }

        /// <summary>
        /// 입사일
        /// </summary>
        [DataMember(Order = 15)]
        public DateTime? EnterDate { get; set; }

        /// <summary>
        /// 핸드폰 번호
        /// </summary>
        [DataMember(Order = 16)]
        public string MobileTel { get; set; }

        /// <summary>
        /// 사무실 번호
        /// </summary>
        [DataMember(Order = 17)]
        public string OfficeTel { get; set; }
        /// <summary>
        /// 사무실 팩스번호
        /// </summary>
        [DataMember(Order = 18)]
        public string OfficeFax { get; set; }
        /// <summary>
        /// 직급 코드
        /// </summary>
        [DataMember(Order = 19)]
        public string TitleCode { get; set; }
        /// <summary>
        /// 직급
        /// </summary>
        [DataMember(Order = 20)]
        public string TitleName { get; set; }
        /// <summary>
        /// 직급 영문
        /// </summary>
        [DataMember(Order = 21)]
        public string TitleNameEn { get; set; }

        /// <summary>
        /// 직위 코드
        /// </summary>
        [DataMember(Order = 22)]
        public string GradeCode { get; set; }

        /// <summary>
        /// 직위
        /// </summary>
        [DataMember(Order = 23)]
        public string GradeName { get; set; }

        /// <summary>
        /// 직위 영문
        /// </summary>
        [DataMember(Order = 24)]
        public string GradeNameEn { get; set; }

        /// <summary>
        /// 직군 코드
        /// </summary>
        [DataMember(Order = 25)]
        public string JobGroupCode { get; set; }

        /// <summary>
        /// 사용자 메일 주소
        /// </summary>
        [DataMember(Order = 26)]
        public string Email { get; set; }

        /// <summary>
        /// 사용자 사진 URL
        /// </summary>
        [DataMember(Order = 27)]
        public string PhotoPath { get; set; }

        /// <summary>
        /// 사용자 집 주소
        /// </summary>
        [DataMember(Order = 28)]
        public string HomeAddress { get; set; }

        /// <summary>
        /// 사용자 집 우편번호
        /// </summary>
        [DataMember(Order = 29)]
        public string HomeZipCode { get; set; }

        [DataMember(Order = 30)]
        public string PositionCode { get; set; }

        [DataMember(Order = 31)]
        public string RoleCode { get; set; }

        [DataMember(Order = 32)]
        public string DisplayDept { get; set; }
        /// <summary>
        /// 검색시 부서/직원 구분
        /// </summary>
        [DataMember(Order = 33)]
        public string Type { get; set; }

        [DataMember(Order = 34)]
        public  string CreatedId { get; set; }
        [DataMember(Order = 35)]
        public  DateTime? CreatedDate { get; set; }
        [DataMember(Order = 36)]
        public  string UpdatedId { get; set; }
        [DataMember(Order = 37)]
        public  DateTime? UpdatedDate { get; set; }
        [DataMember(Order = 38)]
        public string OfficeZipCode { get; set; }
        /// <summary>
        /// 사무실 주소
        /// </summary>
        [DataMember(Order = 39)]
        public string OfficeAddress { get; set; }
        [DataMember(Order = 40)]
        public string DisplayOrder { get; set; }
        /// <summary>
        /// 부서 트리 경로
        /// </summary>
        [DataMember(Order = 41)]
        public string FolderPathName { get; set; }
        [DataMember(Order = 42)]
        public string HpOpen { get; set; }
        [DataMember(Order = 43)]
        public string IsMailUse { get; set; }
        [DataMember(Order = 44)]
        public string CHNG_DT { get; set; }
        [DataMember(Order = 45)]
        public string RoleName { get; set; }
        [DataMember(Order = 46)]
        public string JobRole { get; set; }
        [DataMember(Order = 47)]
        public string OfficeExtTel { get; set; }
        /// <summary>
        /// 휴대폰 번호 마스킹 처리 여부(번호를 ****로 표시)
        /// '0': 사용 안 함, '1': 사용
        /// </summary>
        [DataMember(Order = 48)]
        public string HpMasking { get; set; }
        [DataMember(Order = 49)]
        public string Manager { get; set; }
        /// <summary>
        /// 세부유형코드
        /// </summary>
        [DataMember(Order = 50)]
        public string ORMM_PTL_TPCD { get; set; }
        [DataMember(Order = 51)]
        public virtual bool IsPrimary { get; set; }
        [DataMember(Order = 52)]
        public virtual Guid O365Guid { get; set; }
        [DataMember(Order = 53)]
        public virtual string M365License { get; set; }
        [DataMember(Order = 54)]
        public string PositionName { get; set; }
        [DataMember(Order = 55)]
        public string PositionNameEn { get; set; }
        [DataMember(Order = 56)]
        public string DisplayDeptCode { get; set; }
        [DataMember(Order = 57)]
        public string DisplayDeptName { get; set; }
        [DataMember(Order = 58)]
        public string DisplayDeptNameEn { get; set; }
    }
}
