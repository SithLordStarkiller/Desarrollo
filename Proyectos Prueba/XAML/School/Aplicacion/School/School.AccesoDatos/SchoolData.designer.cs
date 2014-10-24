﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34209
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace School.AccesoDatos
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SchoolSystem")]
	public partial class SchoolDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definiciones de métodos de extensibilidad
    partial void OnCreated();
    partial void InsertREGISTRATION(REGISTRATION instance);
    partial void UpdateREGISTRATION(REGISTRATION instance);
    partial void DeleteREGISTRATION(REGISTRATION instance);
    partial void InsertSTUDENT(STUDENT instance);
    partial void UpdateSTUDENT(STUDENT instance);
    partial void DeleteSTUDENT(STUDENT instance);
    partial void InsertSUBJECT(SUBJECT instance);
    partial void UpdateSUBJECT(SUBJECT instance);
    partial void DeleteSUBJECT(SUBJECT instance);
    #endregion
		
		public SchoolDataDataContext() : 
				base(global::School.AccesoDatos.Properties.Settings.Default.SchoolSystemConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SchoolDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SchoolDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SchoolDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SchoolDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<REGISTRATION> REGISTRATION
		{
			get
			{
				return this.GetTable<REGISTRATION>();
			}
		}
		
		public System.Data.Linq.Table<STUDENT> STUDENT
		{
			get
			{
				return this.GetTable<STUDENT>();
			}
		}
		
		public System.Data.Linq.Table<SUBJECT> SUBJECT
		{
			get
			{
				return this.GetTable<SUBJECT>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.REGISTRATION")]
	public partial class REGISTRATION : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _REGISTRATIONID;
		
		private System.Nullable<int> _SUBJECTID;
		
		private System.Nullable<int> _STUDENTID;
		
		private string _GRADE;
		
		private string _STATUS;
		
		private EntityRef<STUDENT> _STUDENT;
		
		private EntityRef<SUBJECT> _SUBJECT;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnREGISTRATIONIDChanging(int value);
    partial void OnREGISTRATIONIDChanged();
    partial void OnSUBJECTIDChanging(System.Nullable<int> value);
    partial void OnSUBJECTIDChanged();
    partial void OnSTUDENTIDChanging(System.Nullable<int> value);
    partial void OnSTUDENTIDChanged();
    partial void OnGRADEChanging(string value);
    partial void OnGRADEChanged();
    partial void OnSTATUSChanging(string value);
    partial void OnSTATUSChanged();
    #endregion
		
		public REGISTRATION()
		{
			this._STUDENT = default(EntityRef<STUDENT>);
			this._SUBJECT = default(EntityRef<SUBJECT>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REGISTRATIONID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int REGISTRATIONID
		{
			get
			{
				return this._REGISTRATIONID;
			}
			set
			{
				if ((this._REGISTRATIONID != value))
				{
					this.OnREGISTRATIONIDChanging(value);
					this.SendPropertyChanging();
					this._REGISTRATIONID = value;
					this.SendPropertyChanged("REGISTRATIONID");
					this.OnREGISTRATIONIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUBJECTID", DbType="Int")]
		public System.Nullable<int> SUBJECTID
		{
			get
			{
				return this._SUBJECTID;
			}
			set
			{
				if ((this._SUBJECTID != value))
				{
					if (this._SUBJECT.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSUBJECTIDChanging(value);
					this.SendPropertyChanging();
					this._SUBJECTID = value;
					this.SendPropertyChanged("SUBJECTID");
					this.OnSUBJECTIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STUDENTID", DbType="Int")]
		public System.Nullable<int> STUDENTID
		{
			get
			{
				return this._STUDENTID;
			}
			set
			{
				if ((this._STUDENTID != value))
				{
					if (this._STUDENT.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSTUDENTIDChanging(value);
					this.SendPropertyChanging();
					this._STUDENTID = value;
					this.SendPropertyChanged("STUDENTID");
					this.OnSTUDENTIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GRADE", DbType="VarChar(2)")]
		public string GRADE
		{
			get
			{
				return this._GRADE;
			}
			set
			{
				if ((this._GRADE != value))
				{
					this.OnGRADEChanging(value);
					this.SendPropertyChanging();
					this._GRADE = value;
					this.SendPropertyChanged("GRADE");
					this.OnGRADEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS", DbType="VarChar(1)")]
		public string STATUS
		{
			get
			{
				return this._STATUS;
			}
			set
			{
				if ((this._STATUS != value))
				{
					this.OnSTATUSChanging(value);
					this.SendPropertyChanging();
					this._STATUS = value;
					this.SendPropertyChanged("STATUS");
					this.OnSTATUSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="STUDENT_REGISTRATION", Storage="_STUDENT", ThisKey="STUDENTID", OtherKey="STUDENTID", IsForeignKey=true)]
		public STUDENT STUDENT
		{
			get
			{
				return this._STUDENT.Entity;
			}
			set
			{
				STUDENT previousValue = this._STUDENT.Entity;
				if (((previousValue != value) 
							|| (this._STUDENT.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._STUDENT.Entity = null;
						previousValue.REGISTRATION.Remove(this);
					}
					this._STUDENT.Entity = value;
					if ((value != null))
					{
						value.REGISTRATION.Add(this);
						this._STUDENTID = value.STUDENTID;
					}
					else
					{
						this._STUDENTID = default(Nullable<int>);
					}
					this.SendPropertyChanged("STUDENT");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SUBJECT_REGISTRATION", Storage="_SUBJECT", ThisKey="SUBJECTID", OtherKey="SUBJECTID", IsForeignKey=true)]
		public SUBJECT SUBJECT
		{
			get
			{
				return this._SUBJECT.Entity;
			}
			set
			{
				SUBJECT previousValue = this._SUBJECT.Entity;
				if (((previousValue != value) 
							|| (this._SUBJECT.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._SUBJECT.Entity = null;
						previousValue.REGISTRATION.Remove(this);
					}
					this._SUBJECT.Entity = value;
					if ((value != null))
					{
						value.REGISTRATION.Add(this);
						this._SUBJECTID = value.SUBJECTID;
					}
					else
					{
						this._SUBJECTID = default(Nullable<int>);
					}
					this.SendPropertyChanged("SUBJECT");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.STUDENT")]
	public partial class STUDENT : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _STUDENTID;
		
		private string _FIRSTNAME;
		
		private string _LASTNAME;
		
		private string _GENDER;
		
		private System.Nullable<double> _GPA;
		
		private EntitySet<REGISTRATION> _REGISTRATION;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSTUDENTIDChanging(int value);
    partial void OnSTUDENTIDChanged();
    partial void OnFIRSTNAMEChanging(string value);
    partial void OnFIRSTNAMEChanged();
    partial void OnLASTNAMEChanging(string value);
    partial void OnLASTNAMEChanged();
    partial void OnGENDERChanging(string value);
    partial void OnGENDERChanged();
    partial void OnGPAChanging(System.Nullable<double> value);
    partial void OnGPAChanged();
    #endregion
		
		public STUDENT()
		{
			this._REGISTRATION = new EntitySet<REGISTRATION>(new Action<REGISTRATION>(this.attach_REGISTRATION), new Action<REGISTRATION>(this.detach_REGISTRATION));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STUDENTID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int STUDENTID
		{
			get
			{
				return this._STUDENTID;
			}
			set
			{
				if ((this._STUDENTID != value))
				{
					this.OnSTUDENTIDChanging(value);
					this.SendPropertyChanging();
					this._STUDENTID = value;
					this.SendPropertyChanged("STUDENTID");
					this.OnSTUDENTIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FIRSTNAME", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string FIRSTNAME
		{
			get
			{
				return this._FIRSTNAME;
			}
			set
			{
				if ((this._FIRSTNAME != value))
				{
					this.OnFIRSTNAMEChanging(value);
					this.SendPropertyChanging();
					this._FIRSTNAME = value;
					this.SendPropertyChanged("FIRSTNAME");
					this.OnFIRSTNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LASTNAME", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string LASTNAME
		{
			get
			{
				return this._LASTNAME;
			}
			set
			{
				if ((this._LASTNAME != value))
				{
					this.OnLASTNAMEChanging(value);
					this.SendPropertyChanging();
					this._LASTNAME = value;
					this.SendPropertyChanged("LASTNAME");
					this.OnLASTNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GENDER", DbType="VarChar(1) NOT NULL", CanBeNull=false)]
		public string GENDER
		{
			get
			{
				return this._GENDER;
			}
			set
			{
				if ((this._GENDER != value))
				{
					this.OnGENDERChanging(value);
					this.SendPropertyChanging();
					this._GENDER = value;
					this.SendPropertyChanged("GENDER");
					this.OnGENDERChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GPA", DbType="Float")]
		public System.Nullable<double> GPA
		{
			get
			{
				return this._GPA;
			}
			set
			{
				if ((this._GPA != value))
				{
					this.OnGPAChanging(value);
					this.SendPropertyChanging();
					this._GPA = value;
					this.SendPropertyChanged("GPA");
					this.OnGPAChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="STUDENT_REGISTRATION", Storage="_REGISTRATION", ThisKey="STUDENTID", OtherKey="STUDENTID")]
		public EntitySet<REGISTRATION> REGISTRATION
		{
			get
			{
				return this._REGISTRATION;
			}
			set
			{
				this._REGISTRATION.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_REGISTRATION(REGISTRATION entity)
		{
			this.SendPropertyChanging();
			entity.STUDENT = this;
		}
		
		private void detach_REGISTRATION(REGISTRATION entity)
		{
			this.SendPropertyChanging();
			entity.STUDENT = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SUBJECT")]
	public partial class SUBJECT : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SUBJECTID;
		
		private string _SUBJECTNAME;
		
		private string _SUBJECTDESCRIPTION;
		
		private double _SUBJECTCREDITS;
		
		private EntitySet<REGISTRATION> _REGISTRATION;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSUBJECTIDChanging(int value);
    partial void OnSUBJECTIDChanged();
    partial void OnSUBJECTNAMEChanging(string value);
    partial void OnSUBJECTNAMEChanged();
    partial void OnSUBJECTDESCRIPTIONChanging(string value);
    partial void OnSUBJECTDESCRIPTIONChanged();
    partial void OnSUBJECTCREDITSChanging(double value);
    partial void OnSUBJECTCREDITSChanged();
    #endregion
		
		public SUBJECT()
		{
			this._REGISTRATION = new EntitySet<REGISTRATION>(new Action<REGISTRATION>(this.attach_REGISTRATION), new Action<REGISTRATION>(this.detach_REGISTRATION));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUBJECTID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int SUBJECTID
		{
			get
			{
				return this._SUBJECTID;
			}
			set
			{
				if ((this._SUBJECTID != value))
				{
					this.OnSUBJECTIDChanging(value);
					this.SendPropertyChanging();
					this._SUBJECTID = value;
					this.SendPropertyChanged("SUBJECTID");
					this.OnSUBJECTIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUBJECTNAME", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string SUBJECTNAME
		{
			get
			{
				return this._SUBJECTNAME;
			}
			set
			{
				if ((this._SUBJECTNAME != value))
				{
					this.OnSUBJECTNAMEChanging(value);
					this.SendPropertyChanging();
					this._SUBJECTNAME = value;
					this.SendPropertyChanged("SUBJECTNAME");
					this.OnSUBJECTNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUBJECTDESCRIPTION", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SUBJECTDESCRIPTION
		{
			get
			{
				return this._SUBJECTDESCRIPTION;
			}
			set
			{
				if ((this._SUBJECTDESCRIPTION != value))
				{
					this.OnSUBJECTDESCRIPTIONChanging(value);
					this.SendPropertyChanging();
					this._SUBJECTDESCRIPTION = value;
					this.SendPropertyChanged("SUBJECTDESCRIPTION");
					this.OnSUBJECTDESCRIPTIONChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUBJECTCREDITS", DbType="Float NOT NULL")]
		public double SUBJECTCREDITS
		{
			get
			{
				return this._SUBJECTCREDITS;
			}
			set
			{
				if ((this._SUBJECTCREDITS != value))
				{
					this.OnSUBJECTCREDITSChanging(value);
					this.SendPropertyChanging();
					this._SUBJECTCREDITS = value;
					this.SendPropertyChanged("SUBJECTCREDITS");
					this.OnSUBJECTCREDITSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SUBJECT_REGISTRATION", Storage="_REGISTRATION", ThisKey="SUBJECTID", OtherKey="SUBJECTID")]
		public EntitySet<REGISTRATION> REGISTRATION
		{
			get
			{
				return this._REGISTRATION;
			}
			set
			{
				this._REGISTRATION.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_REGISTRATION(REGISTRATION entity)
		{
			this.SendPropertyChanging();
			entity.SUBJECT = this;
		}
		
		private void detach_REGISTRATION(REGISTRATION entity)
		{
			this.SendPropertyChanging();
			entity.SUBJECT = null;
		}
	}
}
#pragma warning restore 1591
