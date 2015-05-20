using UnityEngine;
using System.Collections;

namespace OnePF.OPFPush
{
    public static class OPFPush
    {
		public delegate void MessageDelegate(string message);
		public delegate void MessagesDeletedDelegate(string messagesCount);
		public delegate void RegisteredDelegate(string registrationId);
		public delegate void UnregisteredDelegate(string oldRegistrationId);
		public delegate void NoAvailableProviderDelegate(string error);

		public static event MessageDelegate OnMessage;
		public static event MessagesDeletedDelegate OnDeletedMessages;
		public static event RegisteredDelegate OnRegistered;
		public static event UnregisteredDelegate OnUnregistered;
		public static event NoAvailableProviderDelegate OnNoAvailableProvider;

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

		public static void Register()
		{
			initEventReceiver ();
			_push.Register ();
		}

		public static void Unregister()
		{
			initEventReceiver ();
			_push.Unregister ();
		}

	    private static void initEventReceiver()
		{
			if (_eventReceiver == null) 
			{
				_eventReceiver = new GameObject("OPFPush").AddComponent<EventReceiver>();

				_eventReceiver.OnMessageAction += delegate(string message)
				{
					if (OnMessage != null)
						OnMessage(message);
				};

				_eventReceiver.OnDeletedMessageAction += delegate(string messagesCount)
				{
					if (OnDeletedMessages != null)
						OnDeletedMessages(messagesCount);
				};

				_eventReceiver.OnRegisteredAction += delegate(string registrationId)
				{
					if (OnRegistered != null)
					{				
						OnRegistered(registrationId);
					}
				};

				_eventReceiver.OnUnregisteredAction += delegate(string oldRegistrationId)
				{
					if (OnUnregistered != null)
						OnUnregistered(oldRegistrationId);
				};

				_eventReceiver.OnNoAvailableProviderActon += delegate(string error)
				{
					if (OnNoAvailableProvider != null)
						OnNoAvailableProvider(error);
				};
			}
		}
    }
}