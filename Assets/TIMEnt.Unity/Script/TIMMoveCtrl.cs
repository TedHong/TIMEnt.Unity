using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TIMEnt.Unity
{
    public class TIMMoveCtrl : MonoBehaviour
    {
        float rotate_speed = 5f;
        bool isLookAt = false;
        bool lookSmooth = false;
        Transform lookTarget;
        Animator animator;
        private void Awake()
        {
            animator = this.GetComponent<Animator>();
        }

        private void Update()
        {
            if(isLookAt && lookTarget != null)
            {
                if(lookSmooth == false) transform.LookAt(lookTarget);
                else transform.rotation = Quaternion.RotateTowards(transform.rotation, lookTarget.rotation, rotate_speed * Time.deltaTime);
            }
        }
        public void SetMoveToPosition(TIMEventTask task)
        {
            StartCoroutine(MoveToPosition(task));
        }

        IEnumerator MoveToPosition(TIMEventTask task)
        {
            Transform moveTarget = this.transform;
            Vector3 goalPos = task.targetPosition;
            float speed = task.speed;

            Vector3 startPos = moveTarget.position;
            float step = (speed / (startPos - goalPos).magnitude) * Time.fixedDeltaTime;
            float t = 0;
            while (t <= 1.0f)
            {
                t += step;
                moveTarget.position = Vector3.Lerp(startPos, goalPos, t);
                yield return new WaitForFixedUpdate();
            }
        }

        public void SetLookAt(Transform t, bool ls = false)
        {
            lookTarget = t;
            isLookAt = true;
            lookSmooth = ls;
            if (animator != null) animator.enabled = false;
        }
    }
}