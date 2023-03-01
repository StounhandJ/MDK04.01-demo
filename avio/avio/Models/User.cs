namespace avio.Models;

public class User
{
    private string Email { get; set; }
    private string Password { get; set; }
    private string? FirstName { get; set; }
    private string LastName { get; set; }
    private int? OfficeID { get; set; }
    private int RoleID { get; set; }

    public User(string Email, string Password, string? FirstName, string LastName, int? OfficeID, int RoleID)
    {
        this.Email = Email;
        this.Password = Password;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.OfficeID = OfficeID;
        this.RoleID = RoleID;
    }
}