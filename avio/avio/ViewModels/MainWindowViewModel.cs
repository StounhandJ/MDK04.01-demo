using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Collections;
using Avalonia.Controls;
using avio.Models;
using avio.Pages;
using avio.Service;
using ReactiveUI;

namespace avio.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private static DB? db;
    public static Window? parent;

    private string _errorText;

    public string ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
    }

    public string Login { get; set; }
    public string Password { get; set; }

    private AvaloniaList<User> _users = new();

    public AvaloniaList<User> Users
    {
        get => _users;
        set => this.RaiseAndSetIfChanged(ref _users, value);
    }


    public ICommand CloseCommand
    {
        get => ReactiveCommand.Create(() => { parent?.Close(); });
    }

    private int count = 0;

    public ICommand LoginCommand
    {
        get => ReactiveCommand.Create(() =>
        {
            if (count >= 3)
            {
                ErrorText = "Лимит повторов, попробуйте позже";
                return;
            }

            if (Login.Length < 4)
            {
                ErrorText = "Логин не менее 4 символов";
                return;
            }

            if (Password.Length < 4)
            {
                ErrorText = "Пароль не менее 4 символов";
                return;
            }

            var res = db.execute("SELECT RoleID FROM users WHERE Email=? and Password=?", new List<object>()
            {
                Login,
                Password,
            });

            if (res.Count == 0)
            {
                ErrorText = "Пользователь не найден";
                
                count += 1;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(10000);
                    count -= 1;
                });
                
                return;
            }

            ErrorText = "";

            switch ((int)res[0]["RoleID"])
            {
                case 1:
                    ContentControls.RemoveAt(0);

                    var page = new AdminPage();

                    ContentControls.Add(page);

                    var res2 = db.execute("SELECT * FROM users");

                    AvaloniaList<User> test = new();
                    foreach (var userData in res2)
                    {
                        test.Add(new User(
                            (string)userData["Email"],
                            (string)userData["Password"],
                            (string?)userData["FirstName"],
                            (string)userData["LastName"],
                            (int?)userData["OfficeID"],
                            (int)userData["RoleID"]
                        ));
                    }

                    Users = test;

                    break;
                case 2:
                    break;
            }
        });
    }

    private AvaloniaList<UserControl> _contentControls = new()
    {
        new AuthPage()
    };

    public AvaloniaList<UserControl> ContentControls
    {
        get => _contentControls;
        set => this.RaiseAndSetIfChanged(ref _contentControls, value);
    }

    public MainWindowViewModel()
    {
        if (db == null)
        {
            db = new DB("localhost", "root", "qwerty", "db");
        }
    }
}