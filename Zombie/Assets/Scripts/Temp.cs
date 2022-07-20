public interface IFlyable
{
    // 정의할 메시지를 적자
    void Fly();
}

// 인터페이스에 대해 상속 받으면 반드시 상속받은 메시지에 대해 구현해야 함
// C#에서는 다중상속을 지원하지 않으나 인터페이스는 가능함
public class whale : IFlyable
{
    public void Fly()
    {
        // 난다요~ 고래~
    }
}