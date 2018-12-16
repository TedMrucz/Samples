using System.Linq;
using System.Composition;
using System.Collections.Generic;
using Ecommittees.DataContext;
using Ecommittees.Model;

namespace Ecommittees.Services
{
	[Export]
	public class EcommitteeService
	{
		private readonly IDataContext dataContext;

		[ImportingConstructor]
		public EcommitteeService(IDataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public IList<Committee> GetCommittees()
		{
			return this.dataContext.Committees.ToList();
		}

		public IList<Meeting> GetMeetings()
		{
			return this.dataContext.Meetings.ToList();
		}

		public IList<Conversation> GetConversations()
		{
			return this.dataContext.Conversations.ToList();
		}

		public IList<Conversation> GetConversations(int documentId)
		{
			return this.dataContext.Conversations.Where(p => p.DocumentId == documentId).ToList();
		}
	}
}
