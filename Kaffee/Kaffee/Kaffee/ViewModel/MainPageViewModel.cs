using CommunityToolkit.Mvvm.Input;
using Kaffee.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Kaffee.ViewModel
{
  public class MainPageViewModel : INotifyPropertyChanged
  {
    #region Properties

    private int m_Cappucinos;
    public int Cappucinos
    {
      get { return m_Cappucinos; }
      set
      {
        m_Cappucinos = value;
        OnPropertyChanged();
      }
    }

    private int m_Milchkaffees;
    public int Milchkaffees
    {
      get { return m_Milchkaffees; }
      set
      {
        m_Milchkaffees = value;
        OnPropertyChanged();
      }
    }

    public int m_KaffeesSchwarz;
    public int KaffeesSchwarz
    {
      get { return m_KaffeesSchwarz; }
      set
      {
        m_KaffeesSchwarz = value;
        OnPropertyChanged();
      }
    }

    private int m_DoppelteEspressi;
    public int DoppelteEspressi
    {
      get { return m_DoppelteEspressi; }
      set
      {
        m_DoppelteEspressi = value;
        OnPropertyChanged();
      }
    }

    private int m_EspressoMacchiatos;
    public int EspressoMacchiatos
    {
      get { return m_EspressoMacchiatos; }
      set
      {
        m_EspressoMacchiatos = value;
        OnPropertyChanged();
      }
    }
    private int m_LatteMacchiatos;
    public int LatteMacchiatos
    {
      get { return m_LatteMacchiatos; }
      set
      {
        m_LatteMacchiatos = value;
        OnPropertyChanged();
      }
    }

    private int m_LattesMitSchuss;
    public int LattesMitSchuss
    {
      get { return m_LattesMitSchuss; }
      set
      {
        m_LattesMitSchuss = value;
        OnPropertyChanged();
      }
    }

    public int m_Zucker;
    public int Zucker
    {
      get { return m_Zucker; }
      set
      {
        m_Zucker = value;
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


    #endregion Properties

    #region Konstruktor
    public MainPageViewModel()
    {
      ButtonPressedCommand = new RelayCommand<string>(ButtonPressed);

      Cappucinos = 0;
      Milchkaffees = 0;
      KaffeesSchwarz = 0;
      DoppelteEspressi = 0;
      EspressoMacchiatos = 0;
      LatteMacchiatos = 0;
      LattesMitSchuss = 0;
      Zucker = 0;
      ScreenWidth = ((DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density)) / 4;
      Personen = new ObservableCollection<Person>();
      DoPersons();
    }
    #endregion Konstruktor


    #region Commands
    public ICommand ButtonPressedCommand { get; internal set; }
    private bool CanExecuteCommand()
    {
      return true;
    }
    #endregion Commands

    #region Methoden

    public void ButtonPressed(string sender)
    {
      switch (sender)
      {
        case "CPC+":
          Cappucinos++;
          break;
        case "CPC-":
          if (Cappucinos > 0)
            Cappucinos--;
          break;

        case "MKF+":
          Milchkaffees++;
          break;
        case "MKF-":
          if (Milchkaffees > 0)
            Milchkaffees--;
          break;

        case "KFS+":
          KaffeesSchwarz++;
          break;
        case "KFS-":
          if (KaffeesSchwarz > 0)
            KaffeesSchwarz--;
          break;

        case "DEP+":
          DoppelteEspressi++;
          break;
        case "DEP-":
          if (DoppelteEspressi > 0)
            DoppelteEspressi--;
          break;

        case "EMA+":
          EspressoMacchiatos++;
          break;
        case "EMA-":
          if (EspressoMacchiatos > 0)
            EspressoMacchiatos--;
          break;

        case "LMA+":
          LatteMacchiatos++;
          break;
        case "LMA-":
          if (LatteMacchiatos > 0)
            LatteMacchiatos--;
          break;

        case "LMS+":
          LattesMitSchuss++;
          break;
        case "LMS-":
          if (LattesMitSchuss > 0)
            LattesMitSchuss--;
          break;

        case "ZUC+":
          Zucker++;
          break;
        case "ZUC-":
          if (Zucker > 0)
            Zucker--;
          break;

        default:
          break;
      }
    }

    public void AddKaffee(string name)
    {
      switch (name)
      {
        case "Anna":
          Cappucinos++;
          Zucker = Zucker + 2;
          break;
        case "Annika":
          LattesMitSchuss++;
          break;
        case "Daniel":
          KaffeesSchwarz++;
          break;
        case "Kevin":
          DoppelteEspressi++;
          break;
        case "Tim":
          Cappucinos++;
          Zucker++;
          break;
        case "Jakob":
          Milchkaffees++;
          Zucker = Zucker + 2;
          break;
        case "Jan":
          Milchkaffees++;
          break;
        case "Sabine":
          EspressoMacchiatos++;
          break;
        default:
          break;
      }
    }

    public void SubtractKaffee(string name)
    {
      switch (name)
      {
        case "Anna":
          Cappucinos--;
          Zucker = Zucker - 2;
          break;
        case "Annika":
          LattesMitSchuss--;
          break;
        case "Daniel":
          KaffeesSchwarz--;
          break;
        case "Kevin":
          DoppelteEspressi--;
          break;
        case "Tim":
          Cappucinos--;
          Zucker--;
          break;
        case "Jakob":
          Milchkaffees--;
          Zucker = Zucker - 2;
          break;
        case "Jan":
          Milchkaffees--;
          break;
        case "Sabine":
          EspressoMacchiatos--;
          break;
        default:
          break;
      }
    }

    #endregion Methoden

    #region Helper 

    public void DoPersons()
    {
      ObservableCollection<Person> l_Personen = new ObservableCollection<Person>();

      l_Personen.Add(new Person()
      {
        Name = "Anna",
        IsPresent = false
      });

      l_Personen.Add(new Person()
      {
        Name = "Annika",
        IsPresent = false
      });

      l_Personen.Add(new Person()
      {
        Name = "Daniel",
        IsPresent = false
      });

      l_Personen.Add(new Person()
      {
        Name = "Kevin",
        IsPresent = false
      });

      l_Personen.Add(new Person()
      {
        Name = "Tim",
        IsPresent = false
      });

      l_Personen.Add(new Person()
      {
        Name = "Jakob",
        IsPresent = false
      });

      l_Personen.Add(new Person()
      {
        Name = "Jan",
        IsPresent = false
      });

      l_Personen.Add(new Person()
      {
        Name = "Sabine",
        IsPresent = false
      });

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
