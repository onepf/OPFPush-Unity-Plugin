using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OnePF.OPFPush
{
	public static class OPFPush
	{
		public delegate void MessageDelegate (string providerName,Dictionary<string, string> data);

		public delegate void MessagesDeletedDelegate (string providerName,int messagesCount);

		public delegate void RegisteredDelegate (string providerName,string registrationId);

		public delegate void UnregisteredDelegate (string providerName,string oldRegistrationId);

		public delegate void NoAvailableProviderDelegate (Dictionary<string, PushError> pushErrors);

		public static event MessageDelegate OnMessage;
		public static event MessagesDeletedDelegate OnDeletedMessages;
		public static event RegisteredDelegate OnRegistered;
		public static event UnregisteredDelegate OnUnregistered;
		public static event NoAvailableProviderDelegate OnNoAvailableProvider;

		static IOPFPushHelper _pushHelper = null;
		static EventReceiver _eventReceiver = null;

		static OPFPush ()
		{
#if UNITY_ANDROID
			_pushHelper = new OPFPushHelper_Android ();
#else
			Debug.LogError ("OPFPush is currently not supported on this platform. Sorry.");
			return;
#endif
		}

		public static IOPFPushHelper GetHelper ()
		{
			if (_eventReceiver == null) {
				initEventReceiver ();
			}
			return _pushHelper;
		}

		private static void initEventReceiver ()
		{
			_eventReceiver = new GameObject ("OPFPush").AddComponent<EventReceiver> ();

			_eventReceiver.OnMessageAction += delegate(string providerName, Dictionary<string, string> data) {
				if (OnMessage != null)
					OnMessage (providerName, data);
			};

			_eventReceiver.OnDeletedMessageAction += delegate(string providerName, int messagesCount) {
				if (OnDeletedMessages != null)
					OnDeletedMessages (providerName, messagesCount);
			};

			_eventReceiver.OnRegisteredAction += delegate(string providerName, string registrationId) {
				if (OnRegistered != null)
					OnRegistered (providerName, registrationId);
			};

			_eventReceiver.OnUnregisteredAction += delegate(string providerName, string oldRegistrationId) {
				if (OnUnregistered != null)
					OnUnregistered (providerName, oldRegistrationId);
			};

			_eventReceiver.OnNoAvailableProviderActon += delegate(Dictionary<string, PushError> pushErrors) {
				if (OnNoAvailableProvider != null)
					OnNoAvailableProvider (pushErrors);
			};
		}
	}
}