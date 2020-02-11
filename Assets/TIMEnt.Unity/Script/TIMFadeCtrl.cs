using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TIMEnt.Unity
{
    public class TIMFadeCtrl : MonoBehaviour
    {
        public Animator animator;
        void Start()
        {
        }

        public void SetFadeIn()
        {
            animator.SetBool("isFadeIn", true);
            animator.SetBool("isFadeOut", false);
        }

        public void SetFadeOut()
        {
            animator.SetBool("isFadeIn", false);
            animator.SetBool("isFadeOut", true);
        }
    }
}

