﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.42
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace eClock {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("DS_Eicion_Contenido")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class DS_Eicion_Contenido : System.Data.DataSet {
        
        private EC_SUSCRIPCIONDataTable tableEC_SUSCRIPCION;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DS_Eicion_Contenido() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected DS_Eicion_Contenido(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["EC_SUSCRIPCION"] != null)) {
                    base.Tables.Add(new EC_SUSCRIPCIONDataTable(ds.Tables["EC_SUSCRIPCION"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public EC_SUSCRIPCIONDataTable EC_SUSCRIPCION {
            get {
                return this.tableEC_SUSCRIPCION;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.BrowsableAttribute(true)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override System.Data.DataSet Clone() {
            DS_Eicion_Contenido cln = ((DS_Eicion_Contenido)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["EC_SUSCRIPCION"] != null)) {
                    base.Tables.Add(new EC_SUSCRIPCIONDataTable(ds.Tables["EC_SUSCRIPCION"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new System.Xml.XmlTextReader(stream), null);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableEC_SUSCRIPCION = ((EC_SUSCRIPCIONDataTable)(base.Tables["EC_SUSCRIPCION"]));
            if ((initTable == true)) {
                if ((this.tableEC_SUSCRIPCION != null)) {
                    this.tableEC_SUSCRIPCION.InitVars();
                }
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "DS_Eicion_Contenido";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/DS_Eicion_Contenido.xsd";
            this.Locale = new System.Globalization.CultureInfo("es-MX");
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableEC_SUSCRIPCION = new EC_SUSCRIPCIONDataTable();
            base.Tables.Add(this.tableEC_SUSCRIPCION);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeEC_SUSCRIPCION() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(System.Xml.Schema.XmlSchemaSet xs) {
            DS_Eicion_Contenido ds = new DS_Eicion_Contenido();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }
        
        public delegate void EC_SUSCRIPCIONRowChangeEventHandler(object sender, EC_SUSCRIPCIONRowChangeEvent e);
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class EC_SUSCRIPCIONDataTable : System.Data.DataTable, System.Collections.IEnumerable {
            
            private System.Data.DataColumn columnSUSCRIPCION_ID;
            
            private System.Data.DataColumn columnSUSCRIPCION_NOMBRE;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_SUSCRIPCIONDataTable() {
                this.TableName = "EC_SUSCRIPCION";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal EC_SUSCRIPCIONDataTable(System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected EC_SUSCRIPCIONDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUSCRIPCION_IDColumn {
                get {
                    return this.columnSUSCRIPCION_ID;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUSCRIPCION_NOMBREColumn {
                get {
                    return this.columnSUSCRIPCION_NOMBRE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_SUSCRIPCIONRow this[int index] {
                get {
                    return ((EC_SUSCRIPCIONRow)(this.Rows[index]));
                }
            }
            
            public event EC_SUSCRIPCIONRowChangeEventHandler EC_SUSCRIPCIONRowChanging;
            
            public event EC_SUSCRIPCIONRowChangeEventHandler EC_SUSCRIPCIONRowChanged;
            
            public event EC_SUSCRIPCIONRowChangeEventHandler EC_SUSCRIPCIONRowDeleting;
            
            public event EC_SUSCRIPCIONRowChangeEventHandler EC_SUSCRIPCIONRowDeleted;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddEC_SUSCRIPCIONRow(EC_SUSCRIPCIONRow row) {
                this.Rows.Add(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_SUSCRIPCIONRow AddEC_SUSCRIPCIONRow(decimal SUSCRIPCION_ID, string SUSCRIPCION_NOMBRE) {
                EC_SUSCRIPCIONRow rowEC_SUSCRIPCIONRow = ((EC_SUSCRIPCIONRow)(this.NewRow()));
                rowEC_SUSCRIPCIONRow.ItemArray = new object[] {
                        SUSCRIPCION_ID,
                        SUSCRIPCION_NOMBRE};
                this.Rows.Add(rowEC_SUSCRIPCIONRow);
                return rowEC_SUSCRIPCIONRow;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_SUSCRIPCIONRow FindBySUSCRIPCION_ID(decimal SUSCRIPCION_ID) {
                return ((EC_SUSCRIPCIONRow)(this.Rows.Find(new object[] {
                            SUSCRIPCION_ID})));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone() {
                EC_SUSCRIPCIONDataTable cln = ((EC_SUSCRIPCIONDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance() {
                return new EC_SUSCRIPCIONDataTable();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnSUSCRIPCION_ID = base.Columns["SUSCRIPCION_ID"];
                this.columnSUSCRIPCION_NOMBRE = base.Columns["SUSCRIPCION_NOMBRE"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnSUSCRIPCION_ID = new System.Data.DataColumn("SUSCRIPCION_ID", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUSCRIPCION_ID);
                this.columnSUSCRIPCION_NOMBRE = new System.Data.DataColumn("SUSCRIPCION_NOMBRE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUSCRIPCION_NOMBRE);
                this.Constraints.Add(new System.Data.UniqueConstraint("Constraint1", new System.Data.DataColumn[] {
                                this.columnSUSCRIPCION_ID}, true));
                this.columnSUSCRIPCION_ID.AllowDBNull = false;
                this.columnSUSCRIPCION_ID.Unique = true;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_SUSCRIPCIONRow NewEC_SUSCRIPCIONRow() {
                return ((EC_SUSCRIPCIONRow)(this.NewRow()));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) {
                return new EC_SUSCRIPCIONRow(builder);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType() {
                return typeof(EC_SUSCRIPCIONRow);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.EC_SUSCRIPCIONRowChanged != null)) {
                    this.EC_SUSCRIPCIONRowChanged(this, new EC_SUSCRIPCIONRowChangeEvent(((EC_SUSCRIPCIONRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.EC_SUSCRIPCIONRowChanging != null)) {
                    this.EC_SUSCRIPCIONRowChanging(this, new EC_SUSCRIPCIONRowChangeEvent(((EC_SUSCRIPCIONRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.EC_SUSCRIPCIONRowDeleted != null)) {
                    this.EC_SUSCRIPCIONRowDeleted(this, new EC_SUSCRIPCIONRowChangeEvent(((EC_SUSCRIPCIONRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.EC_SUSCRIPCIONRowDeleting != null)) {
                    this.EC_SUSCRIPCIONRowDeleting(this, new EC_SUSCRIPCIONRowChangeEvent(((EC_SUSCRIPCIONRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveEC_SUSCRIPCIONRow(EC_SUSCRIPCIONRow row) {
                this.Rows.Remove(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs) {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                DS_Eicion_Contenido ds = new DS_Eicion_Contenido();
                xs.Add(ds.GetSchemaSerializable());
                System.Xml.Schema.XmlSchemaAny any1 = new System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                System.Xml.Schema.XmlSchemaAny any2 = new System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                System.Xml.Schema.XmlSchemaAttribute attribute1 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                System.Xml.Schema.XmlSchemaAttribute attribute2 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "EC_SUSCRIPCIONDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class EC_SUSCRIPCIONRow : System.Data.DataRow {
            
            private EC_SUSCRIPCIONDataTable tableEC_SUSCRIPCION;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal EC_SUSCRIPCIONRow(System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableEC_SUSCRIPCION = ((EC_SUSCRIPCIONDataTable)(this.Table));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal SUSCRIPCION_ID {
                get {
                    return ((decimal)(this[this.tableEC_SUSCRIPCION.SUSCRIPCION_IDColumn]));
                }
                set {
                    this[this.tableEC_SUSCRIPCION.SUSCRIPCION_IDColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SUSCRIPCION_NOMBRE {
                get {
                    try {
                        return ((string)(this[this.tableEC_SUSCRIPCION.SUSCRIPCION_NOMBREColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("El valor de la columna \'SUSCRIPCION_NOMBRE\' de la tabla \'EC_SUSCRIPCION\' es DBNull.", e);
                    }
                }
                set {
                    this[this.tableEC_SUSCRIPCION.SUSCRIPCION_NOMBREColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUSCRIPCION_NOMBRENull() {
                return this.IsNull(this.tableEC_SUSCRIPCION.SUSCRIPCION_NOMBREColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUSCRIPCION_NOMBRENull() {
                this[this.tableEC_SUSCRIPCION.SUSCRIPCION_NOMBREColumn] = System.Convert.DBNull;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class EC_SUSCRIPCIONRowChangeEvent : System.EventArgs {
            
            private EC_SUSCRIPCIONRow eventRow;
            
            private System.Data.DataRowAction eventAction;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_SUSCRIPCIONRowChangeEvent(EC_SUSCRIPCIONRow row, System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_SUSCRIPCIONRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591