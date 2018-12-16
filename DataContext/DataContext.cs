using System;
using System.Composition;
using System.Linq;
using Ecommittees.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommittees.DataContext
{
	[Export(typeof(IDataContext))]
    public partial class DataContext : DbContext, IDataContext
	{
        public DataContext()
        {
        }

		public IQueryable<AgendaItem> AgendaItems => Set<AgendaItem>().AsNoTracking();
        public IQueryable<AgendaVersion> AgendaVersions => Set<AgendaVersion>().AsNoTracking();
		public IQueryable<Agenda> Agendas => Set<Agenda>().AsNoTracking();
        public IQueryable<AttendanceType> AttendanceTypes => Set<AttendanceType>().AsNoTracking();
        public IQueryable<Attendance> Attendances => Set<Attendance>().AsNoTracking();
        public IQueryable<CommitteeMember> CommitteeMembers => Set<CommitteeMember>().Include(p => p.Role).Include(p => p.Member).AsNoTracking();
        public IQueryable<CommitteeType> CommitteeTypes => Set<CommitteeType>().AsNoTracking();
        public IQueryable<Committee> Committees => Set<Committee>().Include(p => p.CommitteeType).Include(p => p.CommitteeMembers).AsNoTracking();
        public IQueryable<Conversation> Conversations => Set<Conversation>().Include(p => p.Messages).AsNoTracking();   //.Include(p => p.Members)
		public IQueryable<Device> Devices => Set<Device>().AsNoTracking();
        public IQueryable<Document> Documents => Set<Document>().AsNoTracking();
        public IQueryable<HistoricalEvent> HistoricalEvents => Set<HistoricalEvent>().AsNoTracking();
        public IQueryable<ManifestEntry> ManifestEntries => Set<ManifestEntry>().AsNoTracking();
        public IQueryable<Manifest> Manifests => Set<Manifest>().AsNoTracking();
        public IQueryable<Meeting> Meetings => Set<Meeting>().AsNoTracking();
        public IQueryable<Message> Messages => Set<Message>().AsNoTracking();
        public IQueryable<Participant> Participants => Set<Participant>().AsNoTracking();
        public IQueryable<Person> People => Set<Person>().Include(p => p.CommitteeMembers).AsNoTracking();
        public IQueryable<RevokedMeeting> RevokedMeetings => Set<RevokedMeeting>().AsNoTracking();
        public IQueryable<RevokedMembership> RevokedMemberships => Set<RevokedMembership>().AsNoTracking();
        public IQueryable<Role> Roles => Set<Role>().Include(p => p.CommitteeMembers).AsNoTracking();
        public IQueryable<UserRole> UserRoles => Set<UserRole>().AsNoTracking();
        public IQueryable<User> Users => Set<User>().AsNoTracking();


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Ecommittee;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendaItem>().ToTable("agenda_items");
            modelBuilder.Entity<AgendaVersion>().ToTable("agenda_versions");
            modelBuilder.Entity<Agenda>().ToTable("agendas");
            modelBuilder.Entity<AttendanceType>().ToTable("attendance_types");
            modelBuilder.Entity<Attendance>().ToTable("attendances");
            modelBuilder.Entity<CommitteeMember>().ToTable("committee_members");
            modelBuilder.Entity<CommitteeType>().ToTable("committee_types");
            modelBuilder.Entity<Committee>().ToTable("committees");
            modelBuilder.Entity<Conversation>().ToTable("conversations");
            modelBuilder.Entity<Device>().ToTable("devices");
            modelBuilder.Entity<Document>().ToTable("documents");
            modelBuilder.Entity<HistoricalEvent>().ToTable("historical_events");
            modelBuilder.Entity<ManifestEntry>().ToTable("manifest_entries");
            modelBuilder.Entity<Meeting>().ToTable("meetings");
			modelBuilder.Entity<Manifest>().ToTable("manifests");
			modelBuilder.Entity<Message>().ToTable("messages").HasKey(p => p.Id);
            modelBuilder.Entity<Participant>().ToTable("participants");
            modelBuilder.Entity<Person>().ToTable("people");
            modelBuilder.Entity<RevokedMeeting>().ToTable("revoked_meetings");
            modelBuilder.Entity<RevokedMembership>().ToTable("revoked_memberships");
            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<UserRole>().ToTable("user_roles");
            modelBuilder.Entity<User>().ToTable("users");

			modelBuilder.Entity<Conversation>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Conversation>().Property(p => p.DocumentId).HasColumnName("document_id");
            modelBuilder.Entity<Conversation>().Property(p => p.OwnerId).HasColumnName("owner_id");
            modelBuilder.Entity<Conversation>().Property(p => p.DocumentPlainSha256).HasColumnName("document_plain_sha_256");
            modelBuilder.Entity<Conversation>().Property(p => p.DocumentReferenceX).HasColumnName("document_reference_x");
            modelBuilder.Entity<Conversation>().Property(p => p.DocumentReferenceY).HasColumnName("document_reference_y");
            modelBuilder.Entity<Conversation>().Property(p => p.Visibility).HasColumnName("visibility");
            modelBuilder.Entity<Conversation>().Property(p => p.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<Conversation>().Property(p => p.UpdatedAt).HasColumnName("updated_at");
			modelBuilder.Entity<Conversation>().HasMany(p => p.Messages).WithOne(p => p.Conversation).HasForeignKey(p => p.ConversationId);
			modelBuilder.Entity<Conversation>().Ignore(p => p.ViewMessages);
			modelBuilder.Entity<Conversation>().Ignore(p => p.IsMessageTab);
			modelBuilder.Entity<Conversation>().Ignore(p => p.IsMemberTab);
			modelBuilder.Entity<Conversation>().Ignore(p => p.Members);
			modelBuilder.Entity<Conversation>().Ignore(p => p.Message);
			modelBuilder.Entity<Conversation>().Ignore(p => p.Owner);
			modelBuilder.Entity<Conversation>().Ignore(p => p.MessagesCount);
			modelBuilder.Entity<Conversation>().Ignore(p => p.ParticipantsCount);
			modelBuilder.Entity<Conversation>().Ignore(p => p.Deleted);

			modelBuilder.Entity<Participant>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Participant>().Property(p => p.MemberId).HasColumnName("member_id").HasDefaultValue(1);
            modelBuilder.Entity<Participant>().Property(p => p.ConversationId).HasColumnName("conversation_id").HasDefaultValue(1);
            modelBuilder.Entity<Participant>().Property(p => p.DeletedAt).HasColumnName("deleted_at");

            modelBuilder.Entity<Message>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Message>().Property(p => p.ConversationId).HasColumnName("conversation_id").HasDefaultValue(1);
            modelBuilder.Entity<Message>().Property(p => p.Text).HasColumnName("text");
            modelBuilder.Entity<Message>().Property(p => p.AuthorId).HasColumnName("author_id").HasDefaultValue(1);
            modelBuilder.Entity<Message>().Property(p => p.Ancestry).HasColumnName("ancestry");
            modelBuilder.Entity<Message>().Property(p => p.Annotation).HasColumnName("annotation");
            modelBuilder.Entity<Message>().Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Message>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValue(DateTime.Now);
			modelBuilder.Entity<Message>().Ignore(p => p.OwnerName);


			modelBuilder.Entity<Person>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Person>().Property(p => p.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<Person>().Property(p => p.LastName).HasColumnName("last_name");
            modelBuilder.Entity<Person>().Property(p => p.Email).HasColumnName("email");
			modelBuilder.Entity<Person>().Property(p => p.PhoneNumber).HasColumnName("phone_number");
			modelBuilder.Entity<Person>().Property(p => p.Title).HasColumnName("title");
            modelBuilder.Entity<Person>().Property(p => p.Address).HasColumnName("address");
			modelBuilder.Entity<Person>().Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Person>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValue(DateTime.Now);
			modelBuilder.Entity<Person>().Ignore(p => p.IsSelected);
			modelBuilder.Entity<Person>().Ignore(p => p.Name);
			//modelBuilder.Entity<Person>().HasMany(p => p.CommitteeMembers).WithOne(p => p.Member).HasForeignKey(p => p.MemberId);

			modelBuilder.Entity<CommitteeMember>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<CommitteeMember>().Property(p => p.CommitteeId).HasColumnName("committee_id");
            modelBuilder.Entity<CommitteeMember>().Property(p => p.MemberId).HasColumnName("member_id");
            modelBuilder.Entity<CommitteeMember>().Property(p => p.RoleId).HasColumnName("role_id");
            modelBuilder.Entity<CommitteeMember>().Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<CommitteeMember>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValue(DateTime.Now);
			modelBuilder.Entity<CommitteeMember>().HasOne(p => p.Role).WithMany(p => p.CommitteeMembers).HasForeignKey(p => p.RoleId);
			modelBuilder.Entity<CommitteeMember>().HasOne(p => p.Member).WithMany(p => p.CommitteeMembers).HasForeignKey(p => p.MemberId);

			modelBuilder.Entity<Role>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Role>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<Role>().Property(p => p.Description).HasColumnName("description");
			//modelBuilder.Entity<Role>().HasMany(p => p.CommitteeMembers).WithOne(p => p.Role).HasForeignKey(p => p.RoleId);

			modelBuilder.Entity<CommitteeType>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<CommitteeType>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<CommitteeType>().Property(p => p.Description).HasColumnName("description");
            modelBuilder.Entity<CommitteeType>().Property(p => p.Position).HasColumnName("position");
            modelBuilder.Entity<CommitteeType>().Property(p => p.Color).HasColumnName("color");
            modelBuilder.Entity<CommitteeType>().Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<CommitteeType>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Committee>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Committee>().Property(p => p.Title).HasColumnName("title");
            modelBuilder.Entity<Committee>().Property(p => p.Description).HasColumnName("description");
            modelBuilder.Entity<Committee>().Property(p => p.CommitteeTypeId).HasColumnName("committee_type_id");
            modelBuilder.Entity<Committee>().Property(p => p.Descriptor).HasColumnName("descriptor");
			modelBuilder.Entity<Committee>().HasOne(p => p.CommitteeType).WithMany(p => p.Committies).HasForeignKey(p => p.CommitteeTypeId);
			modelBuilder.Entity<Committee>().HasMany(p => p.CommitteeMembers).WithOne(p => p.Committee).HasForeignKey(p => p.CommitteeId);
			//modelBuilder.Entity<Committee>().HasMany(p => p.Roles).WithOne(p => p.);

			modelBuilder.Entity<Document>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Document>().Property(p => p.Title).HasColumnName("title");
            modelBuilder.Entity<Document>().Property(p => p.EncryptedSha256).HasColumnName("encrypted_sha_256");
			modelBuilder.Entity<Document>().Property(p => p.PlainSha256).HasColumnName("plain_sha_256");
			modelBuilder.Entity<Document>().Property(p => p.ReplacedByDocumentId).HasColumnName("replaced_by_document_id");
            modelBuilder.Entity<Document>().Property(p => p.DeletedAt).HasColumnName("deleted_at");
            modelBuilder.Entity<Document>().Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Document>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValue(DateTime.Now);

			modelBuilder.Entity<Manifest>().Property(p => p.Id).HasColumnName("id");
			modelBuilder.Entity<Manifest>().Property(p => p.DeviceId).HasColumnName("device_id");
			modelBuilder.Entity<Manifest>().Property(p => p.AgendaId).HasColumnName("agenda_version_id");
			modelBuilder.Entity<Manifest>().Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValue(DateTime.Now);
			modelBuilder.Entity<Manifest>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValue(DateTime.Now);

			modelBuilder.Entity<ManifestEntry>().Property(p => p.Id).HasColumnName("id");
			modelBuilder.Entity<ManifestEntry>().Property(p => p.ManifestEntryToken).HasColumnName("manifest_entry_token");
			modelBuilder.Entity<ManifestEntry>().Property(p => p.PlainSha256).HasColumnName("document_plain_sha_256");
			modelBuilder.Entity<ManifestEntry>().Property(p => p.EncryptedSha256).HasColumnName("document_encrypted_sha_256");

			modelBuilder.Entity<Agenda>().Property(p => p.Id).HasColumnName("id");
			modelBuilder.Entity<Agenda>().Property(p => p.MeetingId).HasColumnName("meeting_id");
			modelBuilder.Entity<Agenda>().Property(p => p.CreatedAt).HasColumnName("created_at");
			modelBuilder.Entity<Agenda>().Property(p => p.UpdatedAt).HasColumnName("updated_at");
			modelBuilder.Entity<Agenda>().Property(p => p.DeletedAt).HasColumnName("deleted_at");

			modelBuilder.Entity<AgendaItem>().Property(p => p.Id).HasColumnName("id");
			modelBuilder.Entity<AgendaItem>().Property(p => p.AgendaId).HasColumnName("agenda_id");
			modelBuilder.Entity<AgendaItem>().Property(p => p.ReferenceDate).HasColumnName("reference_date");
			modelBuilder.Entity<AgendaItem>().Property(p => p.Presenter).HasColumnName("presenter");
			modelBuilder.Entity<AgendaItem>().Property(p => p.Name).HasColumnName("name");
			modelBuilder.Entity<AgendaItem>().Property(p => p.CreatedAt).HasColumnName("created_at");
			modelBuilder.Entity<AgendaItem>().Property(p => p.UpdatedAt).HasColumnName("updated_at");
			modelBuilder.Entity<AgendaItem>().Property(p => p.DeletedAt).HasColumnName("deleted_at");
			modelBuilder.Entity<AgendaItem>().Ignore(p => p.AgendaDocuments);

			modelBuilder.Entity<Attendance>().Property(p => p.Id).HasColumnName("id");
			modelBuilder.Entity<Attendance>().Property(p => p.MeetingId).HasColumnName("meeting_id");
			modelBuilder.Entity<Attendance>().Property(p => p.PersonId).HasColumnName("person_id");

			modelBuilder.Entity<AttendanceType>().Property(p => p.Id).HasColumnName("id");
			modelBuilder.Entity<AttendanceType>().Property(p => p.Name).HasColumnName("name");
			modelBuilder.Entity<AttendanceType>().Property(p => p.Description).HasColumnName("description");

			modelBuilder.Entity<Meeting>().Property(p => p.Id).HasColumnName("id");
			modelBuilder.Entity<Meeting>().Property(p => p.CommitteeId).HasColumnName("committee_id");
			modelBuilder.Entity<Meeting>().Property(p => p.Location).HasColumnName("location");
			modelBuilder.Entity<Meeting>().Property(p => p.Title).HasColumnName("title");
			modelBuilder.Entity<Meeting>().Property(p => p.Description).HasColumnName("description");
			modelBuilder.Entity<Meeting>().Property(p => p.StartsAt).HasColumnName("starts_at");
			modelBuilder.Entity<Meeting>().Property(p => p.EndsAt).HasColumnName("ends_at");
			modelBuilder.Entity<Meeting>().Property(p => p.ExpiresAt).HasColumnName("expires_at");
			modelBuilder.Entity<Meeting>().Property(p => p.StartsAtWasChanged).HasColumnName("starts_at_was_changed");
			modelBuilder.Entity<Meeting>().Property(p => p.EndsAtWasChanged).HasColumnName("ends_at_was_changed");
			modelBuilder.Entity<Meeting>().Property(p => p.LocationWasChanged).HasColumnName("location_was_changed");
			modelBuilder.Entity<Meeting>().Property(p => p.DeletedAt).HasColumnName("deleted_at");
			modelBuilder.Entity<Meeting>().Property(p => p.CreatedAt).HasColumnName("created_at");
			modelBuilder.Entity<Meeting>().Property(p => p.UpdatedAt).HasColumnName("updated_at");
			modelBuilder.Entity<Meeting>().Ignore(p => p.Deleted);
			modelBuilder.Entity<Meeting>().Ignore(p => p.Version);

			base.OnModelCreating(modelBuilder);
        }

		public void Entity<T>(T entity) where T : class, IEntity
		{
			if (entity.Id == 0)
				Entry(entity).State = EntityState.Added;
			else
				Entry(entity).State = EntityState.Modified;
		}
	}
}
