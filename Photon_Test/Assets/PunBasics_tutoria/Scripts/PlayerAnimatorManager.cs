using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {
        private Animator _animator;


        #region Private Serialized

        [SerializeField] private float _directionDampTime = 0.25f;

        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {
            _animator = GetComponent<Animator>();
            if (!_animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        void Update()
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
            
            if (!_animator) return;

            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Base Layer.Run"))
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    _animator.SetTrigger("Jump");
                }
            }
            
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (v < 0) v = 0;

            _animator.SetFloat("Speed", h * h + v * v);
            _animator.SetFloat("Direction", h, _directionDampTime, Time.deltaTime);
        }

        #endregion
    }
}