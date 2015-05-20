using UnityEngine;
using System.Collections;
using OnePF.OPFPush;
using System;

public class OPFPushTest : MonoBehaviour
{
    void OnEnable()
    {
        OPFPush.OnMessage += OPFPush_OnMessage;
		OPFPush.OnDeletedMessages += OPFPush_OnDeletedMessages;
		OPFPush.OnRegistered += OPFPush_OnRegistered;
		OPFPush.OnUnregistered += OPFPush_OnUnregistered;
		OPFPush.OnNoAvailableProvider += OPFPush_OnNoAvailableProvider;
    }

    void OnDisable()
    {
		OPFPush.OnMessage -= OPFPush_OnMessage;
		OPFPush.OnDeletedMessages -= OPFPush_OnDeletedMessages;
		OPFPush.OnRegistered -= OPFPush_OnRegistered;
		OPFPush.OnUnregistered -= OPFPush_OnUnregistered;
		OPFPush.OnNoAvailableProvider -= OPFPush_OnNoAvailableProvider;
    }

    void Start()
    {
		OPFPush.Register ();
    }

	void OPFPush_OnMessage(string message)
	{
		Debug.Log (string.Format ("OPFPush. OnMessage: {0}", message));
	}

	void OPFPush_OnDeletedMessages(string messagesCount)
	{
		Debug.Log (string.Format ("OPFPush. OnDeletedMessages: {0}", messagesCount));
	}

	void OPFPush_OnRegistered(string registrationId)
	{
		Debug.Log (string.Format ("OPFPush. OnRegistered: {0}", registrationId));
	}

	void OPFPush_OnUnregistered(string oldRegistrationId)
	{
		Debug.Log (string.Format ("OPFPush. OnUnregistered: {0}", oldRegistrationId));
	}

	void OPFPush_OnNoAvailableProvider(string error)
	{
		Debug.Log (string.Format ("OPFPush. OnNoAvailableProvider: {0}", error));
	}
}