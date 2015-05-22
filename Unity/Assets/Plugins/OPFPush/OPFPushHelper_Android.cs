#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using System;

namespace OnePF.OPFPush
{
	public class OPFPushHelper_Android : IOPFPushHelper
	{
		private AndroidJavaObject _pushHelper_object;

		public OPFPushHelper_Android ()
		{
			var opfPush_class = new AndroidJavaClass ("org.onepf.opfpush.OPFPush");
			_pushHelper_object = opfPush_class.CallStatic<AndroidJavaObject> ("getHelper");
		}

		public void Register ()
		{
			_pushHelper_object.Call ("register");
		}

		public void Unregister ()
		{
			_pushHelper_object.Call ("unregister");
		}

		public string GetRegistrationId ()
		{
			return _pushHelper_object.Call<string> ("getRegistrationId");
		}

		public string GetProviderName ()
		{
			return _pushHelper_object.Call<string> ("getProviderName");
		}

		public bool IsRegistered ()
		{
			return _pushHelper_object.Call<bool> ("isRegistered");
		}

		public bool isRegistering ()
		{
			return _pushHelper_object.Call<bool> ("isRegistering");
		}
	}
}
#endif