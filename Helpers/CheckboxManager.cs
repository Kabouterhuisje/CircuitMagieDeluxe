using CircuitMagieDeluxe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitMagieDeluxe.Helpers
{
    class CheckboxManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isChecked;
        private INode item;
        private CircuitSimulator circuit;

        public CheckboxManager()
        { }

        public CheckboxManager(INode item, CircuitSimulator circuit, bool isChecked = false)
        {
            this.item = item;
            this.circuit = circuit;
            if (item.Input.Count > 0)
            {
                this.isChecked = item.Input[0];
            }
            else
            {
                this.isChecked = isChecked;
            }
        }

        public INode Item
        {
            get { return item; }
            set
            {
                item = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Item"));
            }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));

                circuit.ResetNodes();

                item.Input.Clear();
                item.Input.Add(isChecked);
            }
        }
    }
}
