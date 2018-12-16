using System.Threading.Tasks;
using Ecommittees.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Client
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
		}

		private async void OnConversationMessageSendTapped(object sender, TappedRoutedEventArgs e)
		{
			e.Handled = true;
			if (ConversationsListView?.SelectedItem is Conversation conversation
				&& DataContext is MainPageViewModel viewDataContext)
			{
				await viewDataContext.NewConversationMessageSend(conversation);
			}
		}

		private void OnConversationMessagesTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
		{
			if (ConversationsListView?.SelectedItem is Conversation selectedItem)
				selectedItem.IsMessageTab = !selectedItem.IsMessageTab;
		}

		private void OnConversationMembersTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
		{
			if (ConversationsListView?.SelectedItem is Conversation selectedItem)
				selectedItem.IsMemberTab = !selectedItem.IsMemberTab;
		}
	}
}
