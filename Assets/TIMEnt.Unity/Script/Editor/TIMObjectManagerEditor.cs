using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TIMEnt.Unity
{

    [CustomEditor(typeof(TIMObjectManager))]
    public class TIMObjectManagerEditor : Editor
    {
        TIMObjectManager manager;

        private void OnEnable()
        {
            manager = target as TIMObjectManager;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            DrawAddField();
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Label("[오브젝트 목록]");
            GUILayout.EndHorizontal();
            DrawKeyValueField();
        }

        string key = "";
        GameObject obj = null;
        void DrawAddField()
        {
            EditorGUILayout.BeginVertical();
           
            GUILayout.BeginHorizontal();
            GUILayout.Label("name");
            key = EditorGUILayout.TextField("", key, GUILayout.MinWidth(100));
            GUILayout.Label("Object");
            obj = (GameObject)EditorGUILayout.ObjectField("", obj, typeof(GameObject), true);
            if (GUILayout.Button("Add", GUILayout.Width(50)))
            {
                if (obj == null)
                {
                    TIMLog.Log("GameObject is null.");
                }
                else
                {
                    TIMLog.Log(manager.AddObject(key, obj));
                    key = "";
                    obj = null;
                }
            }
            GUILayout.EndHorizontal();
            //EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        void DrawKeyValueField()
        {
            TIMObjectDictionary dic = manager.GetIngameDic();
            List<string> kList = new List<string>(dic.Keys);
            foreach (var item in kList)
            {
                string key = item;

                GameObject obj;
                if (dic.TryGetValue(key, out obj))
                {
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.BeginHorizontal();
                    {

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("key");
                        key = EditorGUILayout.TextField("", key, GUILayout.Width(100));
                        GUILayout.Label("Value");
                        obj = (GameObject)EditorGUILayout.ObjectField("", obj, typeof(GameObject), true);

                        if (GUILayout.Button("Del", GUILayout.Width(50)))
                        {
                            if (manager.RemoveObject(key))
                            {
                                TIMLog.Log(key + " has removed.");
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel = 0;
                }
            }


        }

    }
}