using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TIMEnt.Unity
{
   [RequireComponent(typeof(Button))]
    public class TIMNaviArrowCtrl : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Sprite sprite_normal;
        [SerializeField] Sprite sprite_select;
        [SerializeField] Text title;
        [SerializeField] Text des;
        [SerializeField] GameObject contentBox;
        [SerializeField] Animator animator;
        [SerializeField] Button button;
        public STEP_TYPE mStep;
        private TIMNaviArrowData mData;
        private ARROW_STATUS mStatus;
        public ARROW_STATUS status
        {
            set 
            {
                mStatus = value;
                if (animator) animator.ResetTrigger("setBlink");
                if (animator) animator.ResetTrigger("setIdle");
                switch (mStatus)
                {
                    case ARROW_STATUS.NORMAL:
                        if (animator) animator.SetTrigger("setIdle");
                        image.sprite = sprite_normal;
                        contentBox.SetActive(false);
                        break;
                    case ARROW_STATUS.SELECT:
                        if (animator) animator.SetTrigger("setIdle");
                        image.sprite = sprite_select;
                        if(mData != null && mData.description != "")
                        {
                            contentBox.SetActive(TIMNaviArrowManager.Get.currentStep == mStep);
                        }
                        break;
                    case ARROW_STATUS.BLINK:
                        if (animator) animator.SetTrigger("setBlink");
                        contentBox.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
            get { return mStatus; }
        }

        public void Init(bool isLast = false)
        {
            if (isLast) contentBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(-50, -50, 0);
            if(button != null) button.onClick.AddListener(OnClickButton);
        }

        public void SetData(TIMNaviArrowData data)
        {
            mData = data;
            SetTitle(data.title);
            SetDescription(data.description);
        }

        public void SetTitle(string str)
        {
            title.text = str;
        }

        public void SetDescription(string str)
        {
            des.text = str;
        }

        public void OnClickButton()
        {
            TIMNaviArrowManager.Get.SetStep((int)mStep);
        }
    }

    public enum ARROW_STATUS
    {
        NORMAL,
        SELECT,
        BLINK,
    }
}

