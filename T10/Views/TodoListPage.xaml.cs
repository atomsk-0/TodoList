using T10.Data;
using T10.Models;

namespace T10.Views;

public partial class TodoListPage : ContentPage
{
	public TodoListPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		listView.ItemsSource = await TodoItemDatabase.GetItemsAsync();
    }

    private async void OnItemAdded(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new TodoItemPage { BindingContext = new TodoItem() });
    }

	private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem != null)
		{
			await Navigation.PushAsync(new TodoItemPage { BindingContext = e.SelectedItem as TodoItem });
		}
	}
}