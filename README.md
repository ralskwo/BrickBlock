# 🧩 Unity Puzzle Game – 전략 & SOLID 기반 퍼즐 게임

![Unity](https://img.shields.io/badge/Engine-Unity-FF5C5C?logo=unity)
![C#](https://img.shields.io/badge/Language-C%23-178600?logo=csharp)
![Design Patterns](https://img.shields.io/badge/Design%20Patterns-Strategy%2C%20Observer-blueviolet)
![SOLID](https://img.shields.io/badge/Principles-SOLID-green)

## 🖥️ 게임 스크린샷

![스크린샷1](image.png)
![스크린샷2](image-1.png)
![스크린샷3](image-2.png)

---

## 🎮 프로젝트 소개

Unity 6 기반으로 개발한 N x M 퍼즐 게임입니다.  
플레이어는 제한된 클릭 횟수 내에 같은 숫자의 블록을 제거하며 목표 점수 도달을 노립니다.  
**전략 패턴**과 **옵저버 패턴**, **SOLID 원칙**을 적용하여 구조적 설계와 유지보수성을 중점적으로 고려했습니다.

---

## 💻 개발환경 및 기술스택

-   **엔진**: Unity 6
-   **언어**: C#
-   **UI 시스템**: TextMeshPro
-   **설계 원칙**:
    -   SOLID
    -   KISS (Keep It Simple, Stupid)
    -   DRY (Don’t Repeat Yourself)
-   **디자인 패턴 적용**:
    -   Strategy Pattern
    -   Observer Pattern

---

## 🧩 게임 플레이 방법

1. N x M 퍼즐 보드가 자동 생성됩니다.
2. 블록에는 1~N의 숫자가 무작위로 배정됩니다.
3. 플레이어는 숫자 블록을 클릭해 **상하좌우 인접한 같은 숫자의 블록**을 제거합니다.
4. 제거된 블록 위의 블록들이 아래로 낙하하고, 새로운 블록이 생성됩니다.
5. 총 5번의 클릭 내에 **목표 점수**에 도달하면 성공, 그렇지 않으면 실패입니다.

---

## ⚙️ 주요 시스템 및 설계 특징

### ✅ 전략 패턴 적용 (Strategy Pattern)

-   `IScoreCalculator` / `ISuccessCondition` 인터페이스로 점수 정책 및 성공 조건을 분리
-   다양한 조건(콤보 기반, 시간 기반 등)으로 유연하게 확장 가능

### ✅ 옵저버 패턴 적용 (Observer Pattern)

-   `GameEvents`를 통해 점수, 클릭 수, 게임 종료 여부를 UIManager 등에서 구독 처리
-   게임 로직과 UI 간 의존성 제거 → 테스트 용이성 및 확장성 강화

### ✅ SOLID 원칙 준수

| 원칙 | 적용 사례                                                     |
| ---- | ------------------------------------------------------------- |
| SRP  | GameManager, ClickHandler, UIManager 등 단일 책임 분리        |
| OCP  | 점수/조건 정책이 인터페이스 기반으로 유연하게 확장            |
| LSP  | 구현체 교체 시 기능이 유지됨                                  |
| ISP  | 불필요한 의존성을 유발하지 않는 작은 인터페이스               |
| DIP  | UIManager는 ClickHandler를 의존성 주입으로 받음 (싱글턴 제거) |

---

## 🔭 향후 확장 방향

-   ✅ 콤보 기반 점수 계산 전략 추가
-   ✅ 특수 블록 (폭탄, 무작위 등) 구현
-   ✅ 상태 패턴 도입 (게임 대기, 낙하 처리, 결과 등)
-   ✅ 사운드 시스템과 애니메이션 이벤트 확장
-   ✅ JSON 기반의 스테이지 시스템 구현

---

---

## 📄 라이선스

본 프로젝트는 개인 포트폴리오 용도로 사용되며, 상업적 용도 사용은 허가되지 않습니다.
