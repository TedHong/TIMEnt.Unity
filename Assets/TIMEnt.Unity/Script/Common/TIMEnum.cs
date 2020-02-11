using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TIMEnt.Unity
{

    public class TIMEnum
    {
        public enum PROJECT_TYPE
        {
            NORMAL,
            AR,
            VR,
        }

        public enum EVENTTYPE
        {
            PLAY_AUDIO,
            STOP_AUDIO,
            SHOW_UI,
            FOLLOW_MOVE, // 미구현
            AUTO_WALK,// 미구현
            MOVE,
            INSTANT_MOVE,
            FADE_IN_OUT,
            FADE_IN,
            FADE_OUT,
            FOCUS_OBJECT, //미구혀
            WAIT,
            SET_ANIMATION,
            LOOKAT,
            DELAY,
            CUSTOM,
        }

        public enum OBJECT_TYPE
        {
            PLAYER,
            OBJECT,
        }

        public enum ANI_TYPE
        {
            IDLE,
            TALK,
            WALK,
        }

        public enum ANI_PARAM_TYPE
        {
            FLOAT,
            INT,
            BOOL,
            TRIGGER,
        }

        public enum SOUND_TYPE
        {
            MAIN,
            BGM,
            FX,
            AMBIENT,
        }
    }
}