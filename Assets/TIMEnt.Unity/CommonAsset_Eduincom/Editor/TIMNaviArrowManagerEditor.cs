using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TIMEnt.Unity
{
    [CustomEditor(typeof(TIMNaviArrowManager))]
    public class TIMNaviArrowManagerEditor : Editor
    {
        TIMNaviArrowManager manager;
        private void OnEnable()
        {
            manager = target as TIMNaviArrowManager;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("[ Navi UI 사용법 ]");
            EditorGUILayout.LabelField("1. 메뉴 - TIMEnt_Unity - Create NaviArrows XML 로 XML 파일 생성");
            EditorGUILayout.LabelField("(경로 : Asset\\TIMEnt.Unity\\CommonAsset_Eduincom\\navi_data.xml");
            EditorGUILayout.LabelField("2. XML 데이터 수정");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("[ 함수 설명 ]");
            EditorGUILayout.LabelField("다음 단계 깜빡이기 : TIMNaviArrowManager.Get.SetBlinkNextStep ");
            EditorGUILayout.LabelField("OnClick 이벤트 : 각 Arrow 의 Button 컴포넌트 이용 ");
            EditorGUILayout.Space();
            base.OnInspectorGUI();

            if (GUILayout.Button("Refresh XML Data", GUILayout.Width(300)))
            {
                InitData();
            }

            if (GUILayout.Button("Blink Next", GUILayout.Width(300)))
            {
                TIMNaviArrowManager.Get.SetBlinkNextStep();
            }
        }
        public void InitData()
        {
            string xmlPath = Application.dataPath + @"\TIMEnt.Unity\CommonAsset_Eduincom\" + "navi_data.xml";
            TIMNaviArrowList list = new TIMNaviArrowList();
            TIMUtil.ReadXML<TIMNaviArrowList>(xmlPath, ref list);
            if (list.arrowList.Count > 0)
            {
                manager.arrowCount = list.arrowList.Count;
                for (int i = 0; i < manager.arrows.Count; i++)
                {
                    TIMNaviArrowCtrl arrow = manager.arrows[i];
                    if (i < manager.arrowCount)
                    {
                        arrow.gameObject.SetActive(true);
                        arrow.Init(i + 1 == manager.arrowCount);
                        arrow.status = ARROW_STATUS.NORMAL;
                        arrow.SetData(list.arrowList[i]);
                    }
                    else
                    {
                        arrow.gameObject.SetActive(false);
                    }
                }

                AssetDatabase.SaveAssets();
            }

        }
    }
}

