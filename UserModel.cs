using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace wpfLoginForm
{
    public class UserModel : INotifyPropertyChanged
    {
        public SecureString Password {private get; set; }
        private string firstName;
        private string lastName;
        private string userName;
        private int age;
        public string Email { get; set; }
        public string PhysAddress { get; set; }
        public int Age
        {
            get { return age; }
            set 
            { 
                age = value;
                NotifyPropertyChanged("Age");
            }
        }

     
        public string FirstName
        {
            get { return firstName; }
            set 
            { 
                firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                this.userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

   
        public UserModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
