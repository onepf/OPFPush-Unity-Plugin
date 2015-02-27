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

        OPFPush.Init(options);
    }

    void OpenPush_InitFinished(bool success, string message)
    {
        Debug.Log(string.Format("OPFPush. Init Finished: {0}; {1}", success, message));
    }
}