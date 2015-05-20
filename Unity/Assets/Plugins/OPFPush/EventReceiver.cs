using UnityEngine;
using System.Collections;
using System;

namespace OnePF.OPFPush
{
    // TODO: create only for Android
    public class EventReceiver : MonoBehaviour
    {
		public event Action<string> OnMessageAction;
		public event Action<string> OnDeletedMessageAction;
        public event Action<string> OnRegisteredAction;
		public event Action<string> OnUnregisteredAction;
		public event Action<string> OnNoAvailableProviderActon;

        #region Native event handlers

		void OnMessage(string message)
		{
			//TODO: get json with provider name and message, parse it and send event
			if (OnMessageAction != null)
				OnMessageAction (message);
	    }

		void OnDeletedMessages(string messagesCount)
		{
			//TODO: get json with provider name and messageCount, parse it and send event
			if (OnDeletedMessageAction != null)
				OnDeletedMessageAction(messagesCount);
		}

		void OnRegistered(string registrationId)
		{
			//TODO: get json with provider name and registration id, parse it and send event 
			if (OnRegisteredAction != null)
				OnRegisteredAction(registrationId);
		}

		void OnUnregistered(string oldRegistrationId)
		{
			//TODO: get json with provider name and registration id, parse it and send event
			if (OnUnregisteredAction != null)
				OnUnregisteredAction(oldRegistrationId);
		}

		void OnNoAvailableProvider(string errorMessage)
		{
			//TODO: get json and parse it.
			if (OnNoAvailableProviderActon != null)
				OnNoAvailableProviderActon(errorMessage);
		}

        #endregion
    }
}