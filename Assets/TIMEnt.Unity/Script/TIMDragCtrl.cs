using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TIMEnt.Unity
{
    /// <summary>
    /// World 에 있는 오브젝트를 드래그하는 컴포넌트
    /// Collider 필요
    /// </summary>
    public class TIMDragCtrl : MonoBehaviour
    {
        private void Start()
        {
            if (this.GetComponent<Collider>() == null)
            {
                TIMLog.LogError("OnMouseDrag is must need Collider.");
            }
        }
        private Vector3 screenPoint;
        private Vector3 offset;

        void OnMouseDown()
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        void OnMouseDrag()
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            transform.position = cursorPosition;
        }

    }
}

