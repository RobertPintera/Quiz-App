using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Quiz_App.Services
{
    public class DialogService : IDialogService
    {
        private static DialogService? _instance;
        private DialogService() { }
        public static DialogService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DialogService();
                }
                return _instance;
            }
        }

        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ShowConfirmation(string message, string title)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
    }
}
