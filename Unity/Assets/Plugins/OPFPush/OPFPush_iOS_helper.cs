using UnityEngine;
using System;
using System.Collections;

namespace OnePF.OPFPush
{
    public class OPFPush_iOS_helper : MonoBehaviour
    {
#if UNITY_IOS
        public event OPFPush.InitFinishedDelegate InitFinished;
        string _token = null;
#endif

        public void Init()
        {
            Debug.Log("OpenPush_iOS_helper.Init");
            StartCoroutine(RequestToken());
        }

        IEnumerator RequestToken()
        {
#if UNITY_IOS
			NotificationServices.RegisterForRemoteNotificationTypes(RemoteNotificationType.Alert |
			                                                        RemoteNotificationType.Badge |
			                                                        RemoteNotificationType.Sound);
			while (true)
			{
				byte[] token = NotificationServices.deviceToken;
				if (token != null)
				{
					_token = System.BitConverter.ToString(token).Replace("-", "");
					Debug.Log("Token received: " + _token);
                    if (InitFinished != null)
                        InitFinished(true, _token);
					yield break;
				}
				yield return new WaitForFixedUpdate();
			}
#else
            yield return null;
#endif
        }
    }
}