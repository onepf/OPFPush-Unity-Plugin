#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using System;

namespace OnePF.OPFPush
{
	public class OPFPush_Android : IOPFPush
	{
		public void Register ()
		{
			AndroidJavaClass unityHelper_class = new AndroidJavaClass ("org.onepf.opfpush.unity.UnityHelper");
			unityHelper_class.CallStatic ("register");
		}

		public void Unregister ()
		{
			AndroidJavaClass unityHelper_class = new AndroidJavaClass ("org.onepf.opfpush.unity.UnityHelper");
			unityHelper_class.CallStatic ("unregister");
		}
	}
}
#endif