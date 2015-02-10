using UnityEngine;
using System.Collections;
using System;

namespace OnePF.Push
{
    public class OPFPush_Android : MonoBehaviour
    {
        const string GCM_SENDER_ID = "539088697591";

        void Awake()
        {
            if (Application.platform != RuntimePlatform.Android)
                return;

            AndroidJNI.AttachCurrentThread();

            // context
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com/unity3d/player/UnityPlayer");
            IntPtr context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity").GetRawObject();

            // OPFPushLog
            IntPtr log_class = AndroidJNI.FindClass("org/onepf/opfpush/OPFPushLog");

            // OPFPushLog.setLogEnable
            IntPtr log_setLogEnable = AndroidJNI.GetStaticMethodID(log_class, "setLogEnable", "(Z)V");
            jvalue[] log_args = new jvalue[1];
            log_args[0].z = true;
            AndroidJNI.CallStaticVoidMethod(log_class, log_setLogEnable, log_args);

            // Configuration.Builder
            IntPtr configBuilder_class = AndroidJNI.FindClass("org/onepf/opfpush/configuration/Configuration$Builder");
            IntPtr configBuilder_constructor = AndroidJNI.GetMethodID(configBuilder_class, "<init>", "()V");
            IntPtr configBuilder = AndroidJNI.NewObject(configBuilder_class, configBuilder_constructor, new jvalue[0]);

            // GCM
            IntPtr gcm_class = AndroidJNI.FindClass("org/onepf/opfpush/gcm/GCMProvider");
            IntPtr gcm_constructor = AndroidJNI.GetMethodID(gcm_class, "<init>", "(Landroid/content/Context;Ljava/lang/String;)V");
            jvalue[] gcm_args = new jvalue[2];
            gcm_args[0].l = context;
            gcm_args[1].l = AndroidJNI.NewStringUTF(GCM_SENDER_ID);
            IntPtr gcm = AndroidJNI.NewObject(gcm_class, gcm_constructor, gcm_args);

            // Configuration.Builder.addProviders
            IntPtr configBuilder_addProviders = AndroidJNI.GetMethodID(configBuilder_class, "addProviders", "([Lorg/onepf/opfpush/PushProvider;)Lorg/onepf/opfpush/configuration/Configuration$Builder;");
            jvalue[] addProviders_args = new jvalue[1];

            // Array of push providers
            IntPtr array_class = AndroidJNI.FindClass("org/onepf/opfpush/PushProvider");
            IntPtr nullProvider = new IntPtr();
            IntPtr providersArray = AndroidJNI.NewObjectArray(1, array_class, nullProvider);
            AndroidJNI.SetObjectArrayElement(providersArray, 0, gcm);

            addProviders_args[0].l = providersArray;
            AndroidJNI.CallObjectMethod(configBuilder, configBuilder_addProviders, addProviders_args);

            /*
            // UnityEventListener
            IntPtr eventListener_class = AndroidJNI.FindClass("org/onepf/opfpush/unity/listener/UnityEventListener");
            IntPtr eventListener_constructor = AndroidJNI.GetMethodID(eventListener_class, "<init>", "(Landroid/content/Context;)V");
            jvalue[] eventListener_constructor_args = new jvalue[1];
            eventListener_constructor_args[0].l = context;
            IntPtr eventListener = AndroidJNI.NewObject(eventListener_class, eventListener_constructor, eventListener_constructor_args);

            // configBuilder.setEventListener
            IntPtr configBuilder_setEventListener = AndroidJNI.GetMethodID(configBuilder_class, "setEventListener", "(Lorg/onepf/opfpush/listener/EventListener;)Lorg/onepf/opfpush/configuration/Configuration$Builder;");
            jvalue[] listener_args = new jvalue[1];
            listener_args[0].l = eventListener;
            AndroidJNI.CallObjectMethod(configBuilder, configBuilder_setEventListener, listener_args);
             */ 
            
            // configBuilder.build
            IntPtr configBuilder_build = AndroidJNI.GetMethodID(configBuilder_class, "build", "()Lorg/onepf/opfpush/configuration/Configuration;");
            IntPtr config = AndroidJNI.CallObjectMethod(configBuilder, configBuilder_build, new jvalue[0]);

            /*
            // OPFPush
            IntPtr opfpush_class = AndroidJNI.FindClass("org/onepf/opfpush/OPFPush");
            
            // OPFPush.init
            IntPtr opfpush_init = AndroidJNI.GetStaticMethodID(opfpush_class, "init", "(Landroid/content/Context;Lorg/onepf/opfpush/configuration/Configuration;)V");
            jvalue[] opfpush_args = new jvalue[2];
            opfpush_args[0].l = context;
            opfpush_args[1].l = config;
            AndroidJNI.CallStaticVoidMethod(opfpush_class, opfpush_init, opfpush_args);

            // OPFPush.getHelper
            IntPtr opfpush_getHelper = AndroidJNI.GetStaticMethodID(opfpush_class, "getHelper", "()Lorg/onepf/opfpush/OPFPushHelper;");
            IntPtr opfpush_helper = AndroidJNI.CallStaticObjectMethod(opfpush_class, opfpush_getHelper, new jvalue[0]);

            // OPFPushHelper.register
            IntPtr helper_class = AndroidJNI.FindClass("org/onepf/opfpush/OPFPushHelper");
            IntPtr helper_register = AndroidJNI.GetMethodID(helper_class, "register", "()V");
            AndroidJNI.CallVoidMethod(opfpush_helper, helper_register, new jvalue[0]);
            */

            IntPtr unityHelper_class = AndroidJNI.FindClass("org/onepf/opfpush/unity/UnityHelper");
            IntPtr unityHelper_init = AndroidJNI.GetStaticMethodID(unityHelper_class, "init", "(Landroid/content/Context;Lorg/onepf/opfpush/configuration/Configuration;)V");
            jvalue[] init_args = new jvalue[2];
            init_args[0].l = context;
            init_args[1].l = config;
            AndroidJNI.CallStaticVoidMethod(unityHelper_class, unityHelper_init, init_args);
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, Screen.width / 4, Screen.height / 8), "Register"))
            {
                IntPtr unityHelper_class = AndroidJNI.FindClass("org/onepf/opfpush/unity/UnityHelper");
                IntPtr unityHelper_register = AndroidJNI.GetStaticMethodID(unityHelper_class, "register", "()V");
                AndroidJNI.CallStaticVoidMethod(unityHelper_class, unityHelper_register, new jvalue[0]);
            }
        }
    }
}