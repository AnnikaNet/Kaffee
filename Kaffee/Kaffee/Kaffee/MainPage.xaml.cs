using Kaffee.Model;
using Kaffee.ViewModel;
﻿using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Kaffee
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      //  if (((Person)e.Item).IsPresent)
      //  {
      //    ((Person)e.Item).IsPresent = false;
      //  }
      //  else
      //  {
      //    ((Person)e.Item).IsPresent = true;

      //  }
    }
    private void ViewCell_Tapped(object sender, EventArgs e)
    {
      var selp = (Person)((ListView)((ViewCell)sender).Parent).SelectedItem;
      var vc = (ViewCell)sender;
      if (vc.View != null)
      {
        if (selp.IsPresent)
        {
          selp.IsPresent = false;
          vc.View.BackgroundColor = Color.Transparent;
          ((MainPageViewModel)this.BindingContext).SubtractKaffee(selp.Name);
        }
        else
        {
          selp.IsPresent = true;
          vc.View.BackgroundColor = Color.Blue;
          ((MainPageViewModel)this.BindingContext).AddKaffee(selp.Name);
        }
      }
    }
  }
}
