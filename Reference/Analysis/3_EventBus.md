# Assets/Scripts/EventBus/EventBus.cs
### 이벤트 버스
> 이벤트 버스는 객체가 구독 또는 게시할 수 있는 이벤트들을 관리하는 중앙 허브 역할을 한다.    
> 파이프라인에 연결되어 있는 객체들 중 하나가 메세지를 보내면(게시자) 다른 객체들(구독자)이 받을 수 있는 패턴을 말한다.
- 게시자(Publisher) : 선언한 특정 종류의 이벤트를 이벤트 버스에서 구독자에게 게시할 수 있다.
- 이벤트 버스 (Event Bus) : 구독자와 게시자 사이의 이벤트 전송을 조정하는 역할을 한다.
- 구독자 (Subscriber) : 이벤트 버스를 통해 특정 이벤트의 구독자로 자신을 등록한다.
<br>

````csharp
using System;
using System.Collections.Generic;

public class EventBus
{
    static Dictionary<Type, EventHandler> bus = new Dictionary<Type, EventHandler>();
    static Dictionary<Type, EventArgs> args = new Dictionary<Type, EventArgs>();

    // 이벤트 구독
    public static void Subscribe(EventArgs arg, EventHandler func)
    {
        Type type = arg.GetType();
        if (bus.ContainsKey(type) == true)
        {
            bus[type] += func;
            return;
        }

        bus.Add(type, func);
        args.Add(type, arg);
    }

    // 이벤트 구독 취소
    public static void Unsbscribe(Type type, EventHandler func)
    {
        if (bus.ContainsKey(type) == false)
            return;

        bus[type] -= func;
        if (bus[type].GetInvocationList().Length == 0)
        {
            bus.Remove(type);
            args.Remove(type);
        }
    }

    // 이벤트 게시 (이벤트 함수 호출)
    public static void Publish(Type type)
    {
        bus[type](0, args[type]);
    }

    // 정의된 이벤트 데이터 호출 
    public static EventArgs Data(Type type)
    {
        return args[type];
    }
}
````
- bus, args
  - 빠른 데이터 검색을 위해 클래스로 정의한 이벤트의 타입을 키값으로 사용하였다.
  - bus : 이벤트를 발생시키는 함수 저장
  - args : 이벤트를 정의한 클래스를 저장
<br>


▼ 이벤트 정의
````csharp
using System;

public class Event_Test : EventArgs
{
    public string message;
    public Event_Test() { message = ""; }
}
````
- 클래스명 : 이벤트 이름
- 클래스 멤버변수 : 이벤트 함수에 전달할 데이타
<br>

▼ 이벤트 구독
````csharp
private void Start()
{
    // 이벤트 구독
    EventBus.Subscribe(new Event_Test(), Print);
}
// 이벤트로 등록할 함수
private void Print(object sender, EventArgs arg)
{
    // 해당 이벤트로 형변환해서 멤버변수 사용
    Debug.Log((arg as Event_Test).message);
}
````
<br>

▼ 이벤트 게시
````csharp
public void PrintMessage()
{
    Event_Test ev = (Event_Test)EventBus.Data(typeof(Event_Test));
    ev.Message = "Play Test";             // 이벤트 함수에 전달할 정보 입력
    EventBus.Publish(typeof(Event_Test)); // 해당 이벤트 함수 실행
}
````
