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
    public class CeteBusiness
    {
        private int _pages { get; set; }
        public int Pages { get { return _pages; } }

        #region Cetes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">Archivo de cetes en base64String</param>
        /// <returns></returns>
        public bool CargarCetes(List<Cete> cetes)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCete = new RepositoryCete(uow);

                var result = repositoryCete.PersistExcelCetes(cetes);

                uow.SaveChanges();

                return result;
            }
        }

        public List<Cete> ExtraerCetes(string file)
        {
            return ParseExcelCetes(file);
        }

        public IEnumerable<Cete> ObtenerPorCriterio(IPaging paging, CeteCriterio criterio)
        {
            List<Cete> list = new List<Cete>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryCete(uow);
                list.AddRange(repository.ObtenerPorCriterio(paging, criterio.FechaInicial, criterio.FechaFinal));
                this._pages = repository.Pages;
            }

            return list;
        }
        private List<Cete> ParseExcelCetes(string file)
        {
            List<Cete> lista = new List<Cete>();
            string sheetName = "Consulta";
            int initialDataRow = 2; //en que fila inicia la lista de valores

            Cete cetes = new Cete();

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

                    //el for recorre las filas del archivo de excel hata donde haya filas con datos
                    foreach (Row row in worksheet.Descendants<Row>().Where(r => r.RowIndex.Value >= initialDataRow))
                    {
                        string value = null;
                        Cete excelRow = new Cete();

                        // Fecha
                        value = OpenXML.GetDateValue(document, row, "A", row.RowIndex.Value);
                        excelRow.Fecha = OpenXML.ValidateDateValue("Fecha", row.RowIndex.Value, value);

                        // Tasa de Rendimiento
                        value = OpenXML.GetStringValue(document, row, "B", row.RowIndex.Value);
                        excelRow.TasaRendimiento = OpenXML.ValidateDecimalValue("Tasa de Rendimiento", row.RowIndex.Value, value).Value;

                        lista.Add(excelRow);
                    }
                }
            }

            return lista;
        }

        #endregion
    }
}
