using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TIMEnt.Unity
{
    [System.Serializable]
    public class TIMNaviArrowList 
    {
        [SerializeField]
       public List<TIMNaviArrowData> arrowList = new List<TIMNaviArrowData>();

    }
}