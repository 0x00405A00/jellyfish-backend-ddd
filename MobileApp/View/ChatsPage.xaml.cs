namespace MobileApp.View;

public partial class ChatsPage : ContentView
{
	public ChatsPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Fixes the CollectionView rerendering
    /// </summary>
    internal void RefreshChats()
    {
        Chats.ScrollTo(0, 0);
    }
}