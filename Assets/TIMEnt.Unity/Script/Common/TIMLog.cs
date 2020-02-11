using System;
using System.Text;
using UnityEngine;

namespace TIMEnt.Unity
{
    public class TIMLog : MonoBehaviour
    {
        public static void Log(string str)
        {
            if (TIMGameManager.Get.showLog)
            {
                Debug.Log(string.Format("[{0}] {1}", DateTime.Now.ToString(), str));
            }
        }
        public static void Log(string str, params object[] p)
        {
            if (TIMGameManager.Get.showLog)
            {
                StringBuilder b = new StringBuilder();
                b.Append(string.Format("[{0}] ", DateTime.Now.ToString()));
                b.Append(string.Format(str, p));
                Debug.Log(b.ToString());
            }
        }

        public static void LogError(string str)
        {
            if (TIMGameManager.Get.showLog)
            {
                Debug.LogError(string.Format("[{0}] {1}", DateTime.Now.ToString(), str));
            }
        }
    }

}
