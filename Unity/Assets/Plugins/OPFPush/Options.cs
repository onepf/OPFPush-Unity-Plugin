using System.Collections.Generic;

namespace OnePF.Push
{
    public class Options
    {
        List<IPushProvider> _pushProviders = new List<IPushProvider>();

        public bool RecoverProvider { get; set; }
        public bool SelectSystemPreferred { get; set; }

        public IEnumerable<IPushProvider> PushProviders { get { return _pushProviders; } }

        public void AddProvider(IPushProvider provider)
        {
            _pushProviders.Add(provider);
        }
    }
}