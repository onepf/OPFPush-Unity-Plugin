using System.Collections.Generic;

namespace OnePF.OPFPush
{
    public class Options
    {
        List<IPushProvider> _pushProviders = new List<IPushProvider>();

        public bool LogEnabled { get; set; }
        public bool SelectSystemPreferred { get; set; }

        public List<IPushProvider> PushProviders { get { return _pushProviders; } }

        public void AddProvider(IPushProvider provider)
        {
            _pushProviders.Add(provider);
        }
    }
}