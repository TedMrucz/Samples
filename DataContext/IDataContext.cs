using Ecommittees.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Ecommittees.DataContext
{
	public interface IDataContext
    {
		IQueryable<AgendaItem> AgendaItems { get; }
		IQueryable<AgendaVersion> AgendaVersions { get; }
		IQueryable<Agenda> Agendas { get; }
		IQueryable<AttendanceType> AttendanceTypes { get; }
		IQueryable<Attendance> Attendances { get; }
		IQueryable<CommitteeMember> CommitteeMembers { get; }
		IQueryable<CommitteeType> CommitteeTypes { get; }
		IQueryable<Committee> Committees { get; }
		IQueryable<Conversation> Conversations { get; }
		IQueryable<Device> Devices { get; }
		IQueryable<Document> Documents { get; }
		IQueryable<HistoricalEvent> HistoricalEvents { get; }
		IQueryable<ManifestEntry> ManifestEntries { get; }
		IQueryable<Manifest> Manifests { get; }
		IQueryable<Meeting> Meetings { get; }
		IQueryable<Message> Messages { get; }
		IQueryable<Participant> Participants { get; }
		IQueryable<Person> People { get; }
		IQueryable<RevokedMeeting> RevokedMeetings { get; }
		IQueryable<RevokedMembership> RevokedMemberships { get; }
		IQueryable<Role> Roles { get; }
		IQueryable<UserRole> UserRoles { get; }
		IQueryable<User> Users { get; }

		void Entity<T>(T entity) where T : class, IEntity;

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
