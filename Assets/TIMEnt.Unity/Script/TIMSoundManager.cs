using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TIMEnt.Unity
{
    public class TIMSoundManager : TIMSingleton<TIMSoundManager>
    {
        [Header("오디오 소스")]
        [SerializeField] AudioSource AudioSource_Main;
        [SerializeField] AudioSource AudioSource_FX;
        [SerializeField] AudioSource AudioSource_Ambient;
        [SerializeField] AudioSource AudioSource_BGM;

        [Header("오디오 클립")]
        [SerializeField] List<AudioClip> Clips_Main;
        [SerializeField] List<AudioClip> Clips_FX;
        [SerializeField] List<AudioClip> Clips_Ambient;
        [SerializeField] List<AudioClip> Clips_BGM;

        public void PlaySound(TIMEnum.SOUND_TYPE type, int index, float delay)
        {
            AudioSource audio = AudioSource_Main;
            switch (type)
            {
                case TIMEnum.SOUND_TYPE.MAIN:
                    audio = AudioSource_Main;
                    audio.clip = Clips_Main[index];
                    break;
                case TIMEnum.SOUND_TYPE.BGM:
                    audio = AudioSource_BGM;
                    audio.clip = Clips_BGM[index];
                    break;
                case TIMEnum.SOUND_TYPE.FX:
                    audio = AudioSource_FX;
                    audio.clip = Clips_FX[index];
                    break;
                case TIMEnum.SOUND_TYPE.AMBIENT:
                    audio = AudioSource_Ambient;
                    audio.clip = Clips_Ambient[index];
                    break;
                default:
                    break;
            }
            audio.PlayDelayed(delay);
        }

        public void StopSound(TIMEnum.SOUND_TYPE type)
        {
            AudioSource audio = AudioSource_Main;
            switch (type)
            {
                case TIMEnum.SOUND_TYPE.MAIN:
                    audio = AudioSource_Main;
                    break;
                case TIMEnum.SOUND_TYPE.BGM:
                    audio = AudioSource_BGM;
                    break;
                case TIMEnum.SOUND_TYPE.FX:
                    audio = AudioSource_FX;
                    break;
                case TIMEnum.SOUND_TYPE.AMBIENT:
                    audio = AudioSource_Ambient;
                    break;
                default:
                    break;
            }

            if (audio != null && audio.isPlaying)
                audio.Stop();
        }

        public void StopAll()
        {
            if (AudioSource_Main != null && AudioSource_Main.isPlaying) AudioSource_Main.Stop();
            if (AudioSource_BGM != null && AudioSource_BGM.isPlaying) AudioSource_BGM.Stop();
            if (AudioSource_FX != null && AudioSource_FX.isPlaying) AudioSource_FX.Stop();
            if (AudioSource_Ambient != null && AudioSource_Ambient.isPlaying) AudioSource_Ambient.Stop();
        }

        public float GetClipLength(TIMEnum.SOUND_TYPE type, int index)
        {
            switch (type)
            {
                case TIMEnum.SOUND_TYPE.MAIN:
                    return Clips_Main[index].length;
                case TIMEnum.SOUND_TYPE.BGM:
                    return Clips_BGM[index].length;
                case TIMEnum.SOUND_TYPE.FX:
                    return Clips_FX[index].length;
                case TIMEnum.SOUND_TYPE.AMBIENT:
                    return Clips_Ambient[index].length;
                default:
                    break;
            }

            return 0;
        }

        public int GetClipIndex(TIMEnum.SOUND_TYPE type, string name)
        {
            List<AudioClip> list = new List<AudioClip>();
            switch (type)
            {
                case TIMEnum.SOUND_TYPE.MAIN:
                    list = Clips_Main;
                    break;
                case TIMEnum.SOUND_TYPE.BGM:
                    list = Clips_BGM;
                    break;
                case TIMEnum.SOUND_TYPE.FX:
                    list = Clips_FX;
                    break;
                case TIMEnum.SOUND_TYPE.AMBIENT:
                    list = Clips_Ambient;
                    break;
                default:
                    break;
            }
            int result = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (name == list[i].name)
                {
                    result = i;
                    continue;
                }
            }

            return result;
        }

        public float GetClipLength(TIMEnum.SOUND_TYPE type, string name)
        {
            return GetClipLength(type, GetClipIndex(type, name));
        }

        public IEnumerator PlaySoudWithLength(TIMEnum.SOUND_TYPE type, string name)
        {
            int idx = GetClipIndex(type, name);
            PlaySound(type, idx, 0);
            yield return new WaitForSeconds(GetClipLength(type, idx));
        }

    }
}