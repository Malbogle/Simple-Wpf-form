using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfLoginForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users = new List<User>(); 
        User curUser;
        public MainWindow()
        {
            InitializeComponent();
            curUser = new User();
            this.DataContext = curUser;
        }

        private void generateNameBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                                  "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            string _firstName = firstNameBox.Text;
            string _lastName = lastNameBox.Text;

            sb.Append(_firstName);
            sb.Append(_lastName);

            for(int i=0; i< 5; i++)
            {
                if(rand.Next(2) == 0) //0 => number goes next
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
            generatedName.Text = "Username: " + sb.ToString().Replace(" ", "");
            
        }

        private void firstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            generatedName.Text = "";
        }

        private void lastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            generatedName.Text = "";
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            users.Add(curUser);
            curUser = new User();
            passwordBox.Password = "";
            this.DataContext = curUser;
            MessageBox.Show("User added successfully");
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
    }

    public class AgeValidator : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        

        public AgeValidator()
        {

        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            if (!string.IsNullOrEmpty((string)value))
            {
                try
                {
                    age = int.Parse((string)value);
                }
                catch(Exception ex)
                {
                    return new ValidationResult(false, "Enter numeric age");
                }
            }
            else
            {
                return new ValidationResult(false, "Field cannot be left blank");
            }
            if(age > Max || age < Min)
            {
                return new ValidationResult(false, $"Enter value between {Min} and {Max}");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class EmailValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Match match = regex.Match(value.ToString());
            if(match == null || match == Match.Empty)
            {
                return new ValidationResult(false, "Enter valid email address");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class RequiredValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Field is required");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class TextLengthToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.IsNullOrEmpty(value as string))
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
