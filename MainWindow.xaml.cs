using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialLoginWpfApp;

public partial class MainWindow : Window
{
    private MyValidator _validator;
    private Brush _defaultBrash;
    private List<User> _users=new List<User>();

    public MainWindow()
    {
        InitializeComponent();
        _validator=new MyValidator();
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (AllFieldsValid())
        {
            if (_users.Any(user => user.Login == LoginTextBox.Text || user.Email == EmailTextBox.Text))
            {
                DrawInvalidFields();
            }
            else
            {
                UsersDatabase.AddUser(new User()
                {
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Password,
                    Email = EmailTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text

                });
                MessageBox.Show("Registration success!");
                Application.Current.Shutdown();
            }
        }
        else
        {
            DrawInvalidFields();
        }

    }
    private bool AllFieldsValid()
    {
        return _validator.IsValidLogin(LoginTextBox.Text) && _validator.IsValidEmail(EmailTextBox.Text) && _validator.IsValidPassword(PasswordTextBox.Password) && _validator.IsValidPhoneNumber(PhoneNumberTextBox.Text);
    }
    private void DrawInvalidFields()
    {
        if (!_validator.IsValidLogin(LoginTextBox.Text) || _users.Any(user=>user.Login==LoginTextBox.Text)) 
            LoginTextBox.BorderBrush = Brushes.IndianRed;
        if (!_validator.IsValidPassword(PasswordTextBox.Password))
            PasswordTextBox.BorderBrush = Brushes.IndianRed;
        if (!_validator.IsValidPhoneNumber(PhoneNumberTextBox.Text))
            PhoneNumberTextBox.BorderBrush = Brushes.IndianRed;
        if (!_validator.IsValidEmail(EmailTextBox.Text) || _users.Any(user=>user.Email == EmailTextBox.Text))
            EmailTextBox.BorderBrush = Brushes.IndianRed;

    }

    private void LoginTextBox_Loaded(object sender, RoutedEventArgs e)
    {
        _defaultBrash = LoginTextBox.BorderBrush;
    }

    private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(!_validator.IsValidLogin(LoginTextBox.Text) && LoginTextBox.BorderBrush == Brushes.IndianRed)
        {
            LoginTextBox.BorderBrush = _defaultBrash;
        }
       

    }

    private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if(_validator.IsValidPassword(PasswordTextBox.Password) && PasswordTextBox.BorderBrush == Brushes.IndianRed)
        {
            PasswordTextBox.BorderBrush=_defaultBrash;
        }
      

    }

    private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_validator.IsValidEmail(EmailTextBox.Text) && EmailTextBox.BorderBrush == Brushes.IndianRed)
        {
            EmailTextBox.BorderBrush = _defaultBrash;
        }
    

    }

    private void PhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_validator.IsValidPhoneNumber(PhoneNumberTextBox.Text) && PhoneNumberTextBox.BorderBrush == Brushes.IndianRed)
        {
            PhoneNumberTextBox.BorderBrush = _defaultBrash;
        }
       

    }

    private void Card_Loaded(object sender, RoutedEventArgs e)
    {
        //_users = UsersDatabase.GetAllUsers()?.ToList() ?? new List<User>();
    }
}
