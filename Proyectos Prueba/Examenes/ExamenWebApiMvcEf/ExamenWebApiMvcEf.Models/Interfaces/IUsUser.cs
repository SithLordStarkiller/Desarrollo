namespace ExamenWebApiMvcEf.Models.Interfaces
{
    public interface IUsUser
    {
        int IdUser { get; set; }
        string UserName { get; set; }
        string Password { get; set; }

        IUsUserType UsertType { get; set; }
    }
}
