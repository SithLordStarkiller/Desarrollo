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
    [System.Xml.Serialization.XmlRootAttribute("DS_AccesosNAsig")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class DS_AccesosNAsig : System.Data.DataSet {
        
        private EC_PERSONASDataTable tableEC_PERSONAS;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public DS_AccesosNAsig() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected DS_AccesosNAsig(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
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
                if ((ds.Tables["EC_PERSONAS"] != null)) {
                    base.Tables.Add(new EC_PERSONASDataTable(ds.Tables["EC_PERSONAS"]));
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
        public EC_PERSONASDataTable EC_PERSONAS {
            get {
                return this.tableEC_PERSONAS;
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
            DS_AccesosNAsig cln = ((DS_AccesosNAsig)(base.Clone()));
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
                if ((ds.Tables["EC_PERSONAS"] != null)) {
                    base.Tables.Add(new EC_PERSONASDataTable(ds.Tables["EC_PERSONAS"]));
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
            this.tableEC_PERSONAS = ((EC_PERSONASDataTable)(base.Tables["EC_PERSONAS"]));
            if ((initTable == true)) {
                if ((this.tableEC_PERSONAS != null)) {
                    this.tableEC_PERSONAS.InitVars();
                }
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "DS_AccesosNAsig";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/DS_AccesosNAsig.xsd";
            this.Locale = new System.Globalization.CultureInfo("es-MX");
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableEC_PERSONAS = new EC_PERSONASDataTable();
            base.Tables.Add(this.tableEC_PERSONAS);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeEC_PERSONAS() {
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
            DS_AccesosNAsig ds = new DS_AccesosNAsig();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }
        
        public delegate void EC_PERSONASRowChangeEventHandler(object sender, EC_PERSONASRowChangeEvent e);
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class EC_PERSONASDataTable : System.Data.DataTable, System.Collections.IEnumerable {
            
            private System.Data.DataColumn columnQUITAR;
            
            private System.Data.DataColumn columnTERMINALES_DEXTRAS_ID;
            
            private System.Data.DataColumn columnTERMINALES_DEXTRAS_TEXTO1;
            
            private System.Data.DataColumn columnTERMINALES_DEXTRAS_TEXTO2;
            
            private System.Data.DataColumn columnTERMINAL_NOMBRE;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_PERSONASDataTable() {
                this.TableName = "EC_PERSONAS";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal EC_PERSONASDataTable(System.Data.DataTable table) {
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
            protected EC_PERSONASDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn QUITARColumn {
                get {
                    return this.columnQUITAR;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn TERMINALES_DEXTRAS_IDColumn {
                get {
                    return this.columnTERMINALES_DEXTRAS_ID;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn TERMINALES_DEXTRAS_TEXTO1Column {
                get {
                    return this.columnTERMINALES_DEXTRAS_TEXTO1;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn TERMINALES_DEXTRAS_TEXTO2Column {
                get {
                    return this.columnTERMINALES_DEXTRAS_TEXTO2;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn TERMINAL_NOMBREColumn {
                get {
                    return this.columnTERMINAL_NOMBRE;
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
            public EC_PERSONASRow this[int index] {
                get {
                    return ((EC_PERSONASRow)(this.Rows[index]));
                }
            }
            
            public event EC_PERSONASRowChangeEventHandler EC_PERSONASRowChanging;
            
            public event EC_PERSONASRowChangeEventHandler EC_PERSONASRowChanged;
            
            public event EC_PERSONASRowChangeEventHandler EC_PERSONASRowDeleting;
            
            public event EC_PERSONASRowChangeEventHandler EC_PERSONASRowDeleted;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddEC_PERSONASRow(EC_PERSONASRow row) {
                this.Rows.Add(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_PERSONASRow AddEC_PERSONASRow(decimal QUITAR, decimal TERMINALES_DEXTRAS_ID, string TERMINALES_DEXTRAS_TEXTO1, string TERMINALES_DEXTRAS_TEXTO2, string TERMINAL_NOMBRE) {
                EC_PERSONASRow rowEC_PERSONASRow = ((EC_PERSONASRow)(this.NewRow()));
                rowEC_PERSONASRow.ItemArray = new object[] {
                        QUITAR,
                        TERMINALES_DEXTRAS_ID,
                        TERMINALES_DEXTRAS_TEXTO1,
                        TERMINALES_DEXTRAS_TEXTO2,
                        TERMINAL_NOMBRE};
                this.Rows.Add(rowEC_PERSONASRow);
                return rowEC_PERSONASRow;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone() {
                EC_PERSONASDataTable cln = ((EC_PERSONASDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance() {
                return new EC_PERSONASDataTable();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnQUITAR = base.Columns["QUITAR"];
                this.columnTERMINALES_DEXTRAS_ID = base.Columns["TERMINALES_DEXTRAS_ID"];
                this.columnTERMINALES_DEXTRAS_TEXTO1 = base.Columns["TERMINALES_DEXTRAS_TEXTO1"];
                this.columnTERMINALES_DEXTRAS_TEXTO2 = base.Columns["TERMINALES_DEXTRAS_TEXTO2"];
                this.columnTERMINAL_NOMBRE = base.Columns["TERMINAL_NOMBRE"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnQUITAR = new System.Data.DataColumn("QUITAR", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnQUITAR);
                this.columnTERMINALES_DEXTRAS_ID = new System.Data.DataColumn("TERMINALES_DEXTRAS_ID", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnTERMINALES_DEXTRAS_ID);
                this.columnTERMINALES_DEXTRAS_TEXTO1 = new System.Data.DataColumn("TERMINALES_DEXTRAS_TEXTO1", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnTERMINALES_DEXTRAS_TEXTO1);
                this.columnTERMINALES_DEXTRAS_TEXTO2 = new System.Data.DataColumn("TERMINALES_DEXTRAS_TEXTO2", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnTERMINALES_DEXTRAS_TEXTO2);
                this.columnTERMINAL_NOMBRE = new System.Data.DataColumn("TERMINAL_NOMBRE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnTERMINAL_NOMBRE);
                this.columnQUITAR.ReadOnly = true;
                this.columnTERMINALES_DEXTRAS_ID.AllowDBNull = false;
                this.columnTERMINALES_DEXTRAS_TEXTO1.AllowDBNull = false;
                this.columnTERMINAL_NOMBRE.AllowDBNull = false;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_PERSONASRow NewEC_PERSONASRow() {
                return ((EC_PERSONASRow)(this.NewRow()));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) {
                return new EC_PERSONASRow(builder);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType() {
                return typeof(EC_PERSONASRow);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.EC_PERSONASRowChanged != null)) {
                    this.EC_PERSONASRowChanged(this, new EC_PERSONASRowChangeEvent(((EC_PERSONASRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.EC_PERSONASRowChanging != null)) {
                    this.EC_PERSONASRowChanging(this, new EC_PERSONASRowChangeEvent(((EC_PERSONASRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.EC_PERSONASRowDeleted != null)) {
                    this.EC_PERSONASRowDeleted(this, new EC_PERSONASRowChangeEvent(((EC_PERSONASRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.EC_PERSONASRowDeleting != null)) {
                    this.EC_PERSONASRowDeleting(this, new EC_PERSONASRowChangeEvent(((EC_PERSONASRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveEC_PERSONASRow(EC_PERSONASRow row) {
                this.Rows.Remove(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs) {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                DS_AccesosNAsig ds = new DS_AccesosNAsig();
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
                attribute2.FixedValue = "EC_PERSONASDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class EC_PERSONASRow : System.Data.DataRow {
            
            private EC_PERSONASDataTable tableEC_PERSONAS;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal EC_PERSONASRow(System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableEC_PERSONAS = ((EC_PERSONASDataTable)(this.Table));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal QUITAR {
                get {
                    try {
                        return ((decimal)(this[this.tableEC_PERSONAS.QUITARColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("El valor de la columna \'QUITAR\' de la tabla \'EC_PERSONAS\' es DBNull.", e);
                    }
                }
                set {
                    this[this.tableEC_PERSONAS.QUITARColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal TERMINALES_DEXTRAS_ID {
                get {
                    return ((decimal)(this[this.tableEC_PERSONAS.TERMINALES_DEXTRAS_IDColumn]));
                }
                set {
                    this[this.tableEC_PERSONAS.TERMINALES_DEXTRAS_IDColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string TERMINALES_DEXTRAS_TEXTO1 {
                get {
                    return ((string)(this[this.tableEC_PERSONAS.TERMINALES_DEXTRAS_TEXTO1Column]));
                }
                set {
                    this[this.tableEC_PERSONAS.TERMINALES_DEXTRAS_TEXTO1Column] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string TERMINALES_DEXTRAS_TEXTO2 {
                get {
                    try {
                        return ((string)(this[this.tableEC_PERSONAS.TERMINALES_DEXTRAS_TEXTO2Column]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("El valor de la columna \'TERMINALES_DEXTRAS_TEXTO2\' de la tabla \'EC_PERSONAS\' es " +
                                "DBNull.", e);
                    }
                }
                set {
                    this[this.tableEC_PERSONAS.TERMINALES_DEXTRAS_TEXTO2Column] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string TERMINAL_NOMBRE {
                get {
                    return ((string)(this[this.tableEC_PERSONAS.TERMINAL_NOMBREColumn]));
                }
                set {
                    this[this.tableEC_PERSONAS.TERMINAL_NOMBREColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsQUITARNull() {
                return this.IsNull(this.tableEC_PERSONAS.QUITARColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetQUITARNull() {
                this[this.tableEC_PERSONAS.QUITARColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsTERMINALES_DEXTRAS_TEXTO2Null() {
                return this.IsNull(this.tableEC_PERSONAS.TERMINALES_DEXTRAS_TEXTO2Column);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetTERMINALES_DEXTRAS_TEXTO2Null() {
                this[this.tableEC_PERSONAS.TERMINALES_DEXTRAS_TEXTO2Column] = System.Convert.DBNull;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class EC_PERSONASRowChangeEvent : System.EventArgs {
            
            private EC_PERSONASRow eventRow;
            
            private System.Data.DataRowAction eventAction;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_PERSONASRowChangeEvent(EC_PERSONASRow row, System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public EC_PERSONASRow Row {
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