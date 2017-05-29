
using System;
using System.Configuration;
using System.Text;

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3", IsNullable = false)]
public partial class Comprobante
{
    public Comprobante()
    {
        emisorField = new ComprobanteEmisor();
        receptorField = new ComprobanteReceptor();
        conceptosField = new ComprobanteConcepto[0];
        impuestosField = new ComprobanteImpuestos();
        complementoField = new ComprobanteComplemento();
        versionField = new decimal(0);
        fechaField = new DateTime();
        selloField = String.Empty;
        formaDePagoField = String.Empty;
        noCertificadoField = new ulong();
        certificadoField = String.Empty;
        subTotalField = new decimal(0);
        descuentoField = new decimal(0);
        tipoCambioField = new decimal(0);
        monedaField = String.Empty;
        totalField = new decimal(0);
        metodoDePagoField = String.Empty;
        tipoDeComprobanteField = String.Empty;
        lugarExpedicionField = String.Empty;
    }
    private ComprobanteEmisor emisorField;

    private ComprobanteReceptor receptorField;

    private ComprobanteConcepto[] conceptosField;

    private ComprobanteImpuestos impuestosField;

    private ComprobanteComplemento complementoField;

    private decimal versionField;

    private System.DateTime fechaField;

    private string selloField;

    private string formaDePagoField;

    private ulong noCertificadoField;

    private string certificadoField;

    private decimal subTotalField;

    private decimal descuentoField;

    private decimal tipoCambioField;

    private string monedaField;

    private decimal totalField;

    private string metodoDePagoField;

    private string tipoDeComprobanteField;

    private string lugarExpedicionField;

    /// <remarks/>
    public ComprobanteEmisor Emisor
    {
        get
        {
            return this.emisorField;
        }
        set
        {
            this.emisorField = value;
        }
    }

    /// <remarks/>
    public ComprobanteReceptor Receptor
    {
        get
        {
            return this.receptorField;
        }
        set
        {
            this.receptorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Concepto", IsNullable = false)]
    public ComprobanteConcepto[] Conceptos
    {
        get
        {
            return this.conceptosField;
        }
        set
        {
            this.conceptosField = value;
        }
    }

    /// <remarks/>
    public ComprobanteImpuestos Impuestos
    {
        get
        {
            return this.impuestosField;
        }
        set
        {
            this.impuestosField = value;
        }
    }

    /// <remarks/>
    public ComprobanteComplemento Complemento
    {
        get
        {
            return this.complementoField;
        }
        set
        {
            this.complementoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime fecha
    {
        get
        {
            return this.fechaField;
        }
        set
        {
            this.fechaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string sello
    {
        get
        {
            return this.selloField;
        }
        set
        {
            this.selloField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string formaDePago
    {
        get
        {
            return this.formaDePagoField;
        }
        set
        {
            this.formaDePagoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong noCertificado
    {
        get
        {
            return this.noCertificadoField;
        }
        set
        {
            this.noCertificadoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string certificado
    {
        get
        {
            return this.certificadoField;
        }
        set
        {
            this.certificadoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal subTotal
    {
        get
        {
            return this.subTotalField;
        }
        set
        {
            this.subTotalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal descuento
    {
        get
        {
            return this.descuentoField;
        }
        set
        {
            this.descuentoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal TipoCambio
    {
        get
        {
            return this.tipoCambioField;
        }
        set
        {
            this.tipoCambioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Moneda
    {
        get
        {
            return this.monedaField;
        }
        set
        {
            this.monedaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal total
    {
        get
        {
            return this.totalField;
        }
        set
        {
            this.totalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string metodoDePago
    {
        get
        {
            return this.metodoDePagoField;
        }
        set
        {
            this.metodoDePagoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tipoDeComprobante
    {
        get
        {
            return this.tipoDeComprobanteField;
        }
        set
        {
            this.tipoDeComprobanteField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string LugarExpedicion
    {
        get
        {
            return this.lugarExpedicionField;
        }
        set
        {
            this.lugarExpedicionField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteEmisor
{
    public ComprobanteEmisor()
    {
        domicilioFiscalField=new ComprobanteEmisorDomicilioFiscal();
        expedidoEnField = new ComprobanteEmisorExpedidoEn();
        regimenFiscalField=new ComprobanteEmisorRegimenFiscal();
        rfcField = String.Empty;
        nombreField = String.Empty;
    }

    private ComprobanteEmisorDomicilioFiscal domicilioFiscalField;

    private ComprobanteEmisorExpedidoEn expedidoEnField;

    private ComprobanteEmisorRegimenFiscal regimenFiscalField;

    private string rfcField;

    private string nombreField;

    /// <remarks/>
    public ComprobanteEmisorDomicilioFiscal DomicilioFiscal
    {
        get
        {
            return this.domicilioFiscalField;
        }
        set
        {
            this.domicilioFiscalField = value;
        }
    }

    /// <remarks/>
    public ComprobanteEmisorExpedidoEn ExpedidoEn
    {
        get
        {
            return this.expedidoEnField;
        }
        set
        {
            this.expedidoEnField = value;
        }
    }

    /// <remarks/>
    public ComprobanteEmisorRegimenFiscal RegimenFiscal
    {
        get
        {
            return this.regimenFiscalField;
        }
        set
        {
            this.regimenFiscalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteEmisorDomicilioFiscal
{
    public ComprobanteEmisorDomicilioFiscal()
    {
        calleField = string.Empty;
        noExterior = 0;
        coloniaField = String.Empty;
        localidadField = String.Empty;
        municipioField = String.Empty;
        estadoField = String.Empty;
        paisField = String.Empty;
        codigoPostal = 0;
    }
    public override string ToString()
    {
        return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", calle, noExterior, colonia, localidad, municipio, estado, pais, codigoPostal);
    }

    private string calleField;

    private byte noExteriorField;

    private string coloniaField;

    private string localidadField;

    private string municipioField;

    private string estadoField;

    private string paisField;

    private ushort codigoPostalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string calle
    {
        get
        {
            return this.calleField;
        }
        set
        {
            this.calleField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte noExterior
    {
        get
        {
            return this.noExteriorField;
        }
        set
        {
            this.noExteriorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string colonia
    {
        get
        {
            return this.coloniaField;
        }
        set
        {
            this.coloniaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string localidad
    {
        get
        {
            return this.localidadField;
        }
        set
        {
            this.localidadField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string municipio
    {
        get
        {
            return this.municipioField;
        }
        set
        {
            this.municipioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string estado
    {
        get
        {
            return this.estadoField;
        }
        set
        {
            this.estadoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string pais
    {
        get
        {
            return this.paisField;
        }
        set
        {
            this.paisField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort codigoPostal
    {
        get
        {
            return this.codigoPostalField;
        }
        set
        {
            this.codigoPostalField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteEmisorExpedidoEn
{
    public ComprobanteEmisorExpedidoEn(){
        calleField = string.Empty;
        noExterior = 0;
        coloniaField = String.Empty;
        localidadField = String.Empty;
        municipioField = String.Empty;
        estadoField = String.Empty;
        paisField = String.Empty;
        codigoPostal = 0;
    }

    private string calleField;

    private byte noExteriorField;

    private string coloniaField;

    private string localidadField;

    private string municipioField;

    private string estadoField;

    private string paisField;

    private ushort codigoPostalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string calle
    {
        get
        {
            return this.calleField;
        }
        set
        {
            this.calleField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte noExterior
    {
        get
        {
            return this.noExteriorField;
        }
        set
        {
            this.noExteriorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string colonia
    {
        get
        {
            return this.coloniaField;
        }
        set
        {
            this.coloniaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string localidad
    {
        get
        {
            return this.localidadField;
        }
        set
        {
            this.localidadField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string municipio
    {
        get
        {
            return this.municipioField;
        }
        set
        {
            this.municipioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string estado
    {
        get
        {
            return this.estadoField;
        }
        set
        {
            this.estadoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string pais
    {
        get
        {
            return this.paisField;
        }
        set
        {
            this.paisField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort codigoPostal
    {
        get
        {
            return this.codigoPostalField;
        }
        set
        {
            this.codigoPostalField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteEmisorRegimenFiscal
{
    public ComprobanteEmisorRegimenFiscal()
    {
        regimenField = String.Empty;
    }
    private string regimenField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Regimen
    {
        get
        {
            return this.regimenField;
        }
        set
        {
            this.regimenField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteReceptor
{
    public ComprobanteReceptor()
    {
        domicilioField= new ComprobanteReceptorDomicilio();
        rfcField = String.Empty;
        nombreField = String.Empty;
    }

    private ComprobanteReceptorDomicilio domicilioField;

    private string rfcField;

    private string nombreField;

    /// <remarks/>
    public ComprobanteReceptorDomicilio Domicilio
    {
        get
        {
            return this.domicilioField;
        }
        set
        {
            this.domicilioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteReceptorDomicilio
{
    public ComprobanteReceptorDomicilio()
    {
        calleField = string.Empty;
        noExterior = 0;
        coloniaField = String.Empty;
        localidadField = String.Empty;
        municipioField = String.Empty;
        estadoField = String.Empty;
        paisField = String.Empty;
        codigoPostal = 0;
    }

    private string calleField;

    private byte noExteriorField;

    private string coloniaField;

    private string localidadField;

    private string municipioField;

    private string estadoField;

    private string paisField;

    private uint codigoPostalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string calle
    {
        get
        {
            return this.calleField;
        }
        set
        {
            this.calleField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte noExterior
    {
        get
        {
            return this.noExteriorField;
        }
        set
        {
            this.noExteriorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string colonia
    {
        get
        {
            return this.coloniaField;
        }
        set
        {
            this.coloniaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string localidad
    {
        get
        {
            return this.localidadField;
        }
        set
        {
            this.localidadField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string municipio
    {
        get
        {
            return this.municipioField;
        }
        set
        {
            this.municipioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string estado
    {
        get
        {
            return this.estadoField;
        }
        set
        {
            this.estadoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string pais
    {
        get
        {
            return this.paisField;
        }
        set
        {
            this.paisField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint codigoPostal
    {
        get
        {
            return this.codigoPostalField;
        }
        set
        {
            this.codigoPostalField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteConcepto
{
    public ComprobanteConcepto()
    {
        cantidadField = 0;
        unidadField = String.Empty;
        descripcionField = String.Empty;
        valorUnitarioField = new decimal(0);
        importeField = new decimal(0);
    }

    private byte cantidadField;

    private string unidadField;

    private string descripcionField;

    private decimal valorUnitarioField;

    private decimal importeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte cantidad
    {
        get
        {
            return this.cantidadField;
        }
        set
        {
            this.cantidadField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unidad
    {
        get
        {
            return this.unidadField;
        }
        set
        {
            this.unidadField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string descripcion
    {
        get
        {
            return this.descripcionField;
        }
        set
        {
            this.descripcionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal valorUnitario
    {
        get
        {
            return this.valorUnitarioField;
        }
        set
        {
            this.valorUnitarioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteImpuestos
{
    public ComprobanteImpuestos()
    {
        retencionesField = new ComprobanteImpuestosRetenciones();
        trasladosField = new ComprobanteImpuestosTraslados();
        totalImpuestosRetenidosField = 0;
        totalImpuestosTrasladadosField = 0;
    }
    private ComprobanteImpuestosRetenciones retencionesField;

    private ComprobanteImpuestosTraslados trasladosField;

    private byte totalImpuestosRetenidosField;

    private decimal totalImpuestosTrasladadosField;

    /// <remarks/>
    public ComprobanteImpuestosRetenciones Retenciones
    {
        get
        {
            return this.retencionesField;
        }
        set
        {
            this.retencionesField = value;
        }
    }

    /// <remarks/>
    public ComprobanteImpuestosTraslados Traslados
    {
        get
        {
            return this.trasladosField;
        }
        set
        {
            this.trasladosField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte totalImpuestosRetenidos
    {
        get
        {
            return this.totalImpuestosRetenidosField;
        }
        set
        {
            this.totalImpuestosRetenidosField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal totalImpuestosTrasladados
    {
        get
        {
            return this.totalImpuestosTrasladadosField;
        }
        set
        {
            this.totalImpuestosTrasladadosField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteImpuestosRetenciones
{
    public ComprobanteImpuestosRetenciones()
    {
        retencionField = new ComprobanteImpuestosRetencionesRetencion();
    }
    private ComprobanteImpuestosRetencionesRetencion retencionField;

    /// <remarks/>
    public ComprobanteImpuestosRetencionesRetencion Retencion
    {
        get
        {
            return this.retencionField;
        }
        set
        {
            this.retencionField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteImpuestosRetencionesRetencion
{
    public ComprobanteImpuestosRetencionesRetencion()
    {
        impuestoField = String.Empty;
        importeField = 0;
    }
    private string impuestoField;

    private byte importeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteImpuestosTraslados
{
    public ComprobanteImpuestosTraslados()
    {
        trasladoField= new ComprobanteImpuestosTrasladosTraslado();
    }
    private ComprobanteImpuestosTrasladosTraslado trasladoField;

    /// <remarks/>
    public ComprobanteImpuestosTrasladosTraslado Traslado
    {
        get
        {
            return this.trasladoField;
        }
        set
        {
            this.trasladoField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteImpuestosTrasladosTraslado
{
    public ComprobanteImpuestosTrasladosTraslado()
    {
        impuestoField = String.Empty;
        importeField = new decimal(0);
        tasaField = new decimal(0);
    }
    private string impuestoField;

    private decimal importeField;

    private decimal tasaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal tasa
    {
        get
        {
            return this.tasaField;
        }
        set
        {
            this.tasaField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
public partial class ComprobanteComplemento
{
    public ComprobanteComplemento()
    {
        timbreFiscalDigitalField= new TimbreFiscalDigital();
    }
    private TimbreFiscalDigital timbreFiscalDigitalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
    public TimbreFiscalDigital TimbreFiscalDigital
    {
        get
        {
            return this.timbreFiscalDigitalField;
        }
        set
        {
            this.timbreFiscalDigitalField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital", IsNullable = false)]
public partial class TimbreFiscalDigital
{
    public TimbreFiscalDigital()
    {
        versionField=new decimal(0);
        uUIDField = String.Empty;
        fechaTimbradoField=new DateTime();
        selloCFDField = String.Empty;
        noCertificadoSATField = 0;
        selloSATField = String.Empty;
    }
    private decimal versionField;

    private string uUIDField;

    private System.DateTime fechaTimbradoField;

    private string selloCFDField;

    private ulong noCertificadoSATField;

    private string selloSATField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string UUID
    {
        get
        {
            return this.uUIDField;
        }
        set
        {
            this.uUIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime FechaTimbrado
    {
        get
        {
            return this.fechaTimbradoField;
        }
        set
        {
            this.fechaTimbradoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string selloCFD
    {
        get
        {
            return this.selloCFDField;
        }
        set
        {
            this.selloCFDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong noCertificadoSAT
    {
        get
        {
            return this.noCertificadoSATField;
        }
        set
        {
            this.noCertificadoSATField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string selloSAT
    {
        get
        {
            return this.selloSATField;
        }
        set
        {
            this.selloSATField = value;
        }
    }
}

