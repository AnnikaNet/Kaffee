using KaffeeApp.Model;
using KaffeeApp.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KaffeeApp
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class KaffeePage : ContentPage
  {
    public KaffeePage()
    {
      InitializeComponent();
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      ((MainPageViewModel)this.BindingContext).PersonTapped(sender, e);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      List<Person> Previous = new List<Person>();
      List<Person> Current = new List<Person>();
      foreach (var item in e.PreviousSelection)
      {
        if (item is Person per)
          Previous.Add(per);
      }

      foreach (var item in e.CurrentSelection)
      {
        if (item is Person per)
          Current.Add(per);
      }

      if (e.PreviousSelection.Count() > e.CurrentSelection.Count()) //An Item was unselected
      {
        if (this.BindingContext is MainPageViewModel mp)
          mp.PersonDeleted(Previous.Except(Current).ToList().FirstOrDefault());
      }
      else // An Item was selected
      {
        if (this.BindingContext is MainPageViewModel mp)
          mp.PersonAdded(Current.Except(Previous).ToList().FirstOrDefault());
      }
    }
  }
}