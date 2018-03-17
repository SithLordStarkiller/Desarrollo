namespace ExamenWebApiMvcEf.Models.Models
{
    using Interfaces;

    using System;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    [DataContract]
    [Serializable]
    [Table("UsUserType", Schema = "Security")]
    public class UsUserType : IUsUserType
    {
        [DataMember]
        [Key]//, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUserType { get; set; }

        [DataMember]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string UserType { get; set; }

        [DataMember]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Description { get; set; }

        [DataMember]
        public ICollection<IUsUser> Usuarios { get; set; }
    }
}
