using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class ConfirmationModalVM: BaseViewModel
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                SendPropertyChanged(() => Text);
            }
        }

        public ConfirmationModalVM(string text)
        {
            HeaderVM = "Уведомление";
            Text = text;
        }
    }
}
