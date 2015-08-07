namespace OnePF.OPFPush
{
	public class OnUnregisteredData
	{
		public string ProviderName { get; set; }
			
		public string OldRegistrationId { get; set; }

		public OnUnregisteredData(string providerName, string oldRegistrationId) {
			this.ProviderName = providerName;
			this.OldRegistrationId = oldRegistrationId;
		}
	}
}
