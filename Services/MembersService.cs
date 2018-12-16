using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Ecommittees.DataContext;
using Ecommittees.Model;

namespace Ecommittees.Services
{
	[Export]
	public class MembersService
	{
		private readonly IDataContext dataContext;

		[ImportingConstructor]
		public MembersService(IDataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public IList<Conversation> conversations()  //"conversations"  GetConversations
		{
			return this.dataContext.Conversations.ToList();
		}

		public IList<Committee> committees()  //"committees"  GetCommittees
		{
			return this.dataContext.Committees.Where(p => !String.IsNullOrEmpty(p.Descriptor)).ToList();
		}

		public IList<DocumentEntry> documents()  //"documents"  GetDocuments
		{
			var docs = new List<DocumentEntry>();
			return docs;
		}

		public IList<Meeting> meetings() //"meetings"  GetMeetings
		{
			return this.dataContext.Meetings.ToList();
		}

		public async Task<Message> messages(Message message)  //"messages"  SaveMessage
		{
			this.dataContext.Entity(message);
			message.Id = await this.dataContext.SaveChangesAsync();
			return message;
		}

		//public async Task<Conversation> conversations(Conversation conversation)  //"conversations"  SaveConversation
		//{
		//	this.dataContext.Entity(conversation);
		//	conversation.Id = await this.dataContext.SaveChangesAsync();
		//	return conversation;
		//}

		public string devices(string channelUri)  //"devices"  UpdateChannelUri
		{
			return @"{ 'id':1265,'device_uuid':'AwAkUgMAtF8FABBlBgABAAQAdAAEALwmBAB6SQEASvsCAGDtCQBksw==','device_type':'tablet','auth_token':'po-DlGi_ktA4xzvbx_SiGQcXC5a7bTXIdHZ0m-H8BIs','auth_uuid':'f4fe6d62-906f-4fd8-8747-cd2818d06ff4','created_at':'2018-01-18T13:21:14.28-05:00','updated_at':'2018-01-18T13:21:14.28-05:00','public_key':'MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDHzJpYsdXiIj431a7AEDelsYm7Fg5hIYYdMJiGxgsZ7189RbL3ACVJ55BLRsd1iW6EIYUqnOVKPzjeUr0O0CIVVwbXjV0XdDHnwwyB6zgsE4uE4kgDmo1mTyxsLAVomiqxKxf2WVlX66OTbU2WjltYJpYDaMnG511tsUy8IiUY9QIDAQAB','person':{ 'id':25,'title':'','first_name':'Test','last_name':'Account (R&P)','email':'','phone_number':'','address':'','created_at':'2017-06-19T17:40:24.746-04:00','updated_at':'2017-06-19T17:40:24.746-04:00'} }";
		}

		public string manifest_entry(string HttpStringContent)  //"devices"  UpdateChannelUri
		{
			return HttpStringContent;
		}

		////////////////////////////////////////////////////////////////////////////////////////
		public IList<CommitteeMember> GetCommitteeMembers()
		{
			return this.dataContext.CommitteeMembers.ToList();
		}

		public IList<Person> GetPeople()
		{
			return this.dataContext.People.ToList();
		}

		public IList<Role> GetRoles()
		{
			return this.dataContext.Roles.ToList();
		}


		public IList<Conversation> GetConversationsByDocument(int documentId)
		{
			return this.dataContext.Conversations.Where(p => p.DocumentId == documentId).ToList();
		}
	}
}
