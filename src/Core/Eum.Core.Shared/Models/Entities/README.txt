

Eum.Core.Shared

- 공통 영역 (인증, 솔루션 설정, 솔루션 로깅, 솔루션 파일, 솔루션 조직정보 등등 )
- 해당 프로젝트에서는 모듈에서 사용하는 Nuget Packages 가 설치됨
- Interface 와 Models 이 작성되며, 실제 로직은 절대 포함하지 않는다.


[Models]
############ 각 모델은 절대 상속하지 않는다. ###############
############ 각 구분된 용도로만 사용함, 속성이 동일하다고 용도 구분없이 사용하지 않는다 ################

- Entities : 데이터베이스 테이블 단위 모델
- CommandRequests : 추가/수정/삭제 요청 파라메터/페이로드 모델
- CommandResponses : 추가/수정/삭제 에 대한 결과 반환 모델
- QueryRequests : 조회 요청 파라메터 모델
- QueryViewModels : 조회 결과 반환 모델


[Interfaces]
- 각 Queries interface , Repository interface, Service Interface 는 아래 구현