using UnityEngine;
using System.Collections.Generic;
using System;
using SimpleJSON;

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
				var rawData = JSON.Parse (messageJson);			
				var dataDictionary = new Dictionary<string, string> ();
				foreach (var key in rawData["data"].AsObject.GetKeys ()) 
				{
					dataDictionary.Add (key.ToString(), rawData["data"][key.ToString()]);
				}

				var data = new OnMessageData (rawData["providerName"].Value, dataDictionary);
				OnMessageAction (data.ProviderName, data.Data);
			} else
				Debug.Log ("OPFPush. OnMessageAction == null");
		}

		void OnDeletedMessages (string deletedMessagesJson)
		{
			Debug.Log ("OPFPush. Receive OnDeletedMessages event. json : " + deletedMessagesJson);
			if (OnDeletedMessageAction != null) {
				var rawData = JSON.Parse (deletedMessagesJson);
				var data = new OnDeletedMessagesData (rawData["providerName"].Value, rawData["messagesCount"].AsInt);
				OnDeletedMessageAction (data.ProviderName, data.MessagesCount);
			} else
				Debug.Log ("OPFPush. OnDeletedMessageAction == null");
		}

		void OnRegistered (string registeredJson)
		{
			Debug.Log ("OPFPush. Receive OnRegistered event. json : " + registeredJson);
			if (OnRegisteredAction != null) {
				var rawData = JSON.Parse (registeredJson);
				var data = new OnRegisteredData (rawData["providerName"].Value, rawData["registrationId"].Value);
				OnRegisteredAction (data.ProviderName, data.RegistrationId);
			} else 
				Debug.Log ("OPFPush. OnRegisteredAction == null");
		}

		void OnUnregistered (string unregisteredJson)
		{
			Debug.Log ("OPFPush. Receive OnUnregistered event. json : " + unregisteredJson);
			if (OnUnregisteredAction != null) {
				var rawData = JSON.Parse (unregisteredJson);
				var data = new OnUnregisteredData (rawData["providerName"].Value, rawData["oldRegistrationId"].Value);
				OnUnregisteredAction (data.ProviderName, data.OldRegistrationId);
			} else
				Debug.Log ("OPFPush. OnUnregisteredAction == null");
		}

		void OnNoAvailableProvider (string noAvailableProviderJson)
		{
			Debug.Log ("OPFPush. Receive OnNoAvailableProvider event. json : " + noAvailableProviderJson);
			if (OnNoAvailableProviderActon != null) {
				var rawData = JSON.Parse (noAvailableProviderJson);				
				var dataDictionary = new Dictionary<string, PushError> ();
				var rawPushErrorsArray = rawData["pushErrors"];
				foreach (var key in rawPushErrorsArray.AsObject.GetKeys ()) 
				{
					var rawPushError = rawPushErrorsArray[key.ToString()];
					dataDictionary.Add (key.ToString(), new PushError(rawPushError["availabilityErrorCode"].Value, rawPushError["type"].Value, rawPushError["originalError"].Value));
				}
				OnNoAvailableProviderActon (dataDictionary);
			} else 
				Debug.Log ("OPFPush. OnNoAvailableProviderActon == null");
		}

        #endregion
	}
}