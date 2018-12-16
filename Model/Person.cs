namespace Ecommittees.Model
{
    using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Newtonsoft.Json;
    using Prism.Mvvm;

	[Table("people")]
    public partial class Person : BindableBase
    {
		public Person()
		{
			CommitteeMembers = new HashSet<CommitteeMember>();
		}
		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "first_name")]
		public string FirstName { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "last_name")]
		public string LastName { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "phone_number")]
		public string PhoneNumber { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "address")]
		public string Address { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        private bool isSelected = false;
        [JsonIgnore]
		public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
		private string name;
		[JsonIgnore]
		public string Name
		{
			get { return name; }
			set { SetProperty(ref name, value); }
		}
		[JsonIgnore]
		public virtual ICollection<CommitteeMember> CommitteeMembers { get; set; }

	}
}
