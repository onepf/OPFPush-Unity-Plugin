namespace OnePF.OPFPush
{
	public class OnDeletedMessagesData
	{
		public string ProviderName { get; set; }

		public int MessagesCount { get; set; }

		public OnDeletedMessagesData(string providerName, int messagesCount) {
			this.ProviderName = providerName;
			this.MessagesCount = messagesCount;
		}
	}
}
