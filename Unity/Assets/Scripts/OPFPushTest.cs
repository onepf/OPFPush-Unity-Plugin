using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OnePF.OPFPush;
using System;
using System.Text;

public class OPFPushTest : MonoBehaviour
{
	void OnEnable ()
	{
		OPFPush.OnMessage += OPFPush_OnMessage;
		OPFPush.OnDeletedMessages += OPFPush_OnDeletedMessages;
		OPFPush.OnRegistered += OPFPush_OnRegistered;
		OPFPush.OnUnregistered += OPFPush_OnUnregistered;
		OPFPush.OnNoAvailableProvider += OPFPush_OnNoAvailableProvider;
	}

	void OnDisable ()
	{
		OPFPush.OnMessage -= OPFPush_OnMessage;
		OPFPush.OnDeletedMessages -= OPFPush_OnDeletedMessages;
		OPFPush.OnRegistered -= OPFPush_OnRegistered;
		OPFPush.OnUnregistered -= OPFPush_OnUnregistered;
		OPFPush.OnNoAvailableProvider -= OPFPush_OnNoAvailableProvider;
	}

	void Start ()
	{
		OPFPush.GetHelper ().Register ();
	}

	void OPFPush_OnMessage (string providerName, Dictionary<string, string> data)
	{
		StringBuilder logBuilder = new StringBuilder (string.Format ("OPFPush. OnMessage: {0}", providerName));

		if (data != null) {
			foreach (KeyValuePair<string, string> entry in data) {
				logBuilder.Append (string.Format (" key : \"{0}\"; value : \"{1}\" ", entry.Key, entry.Value));
			}
		} else {
			logBuilder.Append (" message data == null");
		}


		Debug.Log (logBuilder.ToString ());
	}

	void OPFPush_OnDeletedMessages (string providerName, int messagesCount)
	{
		Debug.Log (string.Format ("OPFPush. OnDeletedMessages(). provider : \"{0}\"; messagesCount : {1}", providerName, messagesCount));
	}

	void OPFPush_OnRegistered (string providerName, string registrationId)
	{
		Debug.Log (string.Format ("OPFPush. OnRegistered(). provider : \"{0}\"; regId : \"{1}\"", providerName, registrationId));

		Debug.Log (string.Format ("OPFPush. Provider name : \"{0}\"", OPFPush.GetHelper ().GetProviderName ()));
		Debug.Log (string.Format ("OPFPush. Registration id : \"{0}\"", OPFPush.GetHelper ().GetRegistrationId ()));
		Debug.Log (string.Format ("OPFPush. isRegistered : \"{0}\"", OPFPush.GetHelper ().IsRegistered ()));
		Debug.Log (string.Format ("OPFPush. isRegistering : \"{0}\"", OPFPush.GetHelper ().isRegistering ()));
	}

	void OPFPush_OnUnregistered (string providerName, string oldRegistrationId)
	{
		Debug.Log (string.Format ("OPFPush. OnUnregistered(). provider : \"{0}\"; regId : \"{1}\"", providerName, oldRegistrationId));
	}

	void OPFPush_OnNoAvailableProvider (Dictionary<string, PushError> pushErrors)
	{
		var logBuilder = new StringBuilder ("OPFPush. OnNoAvailableProvider()");

		foreach (KeyValuePair<string, PushError> pushError in pushErrors) {
			logBuilder.Append (string.Format (" providerName : {0}", pushError.Key))
				.Append (string.Format (
					" availabilityErrorCode : \"{0}\"; type : \"{1}\"; original error : \"{2}\"", 
					pushError.Value.AvailabilityErrorCode,
					pushError.Value.Type,
					pushError.Value.OriginalError
			));
		}

		Debug.Log (logBuilder.ToString ());
	}
}