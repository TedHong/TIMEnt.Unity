## TIMEnt.Unity
This is a library for Unity that able to efficient creation of VR /AR experience content.

TIMEntertainment 에서 제작한 VR/AR 컨텐츠 제작용 라이브러리입니다.
시나리오에 맞게 Event Task 를 Dictionary 에 추가하고 
Coroutine 을 이용해 순차적으로 진행시킵니다.

AR 은 Vuforia, VR 은 Google VR 을 사용합니다.

TIMEnt.Unity구조도.pdf 파일을 보시면 구조를 파악하실 수 있습니다.

감사합니다.

## 적용방법
1. 프로젝트에 TIMEnt.Unity 패키지 임포트
2. 각 프로젝트에 알맞은 VR/AR SDK 설치
3. TIMGameManger 프리팹을 하이어라키에 등록
4. Menu - TIMEnt_Unity-Init Object 를 선택하여
TIMObjectManager 와 TIMSoundManager 를 생성
5. Unity Menu-TIMEnt_Unity-Set ProjetType VR//AR
: 프로젝트 타입 선택하여 해당 타입에 맞는 전처리 지시어 추가
6. TIMObjectManager 에 앞으로 컨트롤 할 게임오브젝트 등록
7. TIMSoundManager 에 사용 할 오디오 파일과 오디오소스 등록
8. 기획서의 각 장면에 맞게 TIMGameManager- InitData 에서 각 이벤트 별
TIMEventTask 객체를 생성해 Dictionary에 추가해 줌
9. 플레이하여 테스트

## 최종 업데이트
* 2020.02.11

## 개발자 정보
* 홍성욱 
* email : sungwooks@gmail.com
* blog : http://blog.tedhome.net
* linkedin : https://www.linkedin.com/in/greatted/
