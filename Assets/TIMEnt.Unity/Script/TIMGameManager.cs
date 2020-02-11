using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Vuforia;

/// <summary>
/// Ted 20191210
/// ********************  작업 순서  ***********************
/// 1. 사용될 오브젝트, 오디오클립, 프리팹 등록
/// 2. AddEventList 함수를 이용해 이벤트 데이터 생성 및 등록
/// TIMEventTask 객체 생성은 TIMEventTask.Get~ 으로 시작하는
/// 각 기능별 Factory 함수 사용
/// 3. 실행 및 테스트
/// ************************ 끝 ****************************
/// </summary>
namespace TIMEnt.Unity
{
    public class TIMGameManager : TIMSingleton<TIMGameManager>
    {
        [SerializeField] TIMEnum.PROJECT_TYPE projectType;

        [Tooltip("시나리오와 무관하게 강제로 진행을 멈춤")]
        public bool isForcePause = false; // 시나리오와 무관하게 강제로 진행을 멈춤
       
        [Tooltip("시나리오 진행 중 필요한 대기 상태")]
        public bool isPause = false; // 시나리오 진행 중 필요한 대기 상태
        
        [Tooltip("task 사이의 딜레이 ")]
        public float taskDelay = 0.5f; // task 사이의 딜레이 
        public bool showLog = true;
        [Header("Play Controller")]
        
        [Tooltip("true : 게임 시작시 자동으로 시나리오 시작")]
        [SerializeField] bool isAutoPlay = true;
        [Tooltip("true : 오디오 클립 재생 이벤트는 패스함.")]
        [SerializeField] bool isPassAudioPlayEvent = false;

        [Tooltip("장면 재생 플래그. true 된 장면의 데이터만 재생합니다. ")]
        [SerializeField] bool play_Scene01 = true; // 장면 재생 플래그. true 된 장면의 데이터만 재생합니다. 
        [SerializeField] bool play_Scene02 = true;
        //[SerializeField] bool play_Scene03 = true;
        //[SerializeField] bool play_Scene04 = true;
        //[SerializeField] bool play_Scene05 = true;

        [Header("Camera")]
        [SerializeField] Camera mainCamera;
        [SerializeField] List<Camera> subCamera;
#if TIM_AR
        // AR 프로젝트용 변수  *******************************************
        [Header("AR Target")]
        [SerializeField] DefaultTrackableEventHandler AR_Target;
        [Header("AR Camera")]
        [SerializeField] Camera arCamera;
        //****************************************************************
#endif



        [Header("Transform")]
        [SerializeField] Transform tempTransform;
        [Header("Vector3")]
        [SerializeField] Vector3 tempPos;
        [Header("UI Prefab")]
        [SerializeField] GameObject tempUI;
       
        [Header("Fade InOUt")]
        [SerializeField] public TIMFadeCtrl fadeCtrl;

        private TIMEventTaskPlayer taskPlayer;

        /// <summary>
        /// TIMEventTask 이벤트 리스트를 모아 놓은 Dictionary. 
        /// </summary>
        Dictionary<string, List<TIMEventTask>> eventDic = new Dictionary<string, List<TIMEventTask>>();

        void Start()
        {
            #if TIM_AR
            // AR용) AR 마커인식이 되면 호출할 함수 등록  **************************
            if (projectType == TIMEnum.PROJECT_TYPE.AR && AR_Target != null) AR_Target.OnTargetFound.AddListener(OnTargetFound);
            //****************************************************************
            #endif

            InitTaskPlayer();
            InitData();

            switch (projectType)
            {
                case TIMEnum.PROJECT_TYPE.NORMAL:
                    if (isAutoPlay)
                    {
                        PlayGame();
                    }
                    break;
                case TIMEnum.PROJECT_TYPE.AR:
                    break;
                case TIMEnum.PROJECT_TYPE.VR:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// AR마커가 인식되면 AR 카메라는 감추고 메인카메라를 활성화 함
        /// </summary>
        public void OnTargetFound()
        {
#if TIM_AR
            TIMLog.Log("OnTargetFound");
            arCamera.SetActive(false);
            mainCamera.SetActive(true);
            PlayGame();
#endif
        }

        void InitTaskPlayer()
        {
            taskPlayer = new TIMEventTaskPlayer(this, isPassAudioPlayEvent);
            taskPlayer.mainCamera = this.mainCamera;
        }

        void PlayGame()
        {
            
            StartCoroutine(taskPlayer.PlayEventTasks());
        }

        /// <summary>
        /// 이벤트 목록 추가
        /// </summary>
        /// <param name="key">장면 구분자</param>
        /// <param name="events">이벤트 리스트</param>
        public void AddEventList(string key, List<TIMEventTask> events)
        {
            if (eventDic.ContainsKey(key)) eventDic.Remove(key);

            eventDic.Add(key, events);
        }

        /// <summary>
        /// Task와 Task 사이에는 0.5초의 딜레이가 기본으로 추가되어 있습니다.
        /// 데이터 생성시 참고하시기 바랍니다.
        /// </summary>
        void InitData()
        {
            // 장면 1 이벤트 
            List<TIMEventTask> events1 = new List<TIMEventTask>();
            events1.Add(TIMEventTask.GetFadeTransition(TIMEnum.EVENTTYPE.FADE_IN));
            events1.Add(TIMEventTask.GetLookAtTarget("cube", "tower"));
            events1.Add(TIMEventTask.GetInstantMove("cube", Vector3.zero, 0, 0, 0, 0));
            events1.Add(TIMEventTask.GetFadeTransition(TIMEnum.EVENTTYPE.FADE_OUT));
            eventDic.Add("scene1", events1);


            // 장면 2 이벤트 
            List<TIMEventTask> events2 = new List<TIMEventTask>();
            events2.Add(TIMEventTask.GetFadeTransition(TIMEnum.EVENTTYPE.FADE_IN));
            events2.Add(TIMEventTask.GetMove("cube", new Vector3(15, 0, -25), 0, 0, 0, 1, 2, 2.5f));
            events2.Add(TIMEventTask.GetAnimationPlayByBool("cube", "isSpin", true));
            //events2.Add(TIMEventTask.GetFadeTransition(TIMEnum.EVENTTYPE.FADE_OUT));
            eventDic.Add("scene2", events2);

            // 장면 3 이벤트 
            //List<TIMEventTask> events3 = new List<TIMEventTask>();
            //eventDic.Add("scene3", events3);

            // 장면 4 이벤트 
            //List<TIMEventTask> events4 = new List<TIMEventTask>();
            //eventDic.Add("scene4", events3);

            // 장면 5 이벤트 
            //List<TIMEventTask> events5 = new List<TIMEventTask>();
            //eventDic.Add("scene5", events5);
        }

        public List<TIMEventTask> GetEventList()
        {
            List<TIMEventTask> result = new List<TIMEventTask>();
            List<string> keys = new List<string>();
            if (play_Scene01) keys.Add("scene1");
            if (play_Scene02) keys.Add("scene2");
            //if (play_Scene03) keys.Add("scene3");
            //if (play_Scene04) keys.Add("scene4");
            //if (play_Scene05) keys.Add("scene5");

            for (int i = 0; i < keys.Count; i++)
            {
                result.AddRange(eventDic[keys[i]]);
            }

            return result;
        }

        void Update()
        {

        }

        [MenuItem("TIMEnt_Unity/InitObject")]
        static void InitTimentObjects()
        {
            if(TIMObjectManager.Get == null)
            {
                GameObject objectManger = new GameObject();
                objectManger.name = "TIMObjectManager";
                objectManger.AddComponent<TIMObjectManager>();
            }
            
            if(TIMSoundManager.Get == null)
            {
                GameObject soundManger = new GameObject();
                soundManger.name = "TIMSoundManager";
                soundManger.AddComponent<TIMSoundManager>();
            }
        }

        [MenuItem("TIMEnt_Unity/Set ProjetType VR")]
        static void SetProjectType_VR()
        {
            string str = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (str.Contains("TIM_")) TIMLog.Log("PlayerSetting - Other Setting - Scripting Defing Symbols 에서 TIM 으로 시작하는 심볼을 지워주세요.");
            else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "TIM_VR");
        }

        [MenuItem("TIMEnt_Unity/Set ProjetType AR")]
        static void SetProjectType_AR()
        {
            string str = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            if (str.Contains("TIM_")) TIMLog.Log("PlayerSetting - Other Setting - Scripting Defing Symbols 에서 TIM 으로 시작하는 심볼을 지워주세요.");
            else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "TIM_AR");
        }
    }
}