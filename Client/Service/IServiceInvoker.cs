using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ecommittees.Client.Service
{
	public interface IServiceInvoker
	{
		Task<T> Get<T>(object parameters = null, [CallerMemberName] string action = null);
		Task<int> Post<T>(T @object, object parameters = null, [CallerMemberName] string action = null);
	}
}