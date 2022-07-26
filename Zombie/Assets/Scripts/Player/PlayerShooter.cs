﻿using UnityEngine;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter : MonoBehaviour {
    public Transform GunPivot; // 총 배치의 기준점
    public Transform LeftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform RightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    private Gun _gun; // 사용할 총
    private PlayerInput _input; // 플레이어의 입력
    private Animator _animator; // 애니메이터 컴포넌트

    private static class AnimID
    {
        public static readonly int Reload = Animator.StringToHash("Reload");
    }

    private void Awake()
    {
        _gun = GetComponentInChildren<Gun>();
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        // 슈터가 활성화될 때 총도 함께 활성화
        _gun.gameObject.SetActive(true);
    }
    
    private void OnDisable() {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        _gun.gameObject.SetActive(false);
    }

    private void Update() {
        // 입력을 감지하고 총 발사하거나 재장전
        if (_input.CanFire)
        {
            _gun.Fire();
        }

        if(_input.CanReload)
        {
            if (_gun.TryReload())
            {
                _animator.SetTrigger(PalyerAnimID.Reload);
            }
        }
    }

    // 탄약 UI 갱신
    private void UpdateUI() {
        if (_gun != null && UIManager.instance != null)
        {
            // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
            //UIManager.instance.UpdateAmmoText(_gun.magAmmo, _gun.ammoRemain);
        }
    }

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex) {
        GunPivot.position = _animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);    // 적용할 목표, 가중치 비율
        // _animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandMount.position);       // 실제 위치값 지정

        // _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        // _animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandMount.rotation);

        // //오른손
        // _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        // _animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandMount.position);

        // _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        // _animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandMount.rotation);


        // _animator.SetIKPositionAndWeight(AvatarIKGoal.LeftHand, LeftHandMount.position);
        // _animator.SetIKPositionAndWeight(AvatarIKGoal.RightHand, RightHandMount.position);

        // _animator.SetIKRotationAndWeight(AvatarIKGoal.LeftHand, LeftHandMount.rotation);
        // _animator.SetIKRotationAndWeight(AvatarIKGoal.RightHand, RightHandMount.rotation);


        _animator.SetIKTransformAndWeight(AvatarIKGoal.LeftHand, LeftHandMount);
        _animator.SetIKTransformAndWeight(AvatarIKGoal.RightHand, RightHandMount);

    }
}