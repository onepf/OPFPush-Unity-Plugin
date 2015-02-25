using System.Collections.Generic;
using System;

namespace OnePF.OPFPush.WP8
{
    public class PushClient
    {
        public static event Action<bool, string> InitFinished;

        public static void Init()
        {
            if (InitFinished != null)
                InitFinished(true, "");
        }
    }
}

