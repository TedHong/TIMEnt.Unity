using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ted, 이벤트 데이터 클래스
/// </summary>
namespace TIMEnt.Unity
{
    public class TIMEventTask
    {
        public TIMEnum.EVENTTYPE eventType;
        public TIMEnum.OBJECT_TYPE npcType;
        public TIMEnum.SOUND_TYPE soundType;
        //public List<AudioClip> audioClips = new List<AudioClip>();
        public int audioClipIndex;
        public Vector3 targetPosition;
        public TIMEnum.OBJECT_TYPE targetNpcType;
        public string selectObjectName;
        public string targetObjectName;
        public float delay;
        public float post_delay;
        public float speed;
        public TIMEnum.ANI_TYPE aniType;
        public TIMEnum.ANI_PARAM_TYPE aniParamType;
        public string aniName;
        public float ani_value_float;
        public int ani_value_int;
        public bool ani_value_bool;
        public IEnumerator customEvent;
        public Transform targetTransform;
        public bool rotateSmooth;
        public TIMEventTask() { }
        /// <summary>
        /// Fade In/Out , Wait
        /// </summary>
        /// <param name="t"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t)
        {
            eventType = t;
        }

        

        /// <summary>
        /// Focus Object
        /// </summary>
        /// <param name="t"></param>
        /// <param name="tName"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, string tName)
        {
            eventType = t;
            selectObjectName = tName;
        }

        public TIMEventTask(TIMEnum.EVENTTYPE t, Transform tr)
        {
            eventType = t;
            targetTransform = tr;
        }

        /// <summary>
        /// Set Delay
        /// </summary>
        /// <param name="t"></param>
        /// <param name="d"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, float d)
        {
            eventType = t;
            delay = d;
        }

        /// <summary>
        /// Play Audio Clip
        /// </summary>
        /// <param name="t"></param>
        /// <param name="s_type"></param>
        /// <param name="index"></param>
        /// <param name="preDelay"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, TIMEnum.SOUND_TYPE s_type, int index, float preDelay)
        {
            eventType = t;
            soundType = s_type;
            audioClipIndex = index;
            delay = preDelay;
        }

        public TIMEventTask(TIMEnum.EVENTTYPE t, TIMEnum.SOUND_TYPE s_type)
        {
            eventType = t;
            soundType = s_type;
        }

        /// <summary>
        /// Look at target / Follow move
        /// </summary>
        /// <param name="t"></param>
        /// <param name="nm"></param>
        /// <param name="n"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, TIMEnum.OBJECT_TYPE type, TIMEnum.OBJECT_TYPE target, Transform tr)
        {
            eventType = t;
            npcType = type;
            targetNpcType = target;
            targetTransform = tr;
        }
        public TIMEventTask(TIMEnum.EVENTTYPE t, string mName, string tName, Transform tr, bool RotateSmooth)
        {
            eventType = t;
            selectObjectName = mName;
            targetObjectName = tName;
            targetTransform = tr;
            rotateSmooth = RotateSmooth;
        }

        /// <summary>
        /// Show UI 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <param name="uiname"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, string uiname, Transform parent)
        {
            eventType = t;
            selectObjectName = uiname;
            targetTransform = parent;
        }

        /// <summary>
        /// Set Animation
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <param name="ani"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, TIMEnum.OBJECT_TYPE type, TIMEnum.ANI_TYPE ani)
        {
            eventType = t;
            npcType = type;
            aniType = ani;
        }

        public TIMEventTask(TIMEnum.EVENTTYPE t, string name, string ani_name, TIMEnum.ANI_PARAM_TYPE paramType, float val1, int val2, bool flag, float ani_speed = 1)
        {
            eventType = t;
            selectObjectName = name;
            aniName = ani_name;
            aniParamType = paramType;
            ani_value_float = val1;
            ani_value_int = val2;
            ani_value_bool = flag;
            speed = ani_speed;
        }


        /// <summary>
        /// Auto move / Instant move
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, string name, Vector3 p, float offset_x, float offset_y, float offset_z, float d)
        {
            eventType = t;
            selectObjectName = name;
            if (offset_x != 0) p.x += offset_x;
            if (offset_y != 0) p.y += offset_y;
            if (offset_z != 0) p.z += offset_z;
            targetPosition = p;
            delay = d;
        }

        /// <summary>
        /// Normal Move
        /// </summary>
        /// <param name="t"></param>
        /// <param name="charType"></param>
        /// <param name="p"></param>
        /// <param name="offset_x"></param>
        /// <param name="offset_y"></param>
        /// <param name="offset_z"></param>
        /// <param name="d"></param>
        /// <param name="sp">speed</param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, string name, Vector3 p, float offset_x, float offset_y, float offset_z, float d, float pd, float sp)
        {
            eventType = t;
            selectObjectName = name;
            if (offset_x != 0) p.x += offset_x;
            if (offset_y != 0) p.y += offset_y;
            if (offset_z != 0) p.z += offset_z;
            targetPosition = p;
            delay = d;
            post_delay = pd;
            speed = sp;
        }

        /// <summary>
        /// Play Custom Corutine
        /// </summary>
        /// <param name="t"></param>
        /// <param name="e"></param>
        public TIMEventTask(TIMEnum.EVENTTYPE t, IEnumerator e)
        {
            eventType = t;
            customEvent = e;
        }


        #region Factory Methods

        /// <summary>
        /// Fade In/Out 화면 전환 이벤트
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TIMEventTask GetFadeTransition(TIMEnum.EVENTTYPE t)
        {
            return new TIMEventTask(t);
        }
        /// <summary>
        /// (미구현) 오브젝트에 카메라 포커싱
        /// </summary>
        /// <param name="tName">오브젝트 이름</param>
        /// <returns></returns>
        public static TIMEventTask GetFocusObject(string tName)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.FOCUS_OBJECT, tName);
        }

        /// <summary>
        /// (미구현) 오브젝트에 카메라 포커싱
        /// </summary>
        /// <param name="tr">대상 오브젝트의 Transform</param>
        /// <returns></returns>
        public static TIMEventTask GetFocusObject(Transform tr)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.FOCUS_OBJECT, tr);
        }

        /// <summary>
        /// 대기 이벤트
        /// </summary>
        /// <returns></returns>
        public static TIMEventTask GetWait()
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.WAIT);
        }

        /// <summary>
        /// 이벤트 사이에 딜레이 추가
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static TIMEventTask GetDelayEvent(float d)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.DELAY, d);
        }

        /// <summary>
        /// 오디오 클립을 재생함
        /// </summary>
        /// <param name="clip">클립</param>
        /// <param name="preDelay">사전 딜레이</param>
        /// <returns></returns>
        public static TIMEventTask GetPlayAudioClip(TIMEnum.SOUND_TYPE sType, int clipIndex, float preDelay = 0)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.PLAY_AUDIO, sType, clipIndex, preDelay);
        }

        /// <summary>
        /// 오디오 정지
        /// </summary>
        /// <param name="sType">오디오 소스 타입</param>
        /// <returns></returns>
        public static TIMEventTask GetStopAudio(TIMEnum.SOUND_TYPE sType)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.PLAY_AUDIO, sType);
        }

        /// <summary>
        /// 지정 오브젝트가 대상 오브젝트를 바라보도록 함
        /// </summary>
        /// <param name="type">지정 오브젝트 타입</param>
        /// <param name="target">대상 오브젝트 타입</param>
        /// <param name="tr">대상 오브젝트 타입이 없으면 Transform 을 지정</param>
        /// <param name="rotateSmooth">부드럽게 회전 시키려면 true</param>
        /// <returns></returns>
        public static TIMEventTask GetLookAtTarget(string m_name, string t_name, Transform tr = null, bool rotateSmooth = false)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.LOOKAT, m_name, t_name, tr, rotateSmooth);
        }

        /// <summary>
        /// (미구현) 지정 오브젝트가 대상 오브젝트를 따라서 이동하게 함
        /// </summary>
        /// <param name="type">지정 오브젝트</param>
        /// <param name="target">대상 오브젝트</param>
        /// <param name="tr">대상 오브젝트 타입이 없으면 Transform 을 지정</param>
        /// <returns></returns>
        public static TIMEventTask GetFollowMoveTarget(TIMEnum.OBJECT_TYPE type, TIMEnum.OBJECT_TYPE target, Transform tr = null)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.FOLLOW_MOVE, type, target, tr);
        }

        /// <summary>
        /// UI 호출
        /// </summary>
        /// <param name="uiname">UI Prefab 의 파일명</param>
        /// <param name="parent">UI의 부모 Transform</param>
        /// <returns></returns>
        public static TIMEventTask GetShowUI(string uiname, Transform parent = null)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.SHOW_UI, uiname, parent);
        }

        /// <summary>
        /// 대상의 애니메이션 플레이
        /// </summary>
        /// <param name="type">대상 타입</param>
        /// <param name="ani">애니메이션 클립 이름</param>
        /// <returns></returns>
        public static TIMEventTask GetAnimationPlay(TIMEnum.OBJECT_TYPE type, TIMEnum.ANI_TYPE ani)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.SET_ANIMATION, type, ani);
        }

        public static TIMEventTask GetAnimationPlayByFloat(string object_name, string ani_name, float val)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.SET_ANIMATION, object_name, ani_name, TIMEnum.ANI_PARAM_TYPE.FLOAT, val, 0, false);
        }
        public static TIMEventTask GetAnimationPlayByInt(string object_name, string ani_name, int val)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.SET_ANIMATION, object_name, ani_name, TIMEnum.ANI_PARAM_TYPE.INT, 0, val, false);
        }
        public static TIMEventTask GetAnimationPlayByBool(string object_name, string ani_name, bool val)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.SET_ANIMATION, object_name, ani_name, TIMEnum.ANI_PARAM_TYPE.BOOL, 0, 0, val);
        }
        public static TIMEventTask GetAnimationPlayByTrigger(string object_name, string ani_name)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.SET_ANIMATION, object_name, ani_name, TIMEnum.ANI_PARAM_TYPE.TRIGGER, 0, 0, false);
        }

        /// <summary>
        /// 즉시 이동
        /// </summary>
        /// <param name="type">대상 타입</param>
        /// <param name="p">위치</param>
        /// <param name="offset_x">오프셋</param>
        /// <param name="offset_y">오프셋</param>
        /// <param name="offset_z">오프셋</param>
        /// <param name="d">사전 지연시간</param>
        /// <returns></returns>
        public static TIMEventTask GetInstantMove(string name, Vector3 p, float offset_x = 0, float offset_y = 0, float offset_z = 0, float d = 0)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.INSTANT_MOVE, name, p, offset_x, offset_y, offset_z, d);
        }

        /// <summary>
        /// 일반 이동
        /// </summary>
        /// <param name="type"></param>
        /// <param name="p"></param>
        /// <param name="offset_x"></param>
        /// <param name="offset_y"></param>
        /// <param name="offset_z"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static TIMEventTask GetMove(string name, Vector3 p, float offset_x = 0, float offset_y = 0, float offset_z = 0, float preDelay = 0, float postDelay = 0, float speed = 1)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.MOVE, name, p, offset_x, offset_y, offset_z, preDelay, postDelay, speed);
        }

        /// <summary>
        /// 임의의 코루틴 함수 실행
        /// </summary>
        /// <param name="e">코루틴에 사용할 IEnumerator 함수</param>
        /// <returns></returns>
        public static TIMEventTask GetCustomEvent(IEnumerator e)
        {
            return new TIMEventTask(TIMEnum.EVENTTYPE.CUSTOM, e);
        }

        #endregion
    }

}
