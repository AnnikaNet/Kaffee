using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace KaffeeApp.Model
{
  public class Person : INotifyPropertyChanged
  {
    #region Properties
    private string m_Name;
    public string Name
    {
      get { return m_Name; }
      set
      {
        m_Name = value;
        OnPropertyChanged();
      }
    }

    private bool m_IsPresent;
    public bool IsPresent
    {
      get { return m_IsPresent; }
      set
      {
        m_IsPresent = value;
        OnPropertyChanged();
      }
    }

    private int m_SugarCount;
    public int SugarCount
    {
      get { return m_SugarCount; }
      set
      {
        if (value > 0)
          m_SugarCount = value;
        else
          m_SugarCount = 0;
        OnPropertyChanged();
      }
    }

    private Kaffee m_Bestellung;
    public Kaffee Bestellung
    {
      get { return m_Bestellung; }
      set
      {
        m_Bestellung = value;
        OnPropertyChanged();
      }
    }

    #endregion Properties

    #region Commands
    public ICommand PlusButtonPressedCommand { get; internal set; }
    public ICommand MinusButtonPressedCommand { get; internal set; }
    public void PlusButtonPressed()
    {
      SugarCount++;
      this.OnPropertyChanged();
    }

    public void MinusButtonPressed()
    {
      if (SugarCount > 0)
        SugarCount--;
    }

    #endregion Commands

    #region Konstruktor 

    public Person()
    {
      PlusButtonPressedCommand = new RelayCommand(PlusButtonPressed);
      MinusButtonPressedCommand = new RelayCommand(MinusButtonPressed);

      Name = "";
      IsPresent = false;
      SugarCount = 0;
      Bestellung = new Kaffee();
    }

    public Person(Person p_Person)
    {
      PlusButtonPressedCommand = new RelayCommand(PlusButtonPressed);
      MinusButtonPressedCommand = new RelayCommand(MinusButtonPressed);

      Name = p_Person.Name;
      IsPresent = p_Person.IsPresent;
      SugarCount = p_Person.SugarCount;
      Bestellung = p_Person.Bestellung;
    }

    public Person(string p_Name, int p_SugarCount, Kaffee p_Bestellung)
    {
      PlusButtonPressedCommand = new RelayCommand(PlusButtonPressed);
      MinusButtonPressedCommand = new RelayCommand(MinusButtonPressed);

      Name = p_Name;
      IsPresent = false;
      SugarCount = p_SugarCount;
      Bestellung = p_Bestellung;
    }
    #endregion Konstruktor

    #region Helper 

    public override string ToString()
    {
      return Name;
    }

    public override bool Equals(object obj)
    {
      if (obj is Person per)
        return Name == per.Name;

      return base.Equals(obj);
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
