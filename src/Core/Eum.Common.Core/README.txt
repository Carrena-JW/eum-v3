

Eum.Common.Core

- 공통 영역 (인증, 솔루션 설정, 솔루션 로깅, 솔루션 파일, 솔루션 조직정보 등등 )
- Core 에 경우에는 비지니스 로직을 담당합니다.
- 이전에 사용했떤 Services 들에 대한 집합





기본적인 플로우는 아래와 같음


[수정/삭제/조회]
gRPC (Host에 대한 endpoint) -> Service (Core) -> Repositories (Infrastructure)

[조회]
gRPC (Hosts에 대한 endpoint) -> Query (Infrastructure)