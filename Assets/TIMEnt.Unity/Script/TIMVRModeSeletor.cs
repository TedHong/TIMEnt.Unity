using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// 카드보드와 일반모드를 선택
/// </summary>
namespace TIMEnt.Unity
{
    public class TIMVRModeSeletor : MonoBehaviour
    {
        public void SelectNone()
        {
            SelectDevice(TIMConstant.DEVICE_NONE);
        }

        public void SelectCardBoard()
        {
            SelectDevice(TIMConstant.DEVICE_CARDBOARD);
        }

        void SelectDevice(string device)
        {
            XRSettings.LoadDeviceByName(device);
            XRSettings.enabled = true;
        }
    }
}
