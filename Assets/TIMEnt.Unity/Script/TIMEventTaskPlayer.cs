using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ted, 201912010 
/// </summary>
/// 
namespace TIMEnt.Unity
{
    public class TIMEventTaskPlayer
    {
        TIMGameManager manager;
        public bool playEventOnly; //no sound, only event
        public bool isVRMode;
        public Camera mainCamera;
        //public AudioSource audioSource;
        float pauseDelay = 0;

        public List<TIMEventTask> eventTasks = new List<TIMEventTask>();

        #region Delegates
        public delegate IEnumerator PlayAudio(TIMEventTask task);
        public static event         PlayAudio playAudio;
        public delegate IEnumerator StopAudio(TIMEventTask task);
        public static event         StopAudio stopAudio;
        public delegate IEnumerator ShowUI(TIMEventTask task);
        public static event         ShowUI showUI;
        public delegate IEnumerator InstantMove(TIMEventTask task);
        public static event         InstantMove instantMove;
        public delegate IEnumerator NormalMove(TIMEventTask task);
        public static event         NormalMove normalMove;
        public delegate IEnumerator SetFadeInOut();
        public static event         SetFadeInOut setFadeInOut;
        public delegate IEnumerator SetFadeInEvent();
        public static event         SetFadeInEvent setFadeIn;
        public delegate IEnumerator SetFadeOutEvent();
        public static event         SetFadeOutEvent setFadeOut;
        public delegate IEnumerator SetWait();
        public static event         SetWait setWait;
        public delegate IEnumerator SetAnimation(TIMEventTask task);
        public static event         SetAnimation setAnimation;
        public delegate IEnumerator SetLookAt(TIMEventTask task);
        public static event         SetLookAt setLookAt;
        public delegate IEnumerator SetCustomEvent(TIMEventTask task);
        public static event         SetCustomEvent setCustomEvent;
        #endregion

        public TIMEventTaskPlayer(TIMGameManager m, bool play_event_only)
        {
            manager = m;
            playEventOnly = play_event_only;
            InitDelegate();
        }

        public IEnumerator PlayEventTasks(bool usePauseTimer = false)
        {
            yield return new WaitForSeconds(manager.taskDelay);

            if (eventTasks.Count < 1) eventTasks = manager.GetEventList();

            for (int i = 0; i < eventTasks.Count; i++)
            {
                while (manager.isForcePause)
                {
                    yield return new WaitForEndOfFrame();
                }

                while (manager.isPause)
                {
                    if (usePauseTimer) SetPauseTimer(pauseDelay);

                    yield return new WaitForEndOfFrame();
                }

                if (pausedTime != 0) pausedTime = 0;

                yield return manager.StartCoroutine(GetEvent(eventTasks[i]));
            }
        }

        float pausedTime = 0;
        /// <summary>
        /// 지정된 시간이 지나면 자동으로 다음단계로 진행시키는 함수
        /// </summary>
        /// <param name="pauseDelay">멈춤 시간</param>
        void SetPauseTimer(float pauseDelay)
        {
            if (pausedTime == 0) pausedTime = Time.time;
            if (Time.time - pausedTime > pauseDelay) // 지정한 지났는데도 정지 상태이면 자동으로 진행 시킴
            {
                manager.isPause = false;
                pausedTime = 0;
            }
        }

        IEnumerator GetEvent(TIMEventTask task)
        {
            switch (task.eventType)
            {
                case TIMEnum.EVENTTYPE.PLAY_AUDIO:
                    yield return playAudio(task);
                    break;
                case TIMEnum.EVENTTYPE.STOP_AUDIO:
                    yield return playAudio(task);
                    break;
                case TIMEnum.EVENTTYPE.SHOW_UI:
                    yield return showUI(task);
                    break;
                case TIMEnum.EVENTTYPE.FOLLOW_MOVE:
                    break;
                case TIMEnum.EVENTTYPE.AUTO_WALK:
                    break;
                case TIMEnum.EVENTTYPE.INSTANT_MOVE:
                    yield return instantMove(task);
                    break;
                case TIMEnum.EVENTTYPE.MOVE:
                    yield return normalMove(task);
                    break;
                case TIMEnum.EVENTTYPE.FADE_IN_OUT:
                    yield return setFadeInOut();
                    break;
                case TIMEnum.EVENTTYPE.FADE_IN:
                    yield return setFadeIn();
                    break;
                case TIMEnum.EVENTTYPE.FADE_OUT:
                    yield return setFadeOut();
                    break;
                case TIMEnum.EVENTTYPE.FOCUS_OBJECT:
                    break;
                case TIMEnum.EVENTTYPE.WAIT:
                    yield return setWait();
                    break;
                case TIMEnum.EVENTTYPE.SET_ANIMATION:
                    yield return setAnimation(task);
                    break;
                case TIMEnum.EVENTTYPE.LOOKAT:
                    yield return setLookAt(task);
                    break;
                case TIMEnum.EVENTTYPE.CUSTOM:
                    yield return setCustomEvent(task);
                    break;
                default:
                    break;
            }
        }

        public void InitDelegate()
        {
            playAudio = SetPlayAudioClip;
            stopAudio = SetStopAudio;
            showUI = SetShowUI;
            instantMove = SetInstantMove;
            normalMove = SetMoveBySpeed;
            setFadeInOut = SetFadeInOutTransition;
            setFadeIn = SetFadeIn;
            setFadeOut = SetFadeOut;
            setWait = SetWaitFlag;
            setAnimation = SetPlayAnimation;
            setLookAt = SetLookAtTarget;
            setCustomEvent = PlayCustomEvent;
        }

        #region 이벤트 처리부

        // 오디오 클립 재생
        public IEnumerator SetPlayAudioClip(TIMEventTask task)
        {
            if (playEventOnly == false)
            {
                TIMEnum.SOUND_TYPE type = task.soundType;
                int index = task.audioClipIndex;
                TIMLog.Log("[Task] SetPlayAudioClips : {0}_{1}", type, index);

                TIMSoundManager.Get.PlaySound(type, index, task.delay);
                float clength = TIMSoundManager.Get.GetClipLength(type, index);
                yield return new WaitForSeconds(clength + 0.1f);
                TIMLog.Log("[{0}_{1}] playback complete.", type, index);
            }
            else
                yield return new WaitForSeconds(1.0f);

            yield return new WaitForEndOfFrame();
        }

        // 오디오 소스 재생 멈춤
        public IEnumerator SetStopAudio(TIMEventTask task)
        {
            if (playEventOnly == false)
            {
                TIMEnum.SOUND_TYPE type = task.soundType;
                TIMLog.Log("[Task] SetStopAudio : {0}_{1}", type);

                TIMSoundManager.Get.StopSound(type);
            }
            else
                yield return new WaitForSeconds(1.0f);

            yield return new WaitForEndOfFrame();
        }

        // UI 표시
        public IEnumerator SetShowUI(TIMEventTask task)
        {
            TIMLog.Log("[Task] SetShowUI");
            switch (task.npcType)
            {
                case TIMEnum.OBJECT_TYPE.PLAYER:
                    //player.SetShowUI(true);
                    break;
                default:
                    break;
            }
            yield return new WaitForEndOfFrame();
        }

        // 오브젝트를 즉시 이동 시킴
        public IEnumerator SetInstantMove(TIMEventTask task)
        {
            TIMLog.Log("[Task] SetInstantMove : " + task.npcType.ToString());
            yield return new WaitForSeconds(task.delay);
            GameObject obj = TIMObjectManager.Get.GetGameObject(task.selectObjectName);
            if (obj != null)
            {
                Transform moveTarget = obj.transform;
                Vector3 goalPos = task.targetPosition;
                moveTarget.position = goalPos;
            }

            yield return new WaitForEndOfFrame();
        }

        // 속도값을 이용해 이동시킴
        public IEnumerator SetMoveBySpeed(TIMEventTask task)
        {
            TIMLog.Log("[Task] SetMoveBySpeed : " + task.npcType.ToString());
            yield return new WaitForSeconds(task.delay);
            GameObject obj = TIMObjectManager.Get.GetGameObject(task.selectObjectName);
            if (obj != null)
            {
                TIMMoveCtrl ctrl = obj.GetComponent<TIMMoveCtrl>();
                if (ctrl == null) ctrl = obj.AddComponent<TIMMoveCtrl>();

                ctrl.SetMoveToPosition(task);
            }
            yield return new WaitForSeconds(task.post_delay);
        }

        /* TIMMoveCtrl 스크립트 안에 추가함
        IEnumerator SetMoveInTarget(GameObject obj, TIMEventTask task)
        {
            Transform moveTarget = obj.transform;
            Vector3 goalPos = task.targetPosition;
            float speed = task.speed;

            Vector3 startPos = moveTarget.position;
            float step = (speed / (startPos - goalPos).magnitude) * Time.fixedDeltaTime;
            float t = 0;
            while (t <= 1.0f)
            {
                t += step;
                moveTarget.position = Vector3.Lerp(startPos, goalPos, t);
                yield return new WaitForFixedUpdate();
            }
        }*/

        public IEnumerator SetFadeInOutTransition()
        {
            TIMLog.Log("[Task] SetFadeInOutTransition");
            FadeToBlack();
            yield return new WaitForSeconds(1.5f);
            FadeFromBlack();
            yield return new WaitForSeconds(1.5f);
            manager.fadeCtrl.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator SetFadeIn()
        {
            manager.fadeCtrl.gameObject.SetActive(true);
            FadeFromBlack();
            yield return new WaitForSeconds(1.5f);
            manager.fadeCtrl.gameObject.SetActive(false);
        }

        public IEnumerator SetFadeOut()
        {
            manager.fadeCtrl.gameObject.SetActive(true);
            FadeToBlack();
            yield return new WaitForSeconds(1.5f);
        }

        private void FadeToBlack()
        {
            manager.fadeCtrl.SetFadeOut();

            //****************************************
            // SteamVR SDK 용
            //****************************************
            //set start color
            //SteamVR_Fade.Start(Color.clear, 0f);
            //set and start fade to
            //SteamVR_Fade.Start(Color.black, 1.0f);
            //****************************************
        }

        private void FadeFromBlack()
        {
            manager.fadeCtrl.SetFadeIn();
            //****************************************
            // SteamVR SDK 용
            //****************************************
            //set start color
            //SteamVR_Fade.Start(Color.black, 0f);
            //set and start fade to
            //SteamVR_Fade.Start(Color.clear, 1.0f);
            //****************************************
        }

        public IEnumerator SetWaitFlag()
        {
            TIMLog.Log("[Task] SetWaitFlag");
            SetWaitFlag(true);
            yield return new WaitForEndOfFrame();
        }

        public void SetWaitFlag(bool flag)
        {
            manager.isPause = flag;
        }

        public IEnumerator SetPlayAnimation(TIMEventTask task)
        {
            TIMLog.Log("[Task] SetCharAnimation : " + task.selectObjectName);
            TIMLog.Log("[Task] ㄴ Ani parm  : " + task.aniParamType);
            TIMLog.Log("[Task] ㄴ Ani type  : " + task.aniName);

            GameObject obj = TIMObjectManager.Get.GetGameObject(task.selectObjectName);
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
                animator.Rebind();
                animator.speed = task.speed;
                switch (task.aniParamType)
                {
                    case TIMEnum.ANI_PARAM_TYPE.FLOAT:
                        animator.SetFloat(task.aniName, task.ani_value_float);
                        break;
                    case TIMEnum.ANI_PARAM_TYPE.INT:
                        animator.SetInteger(task.aniName, task.ani_value_int);
                        break;
                    case TIMEnum.ANI_PARAM_TYPE.BOOL:
                        animator.SetBool(task.aniName, task.ani_value_bool);
                        break;
                    case TIMEnum.ANI_PARAM_TYPE.TRIGGER:
                        animator.SetTrigger(task.aniName);
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator SetLookAtTarget(TIMEventTask task)
        {
            yield return new WaitForSeconds(0.1f);
            TIMLog.Log("[Task] SetLookAtTarget : " + task.selectObjectName + " / target : " + task.targetObjectName);

            GameObject obj = TIMObjectManager.Get.GetGameObject(task.selectObjectName);
            if (obj != null)
            {
                TIMMoveCtrl ctrl = obj.GetComponent<TIMMoveCtrl>();
                if (ctrl == null) ctrl = obj.AddComponent<TIMMoveCtrl>();

                Transform target = (task.targetObjectName == "") ? task.targetTransform : TIMObjectManager.Get.GetGameObject(task.targetObjectName).transform;

                ctrl.SetLookAt(target, task.rotateSmooth);
            }

            yield return new WaitForEndOfFrame();
        }

        /// <summary>
        /// 커스텀 코루틴을 동작시킴
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public IEnumerator PlayCustomEvent(TIMEventTask task)
        {
            yield return manager.StartCoroutine(task.customEvent);
        }
        #endregion
    }
}