using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ecommittees.Client.Service;
using Ecommittees.Model;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace Client
{
	public class MainPageViewModel : BindableBase
	{
		private readonly int _PersonId = 1;
		private readonly int _DocumentId = 626;
		private string _PlainSha256;

		private readonly MembersService service;

		public IList<Conversation> Conversations { get; } = new ObservableCollection<Conversation>();

		public IList<Committee> Committees { get; set; } = new List<Committee>();

		public DelegateCommand LoadControlCommand { get; }
		public DelegateCommand NewConversationCommand { get; }

		public MainPageViewModel()
		{
			this.service = new MembersService();
			this.LoadControlCommand = new DelegateCommand(OnLoadControlCommand);
			this.NewConversationCommand = new DelegateCommand(OnNewConversationCommand);
		}

		private async Task LoadData()
		{
			this.Committees = await this.service.GetCommittees();
		}

		private async void OnLoadControlCommand()
		{
			var committees = await this.service.GetCommittees();
			var people = await this.service.GetPeople();
			var roles = await this.service.GetRoles();

			foreach(Person person in people)
				person.Name = person.FirstName + " " + person.LastName;

			committees.All(c => 
			{
				c.CommitteeMembers.All(m =>
				{
					m.Member = people.FirstOrDefault(p => p.Id == m.MemberId);
					m.Role = roles.FirstOrDefault(p => p.Id == m.RoleId);
					return true;
				});
				return true;
			});

			this.Committees = committees;

			var conversations = await this.service.GetConversationsByDocument(_DocumentId);

			if (conversations.Any())
				_PlainSha256 = conversations.FirstOrDefault().DocumentPlainSha256;

			this.Conversations.Clear();


			foreach (var conversation in this.Conversations)
			{
				this.Conversations.Add(conversation);
				conversation.Owner = people.FirstOrDefault(p => p.Id == conversation.OwnerId);
				conversation.UpdateVisualMessageCollection();
			}
		}

		public async Task NewConversationMessageSend(Conversation conversation)
		{
			int retValue = -1;
			var message = new Message();
			message.ConversationId = conversation.Id;
			message.Text = conversation.Message;
			//message.Conversation = conversation;
			message.CreatedAt = DateTime.Now;
			message.UpdatedAt = DateTime.Now;
			message.AuthorId = _PersonId;

			conversation.Messages.Add(message);
			conversation.ViewMessages.Add(message);

			try
			{
				retValue = await this.service.SaveMessage(message);
			}
			catch { }
		}

		public async void OnNewConversationCommand()
		{
			this.OnNewConversationSave();
		}

		public async Task OnNewConversationSave()
		{
			int retValue = -1;

			var conversation = new Conversation();
			conversation.Id = 0;
			conversation.DocumentId = _DocumentId;
			conversation.DocumentPlainSha256 = _PlainSha256;
			conversation.OwnerId = _PersonId;
			conversation.CreatedAt = DateTime.Now;
			conversation.UpdatedAt = DateTime.Now;

			conversation.DocumentReferenceX = 0;
			conversation.DocumentReferenceY = 0;
			conversation.UpdateVisualMessageCollection();

			this.Conversations.Add(conversation);

			try
			{
				retValue = await this.service.SaveConversation(conversation);
			}
			catch { }
		}
	}
}