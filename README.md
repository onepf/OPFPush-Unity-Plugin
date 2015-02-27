OPFPush Unity Plugin
====================

About
-----
Currently OPFPush is a library that wraps Google Cloud Messaging, Nokia Notification Push, Android Device Messaging and has possibility to integrate new push service. Unity plugin adds support for the Windows Phone 8 and iOS platforms.

Usage
-----
This is full example of plugin usage. 
```
using UnityEngine;
using System.Collections;
using OnePF.OPFPush;

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
        Options options = new Options();
        options.AddProvider(new GCMProvider("539088697591"));
        options.AddProvider(new ADMProvider());
        options.AddProvider(new NokiaProvider(new string[] { "one", "two" }));

        options.LogEnabled = true;
        options.SelectSystemPreferred = true;

        OPFPush.Init(options);
    }

    void OpenPush_InitFinished(bool success, string message)
    {
        Debug.Log(string.Format("OPFPush. Init Finished: {0}; {1}", success, message));
    }
}
```

There is only one method to call - ```Init```. Then ```InitFinished``` event is fired when setup is finished.
