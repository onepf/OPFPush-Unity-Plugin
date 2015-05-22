using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace OnePF.OPFPush
{
	public class EventReceiver : MonoBehaviour
	{
		public event Action<string, Dictionary<string, string> > OnMessageAction;
		public event Action<string, int> OnDeletedMessageAction;
		public event Action<string, string> OnRegisteredAction;
		public event Action<string, string> OnUnregisteredAction;
		public event Action<Dictionary<string, PushError> > OnNoAvailableProviderActon;

        #region Native event handlers

		void OnMessage (string messageJson)
		{
			Debug.Log ("OPFPush. Receive OnMessage event. json : " + messageJson);
			if (OnMessageAction != null) {
				var data = JsonConvert.DeserializeObject<OnMessageData> (messageJson);
				OnMessageAction (data.ProviderName, data.Data);
			} else
				Debug.Log ("OPFPush. OnMessageAction == null");
		}

		void OnDeletedMessages (string deletedMessagesJson)
		{
			Debug.Log ("OPFPush. Receive OnDeletedMessages event. json : " + deletedMessagesJson);
			if (OnDeletedMessageAction != null) {
				var data = JsonConvert.DeserializeObject<OnDeletedMessagesData> (deletedMessagesJson);
				OnDeletedMessageAction (data.ProviderName, data.MessagesCount);
			} else
				Debug.Log ("OPFPush. OnDeletedMessageAction == null");
		}

		void OnRegistered (string registeredJson)
		{
			Debug.Log ("OPFPush. Receive OnRegistered event. json : " + registeredJson);
			if (OnRegisteredAction != null) {
				var data = JsonConvert.DeserializeObject<OnRegisteredData> (registeredJson);
				OnRegisteredAction (data.ProviderName, data.RegistrationId);
			} else 
				Debug.Log ("OPFPush. OnRegisteredAction == null");
		}

		void OnUnregistered (string unregisteredJson)
		{
			Debug.Log ("OPFPush. Receive OnUnregistered event. json : " + unregisteredJson);
			if (OnUnregisteredAction != null) {
				var data = JsonConvert.DeserializeObject<OnUnregisteredData> (unregisteredJson);
				OnUnregisteredAction (data.ProviderName, data.OldRegistrationId);
			} else
				Debug.Log ("OPFPush. OnUnregisteredAction == null");
		}

		void OnNoAvailableProvider (string noAvailableProviderJson)
		{
			Debug.Log ("OPFPush. Receive OnNoAvailableProvider event. json : " + noAvailableProviderJson);
			if (OnNoAvailableProviderActon != null) {
				var data = JsonConvert.DeserializeObject<OnNoAvailableProviderData> (noAvailableProviderJson);
				OnNoAvailableProviderActon (data.PushErrors);
			} else 
				Debug.Log ("OPFPush. OnNoAvailableProviderActon == null");
		}

        #endregion
	}
}