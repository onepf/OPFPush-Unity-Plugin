OPFPush Unity Plugin
====================

About
-----
Unity plugin for [OPFPush](https://github.com/onepf/OPFPush) library.
Currently it supports only android push messages.
[opfpush-all.unitypackage](http://todo) contains all supported push providers (GCM, ADM and Nokia Push)
[opfpush-gcm-adm.unitypackage](http://todo) doesn't contain Nokia Push provider.

The latest unity packages avaliable [here](http://todo)

How to use
-----
1) You have to add all permissions, receivers and services from AndroidManifest.xml of imported unity package. Also you must declare `org.onepf.opfpush.unity.OPFPushApplication` or it's inheritor in your AndroidManifest.xml's `<application>` tag. 

2) To configure library add opfpush_config.json file to the `<project-root>/Assets/Plugins/Android/assets` directory. There is config for the example app in the opfpush unity package. 
Config file must contains following properties: 
* `logEnabled`: set `true` value if you want to get logs (warning / info / errors) from the library, `false` otherwise.
* `debug`: set `true` value if you want to get debug logs from the library, `false` otherwise.
* `providers`: an array of providers. A provider object have to contains following properties:
    *  `name`: one of the following values: ("GCM" / "ADM" / "Nokia")
    *  `senderId` (`senderIdsArray`): string value (or array of strings) which will be used as senderId(s) for registration.
*  `selectSystemPreferred`: If you set `true`, the system push provider gets the highest priority. For Google devices this is Google Cloud Messaging, for Kindle devices - ADM.
    
3) In your script which connected with MainCamera object add methods for OPFPush delegates:

```
using UnityEngine;
using System.Collections;
using OnePF.OPFPush;

public class MyMainCameraScript : MonoBehaviour
{
    void OnEnable()
    {
        OPFPush.OnMessage += OnMessageHandler;
		OPFPush.OnDeletedMessages += OnDeletedMessagesHandler;
		OPFPush.OnRegistered += OnRegisteredHandler;
		OPFPush.OnUnregistered += OnUnregisteredHandler;
		OPFPush.OnNoAvailableProvider += OnNoAvailableProviderHandler;
    }

    void OnDisable()
    {
        OPFPush.OnMessage -= OnMessageHandler;
		OPFPush.OnDeletedMessages -= OnDeletedMessagesHandler;
		OPFPush.OnRegistered -= OnRegisteredHandler;
		OPFPush.OnUnregistered -= OnUnregisteredHandler;
		OPFPush.OnNoAvailableProvider -= OnNoAvailableProviderHandler;
    }

    void OnMessageHandler (string providerName, Dictionary<string, string> data)
    {
        //Your code
    }
    
    void OnDeletedMessagesHandler (string providerName, int messagesCount)
    {
        //Your code
    }
    
    void OnRegisteredHandler (string providerName, string registrationId)
    {
        //Your code
    }
    
    void OnUnregisteredHandler (string providerName, string oldRegistrationId)
    {
        //Your code
    }
    
    void OnNoAvailableProviderHandler (Dictionary<string, PushError> pushErrors)
    {
        //Your code
    }
    
    //...
}
```

In the `Start()` method of the same script call `OPFPush.GetHelper ().Register ()`.

```
    //...
	void Start ()
	{
		OPFPush.GetHelper ().Register ();
	}
    //...
```

Known restrictions
-----

Push messages are delivered to the unity OPFPush delegates only if unity engine is started (an unity activity is resumed). In another case received messages are saved and delivered to unity after starting unity engine. If you want to handle push messages immediatly you should extend the `org.onepf.opfpush.unity.listener.PushEventListener` java class and handle messages in the inheritor. 

But keep in mind that OPFPush has notifications payload support out of the box. To show a notification after push receiving just add  the `opf_notification` field to the sent data and the library will do it. For more information see [Notification payload support](https://github.com/onepf/OPFPush/wiki/Notification-payload-support) section.
