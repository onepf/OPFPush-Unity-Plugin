using UnityEngine;
using System.Collections;
using OnePF.OPFPush;
using System;

public class OPFPushTest : MonoBehaviour
{
    void OnEnable()
    {
        OPFPush.InitFinished += OpenPush_InitFinished;
    }

    void OnDisable()
    {
        OPFPush.InitFinished -= OpenPush_InitFinished;
    }

    void Start()
    {
		//TODO move to OPFPush class
		IntPtr unityHelper_class = AndroidJNI.FindClass("org/onepf/opfpush/unity/UnityHelper");
		IntPtr unityHelper_register = AndroidJNI.GetStaticMethodID(unityHelper_class, "register", "()V");
		AndroidJNI.CallStaticVoidMethod(unityHelper_class, unityHelper_register, new jvalue[0]);
    }

    void OpenPush_InitFinished(bool success, string message)
    {
        Debug.Log(string.Format("OPFPush. Init Finished: {0}; {1}", success, message));
    }
}