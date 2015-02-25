#if UNITY_WP8
namespace OnePF.OPFPush
{
    public class OPFPush_WP8 : IOPFPush
    {
        public event OPFPush.InitFinishedDelegate InitFinished;

        public void Init(Options p)
        {
            WP8.PushClient.InitFinished += PushClient_InitFinished;
            WP8.PushClient.Init();
        }

        void PushClient_InitFinished(bool success, string errorMessage)
        {
            if (InitFinished != null)
                InitFinished(success, errorMessage);
        }
    }
}
#endif