using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float MoveSpeed = 5f; // 앞뒤 움직임의 속도
    public float RotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput _input; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody _rigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator _animator; // 플레이어 캐릭터의 애니메이터

    private static class AnimID
    {
        public static readonly int MOVE = Animator.StringToHash("Move");
    }

    private void Awake()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    // 일관된 물리연산의 결과를 얻을 수 있도록 조정
    private void FixedUpdate() 
    {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        //Time.fixedDeltaTime
        move();
        rotate();

        _animator.SetFloat(PalyerAnimID.Move, _input.MoveDirection);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void move() 
    {
        // 배치 순서에 따라 연산 시간이 달라짐(연산순서 유의)
        // 거리 = 속력 * 시간
        //float movementAmount = MoveSpeed * Time.fixedDeltaTime;
        //방향은 캐릭터 기준
        //Vector3 direction = _input.MoveDirection * transform.forward;
        //Vector3 offset = direction * movementAmount;

        //_rigidbody.MovePosition(_rigidbody.position + offset);
        
        //transform.forward * MoveSpeed * Time.fixedDeltaTime * _input.MoveDirection; // Vector3 관련 연산이 3번이나 있음
        Vector3 deltaPosition = MoveSpeed * Time.fixedDeltaTime * _input.MoveDirection * transform.forward; // Vector3 관련 연산 1번

        _rigidbody.MovePosition(_rigidbody.position + deltaPosition);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void rotate() 
    {
        float rotationAmount = _input.RotateDirection * RotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }
}