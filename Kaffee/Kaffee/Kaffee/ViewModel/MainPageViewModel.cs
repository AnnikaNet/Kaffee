using CommunityToolkit.Mvvm.Input;
using KaffeeApp.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KaffeeApp.ViewModel
{
  public class MainPageViewModel : INotifyPropertyChanged
  {
    #region Properties

    private ObservableCollection<Kaffee> m_AlleKaffees;
    public ObservableCollection<Kaffee> AlleKaffees
    {
      get { return m_AlleKaffees; }
      set
      {
        m_AlleKaffees = value;
        OnPropertyChanged();
      }
    }

    private double m_ScreenWidth;
    public double ScreenWidth
    {
      get { return m_ScreenWidth; }
      set
      {
        m_ScreenWidth = value;
        OnPropertyChanged();
      }
    }

    private ObservableCollection<Person> m_Personen;
    public ObservableCollection<Person> Personen
    {
      get { return m_Personen; }
      set
      {
        m_Personen = value;
        OnPropertyChanged();
      }
    }

    private Person m_SelectedPerson;
    public Person SelectedPerson
    {
      get { return m_SelectedPerson; }
      set
      {
        m_SelectedPerson = value;
        OnPropertyChanged();
      }
    }


    #endregion Properties

    #region Konstruktor
    public MainPageViewModel()
    {
      ScreenWidth = ((DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density)) / 4;
      Personen = new ObservableCollection<Person>();

      AddPersonCommand = new RelayCommand(AddPerson);

      LoadCoffee();
      DoPersons();
    }
    #endregion Konstruktor

    #region Commands
    public ICommand ButtonPressedCommand { get; internal set; }
    private bool CanExecuteCommand()
    {
      return true;
    }

    public ICommand AddPersonCommand { get; internal set; }
    private async void AddPerson()
    {
      string result = await App.Current.MainPage.DisplayPromptAsync("Name eingeben", "Bitte Namen eingeben");
      if (Personen.FirstOrDefault(p => p.Name == result) != null)
      {
        await App.Current.MainPage.DisplayAlert("Dopplung", @"Es existiert schon eine Person mit dem Namen " + result + "", "OK");
      }
      else
      {
        Personen.Add(new Person() { Name = result });
        SelectedPerson = Personen.FirstOrDefault(p => p.Name == result);
      }
    }

    public void PersonTapped(object sender, ItemTappedEventArgs e)
    {
      if (e.Item is Person l_Person)
      {
        if (!l_Person.IsPresent)
        {
          AlleKaffees.FirstOrDefault(k => k.Name == l_Person.Bestellung.Name).Anzahl++;
          AlleKaffees.FirstOrDefault(k => k.Name == "Zucker").Anzahl = AlleKaffees.FirstOrDefault(k => k.Name == "Zucker").Anzahl + l_Person.SugarCount;
          l_Person.IsPresent = true;
        }
        else
        {
          AlleKaffees.FirstOrDefault(k => k.Name == ((Person)e.Item).Bestellung.Name).Anzahl--;
          AlleKaffees.FirstOrDefault(k => k.Name == "Zucker").Anzahl = AlleKaffees.FirstOrDefault(k => k.Name == "Zucker").Anzahl - l_Person.SugarCount;
          l_Person.IsPresent = false;
        }

      }
    }

    #endregion Commands

    #region Methoden

    #endregion Methoden

    #region Helper 

    private void LoadCoffee()
    {
      List<Kaffee> l_Kaffees = new List<Kaffee>()
      {
        new Model.Kaffee("Cappucino"),
        new Model.Kaffee("Milchkaffee"),
        new Model.Kaffee("schwarzer Kaffee"),
        new Model.Kaffee("Doppelter Espresso"),
        new Model.Kaffee("Espresso Macchiato"),
        new Model.Kaffee("Latte Macchiato"),
        new Model.Kaffee("Latte mit Schuss"),
        new Model.Kaffee("Eisschokolade"),
        new Model.Kaffee("Schokochino"),
        new Model.Kaffee("Kakao"),
        new Model.Kaffee("Tee"),
        new Model.Kaffee("Zucker")
      };

      AlleKaffees = new ObservableCollection<Kaffee>(l_Kaffees);
    }

    public void DoPersons()
    {
      List<Person> l_Personen = new List<Person>();

      l_Personen.Add(new Person("Anna", 2, AlleKaffees.FirstOrDefault(k => k.Name == "Cappucino")));
      l_Personen.Add(new Person("Annika", 0, AlleKaffees.FirstOrDefault(k => k.Name == "Latte mit Schuss")));
      l_Personen.Add(new Person("Daniel", 0, AlleKaffees.FirstOrDefault(k => k.Name == "schwarzer Kaffee")));
      l_Personen.Add(new Person("Kevin", 0, AlleKaffees.FirstOrDefault(k => k.Name == "Doppelter Espresso")));
      l_Personen.Add(new Person("Tim", 1, AlleKaffees.FirstOrDefault(k => k.Name == "Cappucino")));
      l_Personen.Add(new Person("Jakob", 1, AlleKaffees.FirstOrDefault(k => k.Name == "Eisschokolade")));
      l_Personen.Add(new Person("Sabine", 0, AlleKaffees.FirstOrDefault(k => k.Name == "Cappucino")));
      l_Personen.Add(new Person("Jan", 1, AlleKaffees.FirstOrDefault(k => k.Name == "Milchkaffee")));


      Personen = new ObservableCollection<Person>(l_Personen.OrderBy(p => p.Name));

    }

    #endregion Helper

    #region PropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(name));
      }
    }
    #endregion PropertyChanged
  }
}
