#if UNITY_IOS
using UnityEngine;

namespace OnePF.OPFPush
{
    public class OPFPush_iOS : IOPFPush
    {
        public event OPFPush.InitFinishedDelegate InitFinished;

		OPFPush_iOS_helper _helper;

        public void Init(Options options)
        {
			_helper = new GameObject(typeof(OPFPush_iOS_helper).Name).AddComponent<OPFPush_iOS_helper>();
            _helper.InitFinished += Helper_InitFinished;
			_helper.Init();
        }

        void Helper_InitFinished(bool success, string message)
        {
            if (InitFinished != null)
                InitFinished(success, message);
        }
    }
}
#endif