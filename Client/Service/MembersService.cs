using System.Linq;
using System.Composition;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommittees.Model;

namespace Ecommittees.Client.Service
{
	public class MembersService
	{
		public readonly IServiceInvoker service;

		public MembersService()
		{
			this.service = new ServiceInvoker("http://localhost:8080/api/MembersService/");
		}

		public Task<int> Count() => this.service.Get<int>(null, "Count");

		public Task<IList<Committee>> GetCommittees() => this.service.Get<IList<Committee>>(null, "GetCommittees");

		public Task<IList<CommitteeMember>> GetCommitteeMembers() => this.service.Get<IList<CommitteeMember>>(null, "GetCommitteeMembers");

		public Task<IList<Person>> GetPeople() => this.service.Get<IList<Person>>(null, "GetPeople");

		public Task<IList<Role>> GetRoles() => this.service.Get<IList<Role>>(null, "GetRoles");

		public Task<IList<Meeting>> GetMeetings() => this.service.Get<IList<Meeting>>(null, "GetMeetings");

		public Task<IList<Conversation>> GetConversations() => this.service.Get<IList<Conversation>>(null, "GetConversations");

		public Task<IList<Conversation>> GetConversationsByDocument(int documentId) => this.service.Get<IList<Conversation>>(new { documentId = documentId }, "GetConversationsByDocument");

		public Task<int> SaveMessage(Message message) => this.service.Post(message);

		public Task<int> SaveConversation(Conversation conversation) => this.service.Post(conversation);
	}
}
