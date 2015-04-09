#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using System;

namespace OnePF.OPFPush
{
    public class OPFPush_Android : IOPFPush
    {
        public void Init(Options options)
        {
            if (Application.platform != RuntimePlatform.Android)
                return;

            AndroidJNI.AttachCurrentThread();

            // context
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com/unity3d/player/UnityPlayer");
            IntPtr context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity").GetRawObject();

            if (options.LogEnabled)
            {
                // OPFPushLog
                IntPtr log_class = AndroidJNI.FindClass("org/onepf/opfutils/OPFLog");

                // OPFPushLog.setLogEnable
                IntPtr log_setEnabled = AndroidJNI.GetStaticMethodID(log_class, "setEnabled", "(ZZ)V");
                jvalue[] log_args = new jvalue[2];
                log_args[0].z = true; //TODO get from BuildConfig.DEBUG
				log_args[1].z = true;
                AndroidJNI.CallStaticVoidMethod(log_class, log_setEnabled, log_args);
            }

            // Configuration.Builder
            IntPtr configBuilder_class = AndroidJNI.FindClass("org/onepf/opfpush/configuration/Configuration$Builder");
            IntPtr configBuilder_constructor = AndroidJNI.GetMethodID(configBuilder_class, "<init>", "()V");
            IntPtr configBuilder = AndroidJNI.NewObject(configBuilder_class, configBuilder_constructor, new jvalue[0]);

            //// Options
            // setSelectSystemPreferred
            IntPtr configBuilder_setSelectSystemPreferred = AndroidJNI.GetMethodID(configBuilder_class, "setSelectSystemPreferred", "(Z)Lorg/onepf/opfpush/configuration/Configuration$Builder;");
            jvalue[] option_args = new jvalue[1];
            option_args[0].z = options.SelectSystemPreferred;
            AndroidJNI.CallObjectMethod(configBuilder, configBuilder_setSelectSystemPreferred, option_args);

            if (options.PushProviders.Count > 0)
            {
                // Array of push providers
                IntPtr array_class = AndroidJNI.FindClass("org/onepf/opfpush/pushprovider/PushProvider");
                IntPtr nullProvider = new IntPtr();
                IntPtr providersArray = AndroidJNI.NewObjectArray(options.PushProviders.Count, array_class, nullProvider);

                for (int i = 0; i < options.PushProviders.Count; ++i)
                {
                    var pushProvider = options.PushProviders[i];
                    IntPtr provider = IntPtr.Zero;
                    
                    // GCM
                    if (pushProvider.GetType() == typeof(GCMProvider))
                    {
                        GCMProvider gcmProvider = pushProvider as GCMProvider;

                        IntPtr gcm_class = AndroidJNI.FindClass("org/onepf/opfpush/gcm/GCMProvider");
                        IntPtr gcm_constructor = AndroidJNI.GetMethodID(gcm_class, "<init>", "(Landroid/content/Context;Ljava/lang/String;)V");
                        jvalue[] gcm_args = new jvalue[2];
                        gcm_args[0].l = context;
                        gcm_args[1].l = AndroidJNI.NewStringUTF(gcmProvider.SenderID);
                        provider = AndroidJNI.NewObject(gcm_class, gcm_constructor, gcm_args);
                    }
                    // ADM
                    else if (pushProvider.GetType() == typeof(ADMProvider))
                    {
                        IntPtr adm_class = AndroidJNI.FindClass("org/onepf/opfpush/adm/ADMProvider");
                        IntPtr adm_constructor = AndroidJNI.GetMethodID(adm_class, "<init>", "(Landroid/content/Context;)V");
                        jvalue[] adm_args = new jvalue[1];
                        adm_args[0].l = context;
                        provider = AndroidJNI.NewObject(adm_class, adm_constructor, adm_args);
                    }
                    // Nokia
                    else if (pushProvider.GetType() == typeof(NokiaProvider))
                    {
                        NokiaProvider nokiaProvider = pushProvider as NokiaProvider;

                        IntPtr nokia_class = AndroidJNI.FindClass("org/onepf/opfpush/nokia/NokiaNotificationsProvider");
                        IntPtr nokia_constructor = AndroidJNI.GetMethodID(nokia_class, "<init>", "(Landroid/content/Context;[Ljava/lang/String;)V");

                        IntPtr string_class = AndroidJNI.FindClass("java/lang/String");
                        IntPtr senderArray = AndroidJNI.NewObjectArray(nokiaProvider.SenderIDs.Count, string_class, AndroidJNI.NewStringUTF(""));

                        jvalue[] nokia_args = new jvalue[2];
                        nokia_args[0].l = context;
                        nokia_args[1].l = senderArray;
                        provider = AndroidJNI.NewObject(nokia_class, nokia_constructor, nokia_args);
                    }
                    
                    if (provider == IntPtr.Zero)
                        Debug.LogWarning("Unsupported provider type: " + pushProvider.GetType());
                    else
                        AndroidJNI.SetObjectArrayElement(providersArray, i, provider);
                }

                // Configuration.Builder.addProviders
                IntPtr configBuilder_addProviders = AndroidJNI.GetMethodID(configBuilder_class, "addProviders", "([Lorg/onepf/opfpush/pushprovider/PushProvider;)Lorg/onepf/opfpush/configuration/Configuration$Builder;");
                jvalue[] addProviders_args = new jvalue[options.PushProviders.Count];
                addProviders_args[0].l = providersArray;
                AndroidJNI.CallObjectMethod(configBuilder, configBuilder_addProviders, addProviders_args);
            }

            /*
            // TODO: add listener or leave receiver and remove completely
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

            IntPtr unityHelper_class = AndroidJNI.FindClass("org/onepf/opfpush/unity/UnityHelper");
            IntPtr unityHelper_init = AndroidJNI.GetStaticMethodID(unityHelper_class, "init", "(Landroid/content/Context;Lorg/onepf/opfpush/configuration/Configuration;)V");
            jvalue[] init_args = new jvalue[2];
            init_args[0].l = context;
            init_args[1].l = config;
            AndroidJNI.CallStaticVoidMethod(unityHelper_class, unityHelper_init, init_args);

            IntPtr unityHelper_register = AndroidJNI.GetStaticMethodID(unityHelper_class, "register", "()V");
            AndroidJNI.CallStaticVoidMethod(unityHelper_class, unityHelper_register, new jvalue[0]);
        }
    }
}
#endif