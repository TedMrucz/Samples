using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace Ecommittees.Model
{
    [Table("conversations")]
    public partial class Conversation : BindableBase, IEntity
	{
        public Conversation()
        {
            Members = new HashSet<Participant>();
            Messages = new HashSet<Message>();
			Participants = new HashSet<int>();
		}

		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }
		[JsonProperty(PropertyName = "document_id")]
		public int DocumentId { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "document_plain_sha_256")]
		public string DocumentPlainSha256 { get; set; }
		[JsonProperty(PropertyName = "owner_id")]
		public int OwnerId { get; set; }
		[JsonProperty(PropertyName = "document_reference_x")]
		public int DocumentReferenceX { get; set; }
		[JsonProperty(PropertyName = "document_reference_y")]
		public int DocumentReferenceY { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "visibility")]
		public string Visibility { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }
		[JsonProperty(PropertyName = "_deleted")]
		public bool Deleted { get; set; }

		[JsonIgnore, NotMapped]
		public virtual ICollection<int> Participants { get; set; }

		[JsonProperty(PropertyName = "messages")]
		public virtual ICollection<Message> Messages { get; set; }

		[JsonIgnore]
		public virtual ICollection<Participant> Members { get; set; }
		public void UpdateVisualMessageCollection()
		{
			ViewMessages = new ObservableCollection<Model.Message>(Messages);
            MessagesCount = ViewMessages.Count;
            ParticipantsCount = Participants.Count;
        }

		private ObservableCollection<Message> viewMessages = new ObservableCollection<Message>();
		[JsonIgnore]
		public ObservableCollection<Message> ViewMessages
		{
			get { return viewMessages; }
			set { SetProperty(ref viewMessages, value); }
		}
		[JsonIgnore]
		public Person Owner { get; set; }

        private string message;
        [JsonIgnore]
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private int messagesCount = 0;
        [JsonIgnore]
        public int MessagesCount
        {
            get { return messagesCount; }
            set { SetProperty(ref messagesCount, value); }
        }

        private int participantsCount = 0;
        [JsonIgnore]
        public int ParticipantsCount
        {
            get { return participantsCount; }
            set { SetProperty(ref participantsCount, value); }
        }

        private bool isMessageTab = false;
		[JsonIgnore]
		public bool IsMessageTab
		{
			get { return isMessageTab; }
			set
			{
				if (value)
					IsMemberTab = false;
				SetProperty(ref isMessageTab, value);
			}
		}

		private bool isMemberTab = false;
		[JsonIgnore]
		public bool IsMemberTab
		{
			get { return isMemberTab; }
			set
			{
				if (value)
					IsMessageTab = false;
				SetProperty(ref isMemberTab, value);
			}
		}
	}
}
