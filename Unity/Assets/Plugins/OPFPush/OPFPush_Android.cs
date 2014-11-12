using UnityEngine;
using System.Collections;

namespace OnePF.Push
{
    public class OPFPush_Android : MonoBehaviour
    {
        private static AndroidJavaObject _push;

        static OPFPush_Android()
        {
            if (Application.platform != RuntimePlatform.Android)
                return;

            AndroidJNI.AttachCurrentThread();

            // Get push client instance
            using (var pushClass = new AndroidJavaClass("org.onepf.opfpush.unity.PushHelper"))
            {
                _push = pushClass.CallStatic<AndroidJavaObject>("instance");
            }
        }

        public void Init(Options options)
        {
            AndroidJavaClass j_currentActivityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject j_currentActivity = j_currentActivityClass.GetStatic<AndroidJavaObject>("currentActivity"); 

            //_push.Call("init", p.ServerUrl, p.SenderId);
        }
    }
}