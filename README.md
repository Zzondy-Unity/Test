# Week6
<details>
  <summary>Q1</summary>
    <div markdown="1">
      <ul>
<li>1.OX 퀴즈</li>
        <ul>
        <li>앵커와 피벗은 같은 기능을 한다. x</li>
        <li>피벗을 왼쪽 상단으로 설정하면, UI 요소는 화면의 왼쪽 상단을 기준으로 위치가 고정된다. O</li>
        <li>피벗을 UI 요소의 중심에 설정하면, 회전 시 UI 요소가 중심을 기준으로 회전한다. O</li>
        </ul>
  <li>2. 게임의 상단바와 같이 화면에 특정 영역에 꽉 차게 구성되는 UI와 화면의 특정 영역에 특정한 크기로 등장하는 UI의 앵커 구성이 어떻게 다른 지 설명해보세요.</li>
        <ul>
     <li>특정구역 꽉차게 구성하는 UI : 앵커를 좌우에 두어서 채운다. 화면이 커지거나 작아져도 해당 UI는 좌우로 찢어진 앵커에 맞춰 좌우로 길게 구성된다.</li>
     <li>특정 영역에 특정한 크기 : 특정 구역에 피벗을 위치시킨다. 예를들어 우하단에 딱 붙어있는 UI의 경우 우하단에 앵커를 두면 화면크기가 달라져도 해당 크기를 유지한다. 만약 비율을 유지시키고 싶으면 앵커를 해당하는 위치에 찢어두고 Left,Top,Right,Bottom을 각각 1로 지정하면 앵커에 맞게 커진다. 이에 맞추면 화면이 달라져도 전체 캔버스 비율에 맞춰서 크기가 고정된다.</li>
        </ul>
  <li>3. 돌아다니는 몬스터의 HP 바와 늘 고정되어있는 플레이어의 HP바는 Canvas 컴포넌트의 어떤 설정이 달라질 지 생각해보세요.</li>
        <ul>
     <li>돌아다니는 몬스터의 HP바의 경우 캠버스를 월드포지션으로 지정하여 월드 맵 자체에서 움직일 수 있게 한다. 
고정되어있는 HP바는 Screen Space로 렌더모드를 지정하여 화면상에 존재하게 한다.</li>
     <li>이때 Canvas Scaler의 ScaleMode를 ScaleWithScreenSize로 지정하여 화면 비율에 맞춰 같이 변할 수 있게 한다.
</li>
        </ul>
<li>4. 게임이 길어지니 힘이 듭니다. 게임을 일시정지하는 버튼을 만들어봅시다.</li>
        <ul>
  <li>일시정지버튼, 계속버튼, 그리고 일시정지시 화면에 불투명한 검은 판을 설치하여 화면이 정지되었음을 알려준다.</li>
  <li>Pause스크립트를 작성.</li>
          <ul>
            <li>버튼과 판넬을 키고끄면서 타임스케일 값을 조절하여 일시정지시킨다.</li>
            <li>UI매니저를 만들어 Pause를 넣고 이벤트를 만든다.</li>
            <li>플레이어의 Input을 담당하는 InputController스크립트에서 canMove, canLook 변수를 작성하고 각 이벤트에 해당하는 함수를 이벤트에 등록시킨다.</li>
          </ul>
        </ul>
        </ul>
    </div>
</details>

<details>
  <summary>Q2</summary>
  <div markdown="1">
    <ul>
      <li>1. OX 퀴즈</li>
      <ul>
        <li>코루틴은 비동기 작업을 처리하기 위해 사용된다. O</li>
        <li>yield return new WaitForSeconds(1);는 코루틴을 1초 동안 대기시킨다. O</li>
        <li>코루틴은 void를 반환하는 메소드의 형태로 구현된다. X</li>
        <ul>
          <li>코루틴은 IEnumerator를 반환하는 메서드이다.</li>
        </ul>
      </ul>
      <li>2. 코루틴을 이미 실행 중이라면 추가로 실행하지 않으려면 어떻게 처리해주면 될까요?</li>
      <ul>
        <li>
          ```csharp
          if(ForceCoroutine != null)
          {
              StopCoroutine(ForceCoroutine);
          }
          ForceCoroutine = StartCoroutine(ChangeSpeed(percentage, duration, up));
          ```
        </li>
        <li>위와 같이 Coroutine 변수를 지정하여 추가 실행 있는지 여부를 항상 체크합니다.</li>
      </ul>
      <li>3. 코루틴 실행 중 게임오브젝트가 파괴되더라도 코루틴의 실행이 정상적으로 지속될까요?</li>
      <ul>
        <li>게임 오브젝트가 파괴되면 코루틴은 정지합니다. 코루틴은 해당 코루틴을 시작한 게임 오브젝트의 생명주기에 종속되기 때문입니다.</li>
      </ul>
      <li>4. 웨이브 10, 30, 50, …에 부여되는 랜덤 디버프를 만들어봅시다.</li>
      <ul>
        <li>GameManager에 ProcessWaveConditions함수에 다음과 같은 조건을 추가한다.</li>
        <ul>
          <li>((currentWaveIndex % 10) % 2).Equals(1)</li>
          <li>10, 30, 50라운드때에 발생하는 이벤트이다.</li>
        </ul>
        <li>랜덤 디버프는 현재 체력의 0 ~ 50%에 해당하는 데미지를 입는 것으로 처리하였다.</li>
      </ul>
    </ul>
  </div>
</details>

<details>
  <summary>Q3</summary>
  <div markdown="1">
    <ul>
      <li>1. OX 퀴즈</li>
        <ul>
          <li>추상 클래스는 new를 통해 인스턴스화(instantiation)할 수 없다. O</li>
          <li>추상 클래스는 다른 클래스처럼 일반 메서드와 속성을 포함할 수 있다. O</li>
          <li>추상 클래스를 상속받은 클래스는 추상 클래스의 모든 추상 메서드를 구현해야 한다. O</li>
          <li>C#에서 한 클래스는 여러 개의 추상 클래스를 상속받을 수 있다. X</li>
        </ul>
      <li>2. 추상 클래스를 사용하지 않고 동일한 기능을 구현하려면 어떤 문제가 발생할 수 있는지 설명해보세요.</li>
        <ul>
          <li>한 객체당 하나의 클래스를 가지고있다.</li>
          <li>어떤 비슷한 기능을 하는 클래스를 사용하고싶으면 Switch case문이나 if문을 통해 어떤 클래스인지 확인후 사용해야한다.</li>
          <li>객체지향 개방-폐쇄 원칙에 어긋난다. 새로운 클래스가 생기면 Swich case문이나 if문이 늘어나기 때문이다.</li>
        </ul>
      <li>3. 코드리뷰 결과에 따라 코드를 개선해봅시다.</li>
        <ul>
          <li>Awake 메소드 내의 초기화 코드를 분리하는 것이 더 깔끔해보일 것 같습니다.</li>
            <ul>
              <li> </li>
            </ul>
          <li>ApplyStatModifiers 메소드 내의 switch식의 코드를 분리하면 가독성이 높아질 것 같습니다.</li>
            <ul>
              <li>  </li>
            </ul>
        </ul>
    </ul>
  </div>
</details>

