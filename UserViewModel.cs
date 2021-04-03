using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace wpfLoginForm
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private List<UserModel> users;
        private UserModel userModel;
        public UserModel UserModel
        {
            get
            {
                return userModel;
            }
            set
            {
                userModel = value;
                NotifyPropertyChanged("UserModel");
            }
        }

        private ICommand generateUserNameBtn;
        private ICommand submitUserBtn;


        public UserViewModel()
        {
            UserModel = new UserModel();
            users = new List<UserModel>();
        }

        public ICommand GenerateUserNameBtn
        {
            get
            {
                if (generateUserNameBtn != null) return generateUserNameBtn;
                generateUserNameBtn = new CommandImpl(OnGenerateUserName, CanGenerateUserName);
                return generateUserNameBtn;
            }
        }

        public ICommand SubmitUserBtn
        {
            get
            {
                if (submitUserBtn != null) return submitUserBtn;
                submitUserBtn = new CommandImpl(OnSubmitUser); //Implement can executre...Atm all done via datatriggers
                return submitUserBtn;
            }
        }

        private bool CanSubmitUser()
        {
            //So when validating check if HasErrors is false and then return 
            throw new NotImplementedException();
        }

        private void OnSubmitUser()
        {
            users.Add(UserModel);
            UserModel = new UserModel();
            MessageBox.Show("Student added succesfully");
        }

        private bool CanGenerateUserName()
        {
            if (string.IsNullOrEmpty(UserModel.FirstName) || string.IsNullOrEmpty(UserModel.LastName)) return false;
            return true;
        }

        private void OnGenerateUserName()
        {
            string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                                  "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();

            sb.Append(UserModel.FirstName);
            sb.Append(UserModel.LastName);

            for (int i = 0; i < 5; i++)
            {
                if (rand.Next(2) == 0) //0 => number goes next
                {
                    sb.Append(rand.Next(10));
                }
                else
                {
                    if (rand.Next(2) == 0) // 0 => lower case, 1 => uppercase
                    {
                        sb.Append(alphabet[rand.Next(26)]);
                    }
                    else
                    {
                        sb.Append(alphabet[rand.Next(26)].ToUpper());
                    }
                }
            }
            UserModel.UserName = sb.ToString().Replace(" ", "");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
