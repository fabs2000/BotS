using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        private Animator _animator;
        
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
        }
        
        #endregion
    }
}