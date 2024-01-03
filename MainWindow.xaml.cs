using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace ContactsApp
{
    public class MyContact : INotifyPropertyChanged
    {
        private string name;
        private string gmail;
        private string telephone;

        //Thuộc tính cho tên của liên lạc 
        public string Name
        {
            get 
            { 
                return name;
            }
            set 
            {
                SetProperty(ref name, value, nameof(Name)); 
            }
        }

        public string Gmail
        {
            get
            { 
                return gmail; 
            }
            set 
            {
                SetProperty(ref gmail, value, nameof(Gmail)); 
            }
        }

        public string Telephone
        {
            get
            { 
                return telephone;
            }
            set 
            { 
                SetProperty(ref telephone, value, nameof(Telephone)); 
            }
        }

        private void SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; 

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<MyContact> Contacts { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeContacts();
            DataContext = this;
        }

        private void InitializeContacts()
        {
          
            Contacts = new ObservableCollection<MyContact>
              {
                 CreateContact("Папа", "papa@gmail.com", "+7 (931) 585 013"),
                 CreateContact("Мама", "mom@gmail.com", "+7 (931) 585 893"),
                 CreateContact("Маша", "maria@gmail.com", "+7 (952) 645 328")
              };
        }

        private MyContact CreateContact(string name, string gmail, string telephone)
        {
            return new MyContact { Name = name, Gmail = gmail, Telephone = telephone };
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                AddNewContact();
                ClearInputFields();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) || string.IsNullOrWhiteSpace(GmailBox.Text) || string.IsNullOrWhiteSpace(TelephoneBox.Text))
            {
                MessageBox.Show("Ошибка данных! Проверьте информацию!");
                return false;
            }
            return true;
        }

        private void AddNewContact()
        {
            string newName = NameBox.Text;
            string newEmail = GmailBox.Text;
            string newPhone = TelephoneBox.Text;

            MyContact newContact = new MyContact
            {
                Name = newName,
                Gmail = newEmail,
                Telephone = newPhone
            };

            Contacts.Add(newContact);
            ContactsList.SelectedItem = newContact;
        }

        private void ClearInputFields()
        {
            NameBox.Clear();
            GmailBox.Clear();
            TelephoneBox.Clear();
        }



        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                SaveContactChanges();
                ClearInputFields();
                ContactsList.SelectedItem = null;
            }
        }

        private void SaveContactChanges()
        {
            MyContact selectedContact = ContactsList.SelectedItem as MyContact;

            if (selectedContact != null)
            {
                UpdateContactProperties(selectedContact);
            }
            else
            {
                MessageBox.Show("No contact selected for editing!");
            }
        }

        private void UpdateContactProperties(MyContact contact)
        {
            contact.Name = NameBox.Text;
            contact.Gmail = GmailBox.Text;
            contact.Telephone = TelephoneBox.Text;
        }


        private void NameBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Phương thức xảy ra khi nội dung trong trường nhập liệu NameBox thay đổi
        }
        private void GmailBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Phương thức xảy ra khi nội dung trong trường nhập liệu GmailBox thay đổi
        }
        private void TelephoneBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Phương thức xảy ra khi nội dung trong trường nhập liệu TelephoneBox thay đổi
        }

    }
}
