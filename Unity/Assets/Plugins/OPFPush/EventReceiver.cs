using UnityEngine;
using System.Collections;
using System;

namespace OnePF.OPFPush
{
    // TODO: create only for Android
    public class EventReceiver : MonoBehaviour
    {
        public event Action<string> InitSucceded;
        public event Action<string> InitFailed;

        #region Native event handlers

        void OnInitSucceeded(string registrationID)
        {
            if (InitSucceded != null)
                InitSucceded(registrationID);
        }

        void OnInitFailed(string error)
        {
            if (InitFailed != null)
                InitFailed(error);
        }

        #endregion
    }
}