using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace OnePF.OPFPush
{
    // TODO: create only for Android
    public class EventReceiver : MonoBehaviour
    {
		public event Action<string, Dictionary<string, string> > OnMessageAction;
		public event Action<string, int> OnDeletedMessageAction;
        public event Action<string, string> OnRegisteredAction;
		public event Action<string, string> OnUnregisteredAction;
		public event Action<Dictionary<string, PushError> > OnNoAvailableProviderActon;

        #region Native event handlers

		void OnMessage(string messageJson)
		{
			if (OnMessageAction != null)
			{
				Debug.Log("OnMessage json : " + messageJson);
				//TODO change to var
				OnMessageData data = JsonConvert.DeserializeObject<OnMessageData>(messageJson);
				OnMessageAction (data.ProviderName, data.Data);
			}
	    }

		void OnDeletedMessages(string deletedMessagesJson)
		{
			if (OnDeletedMessageAction != null)
			{
				Debug.Log("OnDeletedMessages json : " + deletedMessagesJson);
				OnDeletedMessagesData data = JsonConvert.DeserializeObject<OnDeletedMessagesData>(deletedMessagesJson);
				OnDeletedMessageAction(data.ProviderName, data.MessagesCount);
			}
		}

		void OnRegistered(string registeredJson)
		{
			if (OnRegisteredAction != null) 
			{
				Debug.Log("OnRegistered json : " + registeredJson);
				OnRegisteredData data = JsonConvert.DeserializeObject<OnRegisteredData>(registeredJson);
				OnRegisteredAction(data.ProviderName, data.RegistrationId);
			}
		}

		void OnUnregistered(string unregisteredJson)
		{
			if (OnUnregisteredAction != null)
			{
				Debug.Log("OnUnregistered json : " + unregisteredJson);
				OnUnregisteredData data = JsonConvert.DeserializeObject<OnUnregisteredData>(unregisteredJson);
				OnUnregisteredAction(data.ProviderName, data.OldRegistrationId);
			}
		}

		void OnNoAvailableProvider(string noAvailableProviderJson)
		{
			if (OnNoAvailableProviderActon != null)
			{
				Debug.Log("OnNoAvailableProvider json : " + noAvailableProviderJson);
				OnNoAvailableProviderData data = JsonConvert.DeserializeObject<OnNoAvailableProviderData>(noAvailableProviderJson);
				OnNoAvailableProviderActon(data.PushErrors);
			}
		}

        #endregion
    }
}