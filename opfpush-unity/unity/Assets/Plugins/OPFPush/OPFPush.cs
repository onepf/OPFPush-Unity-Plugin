using UnityEngine;
using System.Collections.Generic;

namespace OnePF.OPFPush
{

	/// <summary>
	/// The <c>IOPFPushHelper</c> instance holder.
	/// </summary>
	public static class OPFPush
	{
		public delegate void MessageDelegate (string providerName,Dictionary<string, string> data);

		public delegate void MessagesDeletedDelegate (string providerName,int messagesCount);

		public delegate void RegisteredDelegate (string providerName,string registrationId);

		public delegate void UnregisteredDelegate (string providerName,string oldRegistrationId);

		public delegate void NoAvailableProviderDelegate (Dictionary<string, PushError> pushErrors);

		/// <summary>
		/// Occurs when a new message has been received.
		/// </summary>
		public static event MessageDelegate OnMessage;

		/// <summary>
		/// Occurs when a notification about deleted messages has been received.
		/// Some providers don't send the count of deleted messages. In this case the message count will equal to min int value.
		/// </summary>
		public static event MessagesDeletedDelegate OnDeletedMessages;

		/// <summary>
		/// Occurs when a message about successful registration has been received.
		/// </summary>
		public static event RegisteredDelegate OnRegistered;

		/// <summary>
		/// Occurs when the <c>IOPFPushHelper</c> becomes <c>UNREGISTERED</c>.
		/// Calling this method doesn't mean that the provider is already unregistered. 
		/// The unregistering is performed in the background and is retried if an unregistration error is occurred.
		/// </summary>
		public static event UnregisteredDelegate OnUnregistered;

		/// <summary>
		/// Occurs when the <c>IOPFPushHelper</c> can't find any available provider for register push.
		/// You should notify the user that push notifications will not be received.
		/// 
		/// A push provider can be unavailable in two common reasons:
		/// <ol>
		/// <li>The push provider is unavailable on the specific device.</li>
		/// <li>An unrecoverable registration error has occurred.</li>
		/// </ol>
		/// In the first case an <c>PushError</c> object will be put to the <c>pushErrors</c> Dictionary if <c>AvailabilityErrorCode</c> is not null.
		/// In other words if a provider returned specific error code while an availability check, you will get the <c>AVAILABILITY_ERROR</c> type and
		/// the <c>AvailabilityErrorCode</c> will be not null.
		/// If a provider is no available because the host app (store) of the provider isn't installed, you will not get an error for the provider in the <c>pushErrors</c> Dictionary.
		/// 
		/// In the second case you can get the <c>PushError</c> object from the <c>pushErrors</c> Dictionary.
		/// You can notify the user about the occurred error if the error can be resolved by user.
		/// For example if you get the <c>AUTHENTICATION_FAILED</c> type for the GCM push provider, 
		/// you can ask the user whether he wants to add the google account.
		/// </summary>
		public static event NoAvailableProviderDelegate OnNoAvailableProvider;

		static IOPFPushHelper _pushHelper;
		static EventReceiver _eventReceiver;

		static OPFPush ()
		{

#if UNITY_ANDROID && !UNITY_EDITOR
			_pushHelper = new AndroidOPFPushHelper ();
#else
			_pushHelper = new StubOPFPushHelper ();
			Debug.Log ("OPFPush is currently not supported on this platform. Sorry.");
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

		static void initEventReceiver ()
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