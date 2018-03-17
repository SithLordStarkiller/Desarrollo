namespace ExamenWebApiMvcEf.Models.Models
{
    using Interfaces;

    using System;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [DataContract]
    [Serializable]
    [Table("UsUser", Schema = "Security")]
    public class UsUser : IUsUser
    {
        [DataMember]
        [Key]
        public int IdUser { get; set; }

        [DataMember]
        public int? IdUserType { get; set; }

        [DataMember]
        [Column(TypeName = "VARCHAR")]
        //[Index("IndexUserName",IsClustered = false, IsUnique = true)]
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }

        [DataMember]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        [Required]
        public string Password { get; set; }

        [DataMember]
        [ForeignKey("IdUserType")]
        public virtual IUsUserType UsertType { get ; set; }
    }
}
