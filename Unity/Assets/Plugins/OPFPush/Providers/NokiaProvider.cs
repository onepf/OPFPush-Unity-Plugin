using System.Collections.Generic;

namespace OnePF.Push
{
    public class NokiaProvider : IPushProvider
    {
        private List<string> _senderIDs = new List<string>();

        public List<string> SenderIDs { get { return _senderIDs; } }

        public NokiaProvider(string senderID)
        {
            _senderIDs.Add(senderID);
        }

        public NokiaProvider(string[] senderIDs)
        {
            _senderIDs.AddRange(senderIDs);
        }
    }
}