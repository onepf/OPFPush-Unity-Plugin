using UnityEngine;
using System.Collections.Generic;
using OnePF.OPFPush;
using System.Text;

public class OPFPushSample : MonoBehaviour
{
	const string UNREGISTERED_STATE = "State : Unregistered";
	const string REGISTRING_STATE = "State : Registring";
	const string NO_AVAILABLE_PROVIDER_STATE = "State : Unregistered. No available provider";
	
	const string REGISTER_BUTTON = "Register";
	const string REGISTERING_BUTTON = "Registering";
	const string UNREGISTER_BUTTON = "Unregister";
	
	string stateString;
	string buttonText;
	bool isButtonEnabled;
	bool isMessageWindowVisible;
	string messageText;
	
	GUIStyle stateTextStyle;
	GUIStyle buttonStyle;
	GUIStyle messageTextStyle = new GUIStyle ();
	
	Rect windowRect = new Rect (20, 0.5f * Screen.height - 0.25f * Screen.height, Screen.width - 40, 0.5f * Screen.height);
	
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
		Screen.orientation = ScreenOrientation.Portrait;
		initBackground ();
		
		OPFPush.GetHelper ().Register ();
		initState ();
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit (); 
		}
	}
	
	void OnGUI ()
	{
		initStateTextStyle ();
		initButtonStyle ();		
		
		GUI.Label (new Rect (20, 20, Screen.width - 40, Screen.height), stateString, stateTextStyle);
		
		GUI.enabled = isButtonEnabled;
		if (GUI.Button (new Rect (20, Screen.height - 0.1f * Screen.height - 40, Screen.width - 40, 0.1f * Screen.height), buttonText, buttonStyle)) {
			var helper = OPFPush.GetHelper ();
			if (helper.IsRegistered ()) {
				//OPFPush is registered, need to unregister
				isButtonEnabled = false;
				helper.Unregister ();
			} else if (!helper.IsRegistering ()) {
				//OPFPush is unregistered, need to register
				isButtonEnabled = false;
				buttonText = REGISTERING_BUTTON;
				helper.Register ();
			}
		}
		GUI.enabled = true;
		
		if (isMessageWindowVisible) {
			windowRect = GUI.Window (0, windowRect, DoMessageWindow, "Message");
		}
	}
	
	void DoMessageWindow (int windowID)
	{
		messageTextStyle.fontSize = (int)(0.03f * Screen.height);
		messageTextStyle.wordWrap = true;
		messageTextStyle.normal.textColor = Color.white;
		
		GUILayout.BeginVertical ();
		GUILayout.Label (messageText, messageTextStyle);
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("Close")) {
			print ("Got a click");
			isMessageWindowVisible = false;
		}
		GUILayout.EndHorizontal ();
	}
	
	void initBackground ()
	{
		var camera = GetComponent<Camera> ();
		camera.clearFlags = CameraClearFlags.SolidColor;
		camera.backgroundColor = Color.white;
	}
	
	void initState ()
	{
		var opfpushHelper = OPFPush.GetHelper ();
		if (opfpushHelper.IsRegistered ()) {
			stateString = getRegisteredStateString (opfpushHelper.GetProviderName (), opfpushHelper.GetRegistrationId ());
			buttonText = UNREGISTER_BUTTON;
			isButtonEnabled = true;
		} else if (opfpushHelper.IsRegistering ()) {
			stateString = REGISTRING_STATE;
			buttonText = REGISTERING_BUTTON;
			isButtonEnabled = false;
		} else {
			stateString = UNREGISTERED_STATE;
			buttonText = REGISTER_BUTTON;
			isButtonEnabled = true;
		}
	}
	
	void initStateTextStyle ()
	{
		if (stateTextStyle == null) {
			stateTextStyle = new GUIStyle ();
			stateTextStyle.fontSize = (int)(0.03f * Screen.height);
			stateTextStyle.wordWrap = true;
		}		
	}
	
	void initButtonStyle ()
	{
		if (buttonStyle == null) {
			buttonStyle = GUI.skin.GetStyle ("Button");
			buttonStyle.fontSize = (int)(0.03f * Screen.height);
		}
	}
	
	string getRegisteredStateString (string providerName, string registrationId)
	{
		return string.Format (
			"State : Registered. \n Provider name : \"{0}\"; \n Registration id : \"{1}\"",
			providerName,
			registrationId
		);
	}
	
	void OPFPush_OnMessage (string providerName, Dictionary<string, string> data)
	{
		var logBuilder = new StringBuilder (string.Format ("OPFPush. OnMessage: {0}", providerName));
		
		if (data != null) {
			foreach (KeyValuePair<string, string> entry in data) {
				logBuilder.Append (string.Format (" key : \"{0}\"; value : \"{1}\" ", entry.Key, entry.Value));
			}
		} else {
			logBuilder.Append (" message data == null");
		}
		
		
		Debug.Log (logBuilder.ToString ());
		
		messageText = data ["message"];
		isMessageWindowVisible = true;
	}
	
	void OPFPush_OnDeletedMessages (string providerName, int messagesCount)
	{
		Debug.Log (string.Format ("OPFPush. OnDeletedMessages(). provider : \"{0}\"; messagesCount : {1}", providerName, messagesCount));
	}
	
	void OPFPush_OnRegistered (string providerName, string registrationId)
	{
		Debug.Log (string.Format ("OPFPush. OnRegistered(). provider : \"{0}\"; regId : \"{1}\"", providerName, registrationId));
		
		stateString = getRegisteredStateString (providerName, registrationId);
		buttonText = UNREGISTER_BUTTON;
		isButtonEnabled = true;
	}
	
	void OPFPush_OnUnregistered (string providerName, string oldRegistrationId)
	{
		Debug.Log (string.Format ("OPFPush. OnUnregistered(). provider : \"{0}\"; regId : \"{1}\"", providerName, oldRegistrationId));
		
		stateString = UNREGISTERED_STATE;
		buttonText = REGISTER_BUTTON;
		isButtonEnabled = true;
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
		
		stateString = NO_AVAILABLE_PROVIDER_STATE;
		buttonText = REGISTER_BUTTON;
		isButtonEnabled = true;
	}
}

