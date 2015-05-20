#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using System;

namespace OnePF.OPFPush
{
    public class OPFPush_Android : IOPFPush
    {
        public void Register() 
		{
			IntPtr unityHelper_class = AndroidJNI.FindClass("org/onepf/opfpush/unity/UnityHelper");
			IntPtr unityHelper_register = AndroidJNI.GetStaticMethodID(unityHelper_class, "register", "()V");
			AndroidJNI.CallStaticVoidMethod(unityHelper_class, unityHelper_register, new jvalue[0]);
		}

		public void Unregister()
		{
			IntPtr unityHelper_class = AndroidJNI.FindClass("org/onepf/opfpush/unity/UnityHelper");
			IntPtr unityHelper_register = AndroidJNI.GetStaticMethodID(unityHelper_class, "unregister", "()V");
			AndroidJNI.CallStaticVoidMethod(unityHelper_class, unityHelper_register, new jvalue[0]);
		}
    }
}
#endif