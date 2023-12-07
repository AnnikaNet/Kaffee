using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace KaffeeApp.Model
{
  public class Kaffee : INotifyPropertyChanged
  {
    #region Properties
    private string m_Name;
    public string Name
    {
      get
      {
        return m_Name;
      }
      set
      {
        m_Name = value;
        OnPropertyChanged();
      }
    }

    private string m_Image;
    public string Image
    {
      get { return m_Image; }
      set
      {
        m_Image = value;
        OnPropertyChanged();
      }
    }


    private int m_Anzahl;
    public int Anzahl
    {
      get { return m_Anzahl; }
      set
      {
        if (value > 0)
          m_Anzahl = value;
        else
          m_Anzahl = 0;
        OnPropertyChanged();
      }
    }


    #endregion Properties

    #region Commands
    public ICommand PlusButtonPressedCommand { get; internal set; }
    public ICommand MinusButtonPressedCommand { get; internal set; }
    public void PlusButtonPressed()
    {
      Anzahl++;
      this.OnPropertyChanged();
    }

    public void MinusButtonPressed()
    {
      if (Anzahl > 0)
        Anzahl--;
    }

    #endregion Commands

    #region Konstruktor

    public Kaffee()
    {
      PlusButtonPressedCommand = new RelayCommand(PlusButtonPressed);
      MinusButtonPressedCommand = new RelayCommand(MinusButtonPressed);

      Name = "";
      Anzahl = 0;
      Image = "";
    }

    public Kaffee(Kaffee p_Kaffee)
    {
      PlusButtonPressedCommand = new RelayCommand(PlusButtonPressed);
      MinusButtonPressedCommand = new RelayCommand(MinusButtonPressed);

      Name = p_Kaffee.Name;
      Anzahl = p_Kaffee.Anzahl;
      Image = p_Kaffee.Image;
    }

    public Kaffee(string p_Name, string p_Image = "")
    {
      PlusButtonPressedCommand = new RelayCommand(PlusButtonPressed);
      MinusButtonPressedCommand = new RelayCommand(MinusButtonPressed);

      Name = p_Name;
      Anzahl = 0;
      Image = p_Image == "" ? p_Name.Replace(" ", "") : p_Image;
    }

    #endregion Konstruktor

    #region Methoden
    public override string ToString()
    {
      return Name;
    }
    #endregion Methoden

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    #endregion PropertyChanged

  }
}
