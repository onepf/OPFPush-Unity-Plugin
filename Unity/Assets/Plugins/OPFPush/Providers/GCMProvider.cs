namespace OnePF.Push
{
    public class GCMProvider : IPushProvider
    {
        public string SenderID { get; private set; }

        public GCMProvider(string senderID)
        {
            SenderID = senderID;
        }
    }
}