# MiniPubSub-Unity
유니티 엔진과 모바일 플랫폼 간 통신을 위한 pubsub 방식의 라이브러리입니다.
직렬화된 데이터를 발행하여 사전 등록된 구독자들에게 전달합니다.

## 시작하기
1. Unity Package Manager을 엽니다.
2. 좌측 상단의 + 버튼을 눌러 Add package from git URL... 을 선택합니다.
3. `https://github.com/minisdk/MiniPubSub-Unity.git?path=Package/MiniPubSub` 을 추가(Add) 합니다.

## 주요 요소들

### Payload
데이터 저장 객체입니다.<br>
데이터를 json 시리얼라이즈 하여 저장합니다.

### Topic
발행 데이터의 타입입니다.<br>
어느 플랫폼(Game or Native) 의 어느 구독자에게 전달할지 결정합니다.

### MessageInfo
발행 데이터의 메타데이터입니다.<br>
해당 발행 데이터의 발행자, 목적지(`Topic`) 같은 데이터 전달에 필요한 정보를 담고 있습니다.

### Message
`Payload`와 `MessageInfo` 객체를 포함한 데이터 전달 객체입니다.<br>
- `Key` : 발행자와 구독자 사이에 약속한 발행 데이터 식별자입니다.<br>
발행자가 특정 키로 `Topic`을 생성해서 메세지를 발행하면, 사전에 해당 키를 구독한 구독자가 메세지를 받습니다.
- `Data<T>` : 역직렬화된 객체를 반환합니다.

### Messenger
`Message` 객체를 발행(`Publish`) 하거나 구독(`Subscribe`)합니다.<br>
또한 동기적으로 `Message` 객체를 보내고(`SendSync`) 처리(`Handle`) 하여 반환합니다.<br><br>
데이터는 크게 세가지 방법으로 주고 받을 수 있습니다.
- pub/sub
  - 기본적인 발행/구독 방식. 구독자가 `Subscribe` 하고 발행자가 `Publish` 하여 `Message`를 주고 받습니다. 
- pub/sub and reply
  - 구독자가 다시 발행자에게 `Message`를 비동기적으로 전달(`Reply`)합니다.
- sendSync/handle
  - 발행자가 동기적으로 `Message`를 전달(`SendSync`)하고 사전에 처리(`Handle`) 하기로 한 구독자가 그 결과(`Payload`) 를 반환합니다.
 
## 예제 코드
```csharp
class MyController
{
    private readlonly string KEY_PUB_HELLO = "SAMPLE::HELLO";
    private readlonly string KEY_SUB_WORLD = "SAMPLE::WORLD";
    private readlonly string KEY_PUB_HELLO_REPLY_MODE = "SAMPLE::HELLO_REPLY";
    private readlonly string KEY_SYNC_SEND = "SAMPLE::SYNCSEND";
    private Messenger messenger = Messenger();

    public MyController()
    {
        messenger.Subscribe(KEY_SUB_WORLD, OnWorld);
    }

    public void Hello()
    {
        messenger.Publish(
            new Topic(KEY_PUB_HELLO, SdkType::Native),
            new Payload(new MyHelloData())
        );
    }

    private void OnWorld(Message message)
    {
        MyWorldData myData = message.Data<MyWorldData>();
        // Do Something...
    }

    public void HelloReplyMode()
    {
        messenger.Publish(
            new Topic(KEY_PUB_HELLO, SdkType::Native),
            new Payload(new MyHelloData()),
            received => {
                MyWorldData data = received.Data<MyWorldData>();
                // Do Something...
            }
        );
    }

    public void HelloSync()
    {
        Payload result = messenger.SendSync(
            new Topic(KEY_SYNC_SEND, SdkType::Native),
            new Payload(new MySyncData())
        );
        MySyncResult myResult = result.Data<MySyncResult>();
        // Do Something....
    }
}
```
