using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace TIMEnt.Unity
{
    public class TIMUtil
    {
        /// <summary>
        /// XML 파일 쓰기
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="path">Path.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void WriteXML<T>(T type, string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                var stream = new FileStream(path, FileMode.Create);
                serializer.Serialize(stream, type);
                stream.Close();
            }
            catch (System.Exception ex)
            {
                TIMLog.Log(ex.ToString());
            }

        }

        public static void ReadXML<T>(string path, ref T result)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                var stream = new FileStream(path, FileMode.Open);
                if (stream != null)
                {
                    result = (T)serializer.Deserialize(stream);
                    stream.Close();

                }
            }
            catch (System.Exception ex)
            {
                TIMLog.Log(ex.ToString());
            }
        }

        public static bool DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            else
                return false;
        }

        public static string AddComma(int d)
        {
            return string.Format("{0:n0}", d);
        }

        public static void LookTarget(Transform tr, Vector3 myPos, Vector3 targetPos)
        {
            Vector3 degree = Vector3.zero;
            degree.z = GetDegree(myPos, targetPos);
            tr.localEulerAngles = degree;
        }

        /// <summary>
        /// 두 Vector3 사이의 각도 구하기
        /// </summary>
        /// <param name="m"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        static float GetDegree(Vector3 m, Vector3 t)
        {
            float ang = Mathf.Atan2(t.y - m.y, t.x - m.x) * 180 / Mathf.PI;
            if (ang < 0) ang += 360;

            return ang;
        }
    }
}

