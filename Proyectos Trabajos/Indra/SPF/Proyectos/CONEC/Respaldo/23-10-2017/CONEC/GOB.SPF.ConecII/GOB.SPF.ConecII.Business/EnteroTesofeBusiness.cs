using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Amatzin;
using GOB.SPF.ConecII.Entities.DTO;
using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    public class EnteroTesofeBusiness
    {
        private int _pages { get; set; }
        public int Pages => _pages;

        public bool CargarEnteroTesofe(List<EnteroTesofe> lista)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryEnteroTesofe = new RepositoryEnteroTesofe(uow);
                              
                var result = repositoryEnteroTesofe.PersistExcelEnterostesofe(lista);

                uow.SaveChanges();

                return result;
            }
        }

        public List<EnteroTesofe> ExtraerEnteroTesofe(string file)
        {
            return ParseExcelEnteroTesofe(file);
        }

        private List<EnteroTesofe> ParseExcelEnteroTesofe(string file)
        {
            List<EnteroTesofe> lista = new List<EnteroTesofe>();

            string sheetName = "Detalle 1";
            int initialDataRow = 2;

            byte[] bytearray = Convert.FromBase64String(file);
            using (Stream stream = new MemoryStream(bytearray))
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false))
                {
                    Sheet sheet = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();
                    if (sheet == null)
                    {
                        throw new ConecException(String.Format("No existe la hoja con el nombre {0}", sheetName));
                    }

                    WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);
                    Worksheet worksheet = worksheetPart.Worksheet;

                    foreach (Row row in worksheet.Descendants<Row>().Where(r => r.RowIndex.Value >= initialDataRow))
                    {

                        EnteroTesofe excelRow = new EnteroTesofe();

                        string value = null;

                        // Numero de Operacion
                        value = OpenXML.GetStringValue(document, row, "A", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Numero de Operación", row.RowIndex.Value, value);
                        excelRow.NumeroOperacion = Convert.ToInt64(value);

                        // RFC
                        value = OpenXML.GetStringValue(document, row, "B", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("RFC", row.RowIndex.Value, value);
                        excelRow.RFC = value;

                        // CURP
                        value = OpenXML.GetStringValue(document, row, "C", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("CURP", row.RowIndex.Value, value);
                        excelRow.CURP = value;

                        // Razon Social
                        value = OpenXML.GetStringValue(document, row, "D", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Razon Social", row.RowIndex.Value, value);
                        excelRow.RazonSocial = value;

                        // Fecha de Presentación
                        value = OpenXML.GetDateValue(document, row, "E", row.RowIndex.Value);
                        var fecha = OpenXML.ValidateDateValue("Fecha de Presentación", row.RowIndex.Value, value);

                        // Hora Pago    
                        value = OpenXML.GetStringValue(document, row, "F", row.RowIndex.Value);
                        var hora = OpenXML.ValidateTimeSpanValue("Hora Pago", row.RowIndex.Value, value);

                        excelRow.FechaPresentacion = Convert.ToDateTime(String.Format("{0} {1}", fecha.ToShortDateString(), hora.ToString()));

                        //Sucursal
                        value = OpenXML.GetStringValue(document, row, "G", row.RowIndex.Value);
                        excelRow.Sucursal = OpenXML.ValidateIntegerValue("Sucursal", row.RowIndex.Value, value);

                        // Lave Pago
                        value = OpenXML.GetStringValue(document, row, "H", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Llave Pago", row.RowIndex.Value, value);
                        excelRow.LlavePago = value;

                        // Banco
                        value = OpenXML.GetStringValue(document, row, "I", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Banco", row.RowIndex.Value, value);
                        excelRow.Banco = value;

                        // Medio recepcion
                        value = OpenXML.GetStringValue(document, row, "J", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Medio de Recepción", row.RowIndex.Value, value);
                        excelRow.MedioRecepcion = value;

                        // Dependencia
                        value = OpenXML.GetStringValue(document, row, "K", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Dependencia", row.RowIndex.Value, value);
                        excelRow.Dependencia = value;

                        // Periodo
                        value = OpenXML.GetStringValue(document, row, "L", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Periodo", row.RowIndex.Value, value);
                        excelRow.Periodo = value;

                        // Saldo a Favor
                        value = OpenXML.GetStringValue(document, row, "M", row.RowIndex.Value);
                        excelRow.SaldoFavor = OpenXML.ValidateDecimalValue("Saldo a Favor", row.RowIndex.Value, value).Value;

                        // Importe
                        value = OpenXML.GetStringValue(document, row, "N", row.RowIndex.Value);
                        excelRow.Importe = OpenXML.ValidateDecimalValue("Importe", row.RowIndex.Value, value).Value;

                        // Parte Actualizada
                        value = OpenXML.GetStringValue(document, row, "O", row.RowIndex.Value);
                        excelRow.ParteActualizada = OpenXML.ValidateDecimalValue("Parte Actualizada", row.RowIndex.Value, value).Value;

                        // Recargos
                        value = OpenXML.GetStringValue(document, row, "P", row.RowIndex.Value);
                        excelRow.Recargos = OpenXML.ValidateDecimalValue("Recargos", row.RowIndex.Value, value).Value;

                        // Multa Correccion
                        value = OpenXML.GetStringValue(document, row, "Q", row.RowIndex.Value);
                        excelRow.MultaCorreccion = OpenXML.ValidateDecimalValue("Multa Por Corrección", row.RowIndex.Value, value).Value;

                        // Compensacion
                        value = OpenXML.GetStringValue(document, row, "R", row.RowIndex.Value);
                        excelRow.Compensacion = OpenXML.ValidateDecimalValue("Compensacion", row.RowIndex.Value, value).Value;

                        // Cantidad Pagada
                        value = OpenXML.GetStringValue(document, row, "S", row.RowIndex.Value);
                        excelRow.CantidadPagada = OpenXML.ValidateDecimalValue("Cantidad Pagada", row.RowIndex.Value, value).Value;

                        // Clave de Ref. del DPA
                        value = OpenXML.GetStringValue(document, row, "T", row.RowIndex.Value);
                        excelRow.ClaveReferenciaDPA = OpenXML.ValidateIntegerValue("Clave de Ref. del DPA", row.RowIndex.Value, value);

                        // Cadena Dependencia
                        value = OpenXML.GetStringValue(document, row, "U", row.RowIndex.Value);
                        OpenXML.ValidateStringValue("Cadena de la Dependencia", row.RowIndex.Value, value);
                        excelRow.CadenaDependencia = value;

                        // Importe IVA NO REQUERIDO
                        value = OpenXML.GetStringValue(document, row, "V", row.RowIndex.Value);
                        excelRow.ImporteIVA = OpenXML.ValidateDecimalValue("Importe IVA", row.RowIndex.Value, value, false);

                        // Parte Actualizada IVA NO REQUERIDO
                        value = OpenXML.GetStringValue(document, row, "W", row.RowIndex.Value);
                        excelRow.ParteActualizadaIVA = OpenXML.ValidateDecimalValue("Parte Actualizada IVA", row.RowIndex.Value, value, false);

                        // Recargos IVA NO REQUERIDO
                        value = OpenXML.GetStringValue(document, row, "X", row.RowIndex.Value);
                        excelRow.RecargosIVA = OpenXML.ValidateDecimalValue("Recargo IVA", row.RowIndex.Value, value, false);

                        //Multa por Correccion IVA NO REQUERIDO
                        value = OpenXML.GetStringValue(document, row, "Y", row.RowIndex.Value);
                        excelRow.MultaCorreccionIVA = OpenXML.ValidateDecimalValue("Multa por Correcion IVA", row.RowIndex.Value, value, false);

                        // Cantidad Pagada IVA
                        value = OpenXML.GetStringValue(document, row, "Z", row.RowIndex.Value);
                        excelRow.CantidadPagadaIVA = OpenXML.ValidateDecimalValue("Cantidad Pagada IVA", row.RowIndex.Value, value).Value;

                        // Total Efectivamente Pagado
                        value = OpenXML.GetStringValue(document, row, "AA", row.RowIndex.Value);
                        excelRow.TotalEfectivamentePagado = OpenXML.ValidateDecimalValue("Total Efectivamente Pagado", row.RowIndex.Value, value).Value;

                        lista.Add(excelRow);

                    }
                }
            }

            return lista;
        }


        public IEnumerable<EnteroTesofe> ObtenerPorCriterio(IPaging paging, EnteroTesofeCriterio criterio)
        {
            var list = new List<EnteroTesofe>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryEnteroTesofe(uow);
                list.AddRange(repository.ObtenerPorCriterio(paging, criterio));
                _pages = repository.Pages;
            }

            return list;
        }

    }
}
