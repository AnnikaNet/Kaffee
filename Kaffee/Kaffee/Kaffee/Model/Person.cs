using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kaffee.Model
{
  public class Person
  {
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


    #region Helper 

    public override string ToString()
    {
      return Name;
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
