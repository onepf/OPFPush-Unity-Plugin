using System.Collections.Generic;

namespace OnePF.OPFPush
{
	public class OnMessageData
	{
		public string ProviderName { get; set; }

		public Dictionary<string, string> Data { get; set; }

		public OnMessageData(string providerName, Dictionary<string, string> data) {
			this.ProviderName = providerName;
			this.Data = data;
		}
	}	
}
