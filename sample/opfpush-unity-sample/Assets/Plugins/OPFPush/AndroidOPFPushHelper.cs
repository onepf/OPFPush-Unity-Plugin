#if UNITY_ANDROID
using UnityEngine;

namespace OnePF.OPFPush
{
	public class AndroidOPFPushHelper : IOPFPushHelper
	{
		AndroidJavaObject _pushHelper_object;

		public AndroidOPFPushHelper ()
		{
			var opfPush_class = new AndroidJavaClass ("org.onepf.opfpush.OPFPush");
			_pushHelper_object = opfPush_class.CallStatic<AndroidJavaObject> ("getHelper");
		}

		public void Register ()
		{
			_pushHelper_object.Call ("register");

			var messageDeliveryController_class = new AndroidJavaClass("org.onepf.opfpush.unity.utils.MessagesDeliveryController");
			messageDeliveryController_class.CallStatic ("resendUndeliveredMessages");
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

		public bool IsRegistering ()
		{
			return _pushHelper_object.Call<bool> ("isRegistering");
		}
	}
}
#endif