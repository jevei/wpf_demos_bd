using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ContactModel selectedContact;
        private ObservableCollection<ContactModel> contacts;
        private bool Old = true;
        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            private set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set { 
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand SaveContactCommand { get; set; }
        public RelayCommand SuppContactCommand { get; set; }
        public RelayCommand NewCustomerCommand { get; set; }
        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);
            SaveContactCommand = new RelayCommand(SaveContact);
            SuppContactCommand = new RelayCommand(SuppContact);
            NewCustomerCommand = new RelayCommand(NewContact);
            //SelectedContact = PhoneBookBusiness.GetContactByID(1);
            Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAllContact());
            Debug.WriteLine(Contacts.Count);
        }

        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            } else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":
                    SelectedContact = PhoneBookBusiness.GetContactByID(output);
                    break;
                case "name":
                    SelectedContact = PhoneBookBusiness.GetContactByName(input);
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }
        private void SaveContact(object parameter)
        {
            ContactModel SC=SelectedContact;
            if ((SelectedContact != null) && (Old == true))
            {
                PhoneBookBusiness.SaveContact(SelectedContact);
                Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAllContact());
                SelectedContact = PhoneBookBusiness.GetContactByID(SC.ContactID);
            }
            else if ((SelectedContact != null) && (Old == false))
            {
                Old = true;
                PhoneBookBusiness.AddContact(SelectedContact);
                Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAllContact());
            }
        }
        private void SuppContact(object parameter)
        {
            string input = parameter as string;
            int output;
            Int32.TryParse(input, out output);
            if (SelectedContact != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to suppress this contact?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    PhoneBookBusiness.SuppContact(output);
                    Contacts = new ObservableCollection<ContactModel>(PhoneBookBusiness.GetAllContact());
                }
            }
        }
        private void NewContact(object parameter)
        {
            SelectedContact = new ContactModel();
            Old = false;
        }
    }
}
