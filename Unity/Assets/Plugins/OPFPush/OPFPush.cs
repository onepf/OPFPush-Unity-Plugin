using UnityEngine;
using System.Collections;

namespace OnePF.OPFPush
{
    public static class OPFPush
    {
        public delegate void InitFinishedDelegate(bool success, string message);
        public static event InitFinishedDelegate InitFinished;

        static IOPFPush _push = null;

        static EventReceiver _eventReceiver = null;

        static OPFPush()
        {
#if UNITY_ANDROID
            _push = new OPFPush_Android();
#else
			Debug.LogError("OPFPush is currently not supported on this platform. Sorry.");
            return;
#endif
        }

        public static void Init(Options options)
        {
            if (_eventReceiver == null)
            {
                _eventReceiver = new GameObject("OPFPush").AddComponent<EventReceiver>();

                _eventReceiver.InitSucceded += delegate(string registrationID)
                {
                    if (InitFinished != null)
                        InitFinished(true, registrationID);
                };

                _eventReceiver.InitFailed += delegate(string error)
                {
                    if (InitFinished != null)
                        InitFinished(false, error);
                };
            }

            _push.Init(options);
        }
    }
}