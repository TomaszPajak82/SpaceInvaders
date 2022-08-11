using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SoftwareCore.UI.ViewModel
{
    public class ViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ChangablePropertyChangedEventArgs ReusablePropertyChangedEventArgs { get; set; } = new ChangablePropertyChangedEventArgs();


        private class ChangablePropertyChangedEventArgs: PropertyChangedEventArgs
        {
            string propertyName;
            public override string PropertyName {
                get { return this.propertyName; }
            }

            public void SetPropertyName(string name) {
                this.propertyName = name;
            }

            public ChangablePropertyChangedEventArgs():base(null) {
            }
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "") {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            ReusablePropertyChangedEventArgs.SetPropertyName(propertyName);
            PropertyChanged?.Invoke(this, ReusablePropertyChangedEventArgs);
        }


    }
}