using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 직렬화 Dictionary 구현 클래스
/// </summary>
namespace TIMEnt.Unity
{
    [Serializable]
    public class TIMObjectDictionary : TIMSerializableDictionary<string, GameObject> { }
}