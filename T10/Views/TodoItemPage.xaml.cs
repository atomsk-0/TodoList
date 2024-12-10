using T10.Data;
using T10.Models;

namespace T10.Views;

public partial class TodoItemPage : ContentPage
{
	public TodoItemPage()
	{
		InitializeComponent();
	}

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)BindingContext;
        await TodoItemDatabase.Save(todoItem);
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)BindingContext;
        await TodoItemDatabase.Delete(todoItem);
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}