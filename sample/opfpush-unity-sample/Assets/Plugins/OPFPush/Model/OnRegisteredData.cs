namespace OnePF.OPFPush
{
	public class OnRegisteredData
	{
		public string ProviderName { get; set; }

		public string RegistrationId { get; set; }

		public OnRegisteredData(string providerName, string registrationId) {
			this.ProviderName = providerName;
			this.RegistrationId = registrationId;
		}
	}
}
