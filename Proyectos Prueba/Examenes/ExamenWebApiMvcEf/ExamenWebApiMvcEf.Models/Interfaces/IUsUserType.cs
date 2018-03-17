namespace ExamenWebApiMvcEf.Models.Interfaces
{
    using System.Collections.Generic;

    public interface IUsUserType
    {
        int IdUserType { get; set; }
        string UserType { get; set; }
        string Description { get; set; }

        ICollection<IUsUser> Usuarios {get;set;}
    }
}
