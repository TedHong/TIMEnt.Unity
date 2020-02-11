using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TIMEnt.Unity
{
    public class TIMNaviArrowManager : MonoBehaviour
    {
        private static TIMNaviArrowManager instance;
        public static TIMNaviArrowManager Get
        {
            get { return instance; }
        }
        [SerializeField] public int arrowCount;
        [SerializeField] public List<TIMNaviArrowCtrl> arrows = new List<TIMNaviArrowCtrl>();
        [SerializeField] public STEP_TYPE currentStep;

        void Start()
        {
            instance = this;
            InitData();
        }

        void InitArrow()
        {
            for (int i = 1; i < arrows.Count; i++)
            {
                TIMNaviArrowCtrl arrow = arrows[i];
                if (i < arrowCount)
                {
                    arrow.gameObject.SetActive(true);
                    arrow.Init(i + 1 == arrowCount);
                    arrow.status = ARROW_STATUS.NORMAL;
                }
                else
                {
                    arrow.gameObject.SetActive(false);
                }
            }
        }

        public void InitData()
        {
            string xmlPath = Application.dataPath + @"\TIMEnt.Unity\CommonAsset_Eduincom\" + "navi_data.xml";
            TIMNaviArrowList list = new TIMNaviArrowList();
            TIMUtil.ReadXML<TIMNaviArrowList>(xmlPath, ref list);
            if(list.arrowList.Count > 0)
            {
                arrowCount = list.arrowList.Count;
                for (int i = 0; i < arrows.Count; i++)
                {
                    TIMNaviArrowCtrl arrow = arrows[i];
                    if (i < arrowCount)
                    {
                        arrow.gameObject.SetActive(true);
                        arrow.Init(i + 1 == arrowCount);
                        arrow.status = ARROW_STATUS.NORMAL;
                        arrow.SetData(list.arrowList[i]);
                    }
                    else
                    {
                        arrow.gameObject.SetActive(false);
                    }
                }
            }

        }
        
        public void GoNextStep()
        {

        }

        public void GoPrevStep()
        {

        }

        public void SetStep(int step)
        {
            currentStep = (STEP_TYPE)step;
            for (int i = 0; i < arrows.Count; i++)
            {
                TIMNaviArrowCtrl arrow = arrows[i];
                if ((int)arrow.mStep <= step)
                {
                    arrow.status = ARROW_STATUS.SELECT;
                }
                else
                {
                    arrow.status = ARROW_STATUS.NORMAL;
                }
            }
        }

        public void SetBlinkNextStep()
        {
            if((int)currentStep < arrowCount - 1)
            {
                arrows[(int)currentStep + 1].status = ARROW_STATUS.BLINK;
            }
        }

        [MenuItem("TIMEnt_Unity/Create NaviArrows XML")]
        static void CreateNaviArrowsXML()
        {
            string xmlPath = Application.dataPath + @"\TIMEnt.Unity\CommonAsset_Eduincom\" + "navi_data.xml";
            TIMNaviArrowList list = new TIMNaviArrowList();
            list.arrowList = new List<TIMNaviArrowData>();
            list.arrowList.Add(new TIMNaviArrowData() { index = 0, title = "Sample_1", description = "Sample 1 "});

            TIMUtil.WriteXML<TIMNaviArrowList>(list, xmlPath);
        }

    }

    public enum STEP_TYPE
    {
        STEP_0 = 0,
        STEP_1 = 1,
        STEP_2 = 2,
        STEP_3 = 3,
        STEP_4 = 4,
        STEP_5 = 5,
        STEP_6 = 6,
        STEP_7 = 7,
        STEP_8 = 8,
        STEP_9 = 9,
    }
}

