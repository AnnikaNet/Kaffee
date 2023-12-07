using KaffeeApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}