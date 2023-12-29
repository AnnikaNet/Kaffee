using CommunityToolkit.Mvvm.Input;
using KaffeeApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
      SaveCommand = new RelayCommand(Save);
      DeleteCommand = new RelayCommand(Delete);

      LoadCoffee();
      ReadPersons();
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
      }
      Personen = new ObservableCollection<Person>(Personen.OrderBy(p => p.Name));
      SelectedPerson = Personen.FirstOrDefault(p => p.Name == result);
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

    public ICommand SaveCommand { get; internal set; }

    private void Save()
    {
      SavePersons();
    }

    public ICommand DeleteCommand { get; internal set; }

    private void Delete()
    {
      if (SelectedPerson.IsPresent)
      {
        AlleKaffees.FirstOrDefault(k => k.Name == SelectedPerson.Bestellung.Name).Anzahl--;
        AlleKaffees.FirstOrDefault(k => k.Name == "Zucker").Anzahl = AlleKaffees.FirstOrDefault(k => k.Name == "Zucker").Anzahl - SelectedPerson.SugarCount;
      }
      Personen.Remove(SelectedPerson);
      SelectedPerson = new Person();
    }

    #endregion Commands

    #region Methoden

    public void PersonAdded(Person p_Added)
    {
      AlleKaffees.FirstOrDefault(k => k.Name == p_Added.Bestellung.Name).Anzahl++;
      AlleKaffees.FirstOrDefault(z => z.Name == "Zucker").Anzahl += p_Added.SugarCount;
      Personen.FirstOrDefault(p => p == p_Added).IsPresent = true;
    }

    public void PersonDeleted(Person p_Deleted)
    {
      AlleKaffees.FirstOrDefault(k => k.Name == p_Deleted.Bestellung.Name).Anzahl--;
      AlleKaffees.FirstOrDefault(z => z.Name == "Zucker").Anzahl -= p_Deleted.SugarCount;
      Personen.FirstOrDefault(p => p == p_Deleted).IsPresent = false;
    }

    public void ReadPersons()
    {
      string l_fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Personen.txt");
      if (File.Exists(l_fileName))
      {
        Personen = new ObservableCollection<Person>();
        string Text = File.ReadAllText(l_fileName);
        string[] lines = Text.Split('\n');
        foreach (string line in lines.Where(l => l != ""))
        {
          string[] values = line.Split(',');
          Person p = new Person();
          p.Name = values[0];
          p.Bestellung = AlleKaffees.FirstOrDefault(k => k.Name == values[1]);
          p.SugarCount = Convert.ToInt32(values[2]);
          Personen.Add(p);
        }
        Personen = new ObservableCollection<Person>(Personen.OrderBy(p => p.Name));
      }
    }

    public void SavePersons()
    {
      string filecontent = "";
      foreach (Person p in Personen)
      {
        filecontent = filecontent + p.Name + "," + p.Bestellung.Name + "," + p.SugarCount + "\n";
      }

      string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Personen.txt");
      File.WriteAllText(_fileName, filecontent);
    }

    #endregion Methoden

    #region Helper 

    private void LoadCoffee()
    {
      List<Kaffee> l_Kaffees = new List<Kaffee>()
      {
        new Model.Kaffee("Cappucino"),
        new Model.Kaffee("Milchkaffee"),
        new Model.Kaffee("schwarzer Kaffee","Schwarz"),
        new Model.Kaffee("Doppelter Espresso","Espresso"),
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
