using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace GOB.SPF.ConecII.Business
{
    public class OpenXML
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://stackoverflow.com/questions/5115257/open-xml-excel-read-cell-value
        /// </remarks>
        public static string GetTextFromSharedTable(SpreadsheetDocument document, int index)
        {
            return document.WorkbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(Convert.ToInt32(index)).InnerText;
        }

        public static Cell GetCell(Row row, string column, int rowIndex)
        {
            return row.Descendants<Cell>().Where(c => c.CellReference == String.Format("{0}{1}", column, rowIndex)).FirstOrDefault();
        }
        public static string GetStringValue(SpreadsheetDocument document, Row row, string column, uint rowIndex)
        {
            //numer de operacion
            string value = null;
            var cell = GetCell(row, column, Convert.ToInt32(row.RowIndex.Value));

            if (cell != null)
            {
                if (cell.DataType == null)
                {
                    value = cell.CellValue.Text;
                }
                else if (cell.DataType == "s")
                {
                    //es string, entonces obenerlo de la sharedtable
                    value = GetTextFromSharedTable(document, Convert.ToInt32(cell.CellValue.Text));
                }
            }

            return value;
        }
        public static string GetDateValue(SpreadsheetDocument document, Row row, string column, uint rowIndex)
        {
            //numer de operacion
            string value = null;
            var cell = GetCell(row, column, Convert.ToInt32(row.RowIndex.Value));

            if (cell.DataType == null)
            {

                //es tipo fecha obtener la fecha con FromOADate
                if (!String.IsNullOrEmpty(cell.CellValue.Text))
                {
                    value = DateTime.FromOADate(double.Parse(cell.CellValue.InnerXml)).ToLongDateString();
                }
            }
            else if (cell.DataType == "s")
            {
                //es string, entonces obenerlo de la sharedtable
                value = GetTextFromSharedTable(document, Convert.ToInt32(cell.CellValue.Text));
            }

            return value;
        }
        public static void ValidateStringValue(string field, uint rowIndex, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ConecException(String.Format("Error en el campo {0} fila {1}", field, rowIndex));
            }
        }
        public static DateTime ValidateDateValue(string field, uint rowIndex, string value)
        {
            DateTime date;
            if (!DateTime.TryParse(value, out date))
            {
                throw new ConecException(String.Format("Error en el campo {0} fila {1}", field, rowIndex));
            }

            return date;
        }
        public static TimeSpan ValidateTimeSpanValue(string field, uint rowIndex, string value)
        {
            TimeSpan time;
            if (!TimeSpan.TryParse(value, out time))
            {
                throw new ConecException(String.Format("Error en el campo {0} fila {1}", field, rowIndex));
            }

            return time;
        }
        public static int ValidateIntegerValue(string field, uint rowIndex, string value)
        {
            int intValue;
            if (!int.TryParse(value, out intValue))
            {
                throw new ConecException(String.Format("Error en el campo {0} fila {1}", field, rowIndex));
            }

            return intValue;
        }
        public static decimal? ValidateDecimalValue(string field, uint rowIndex, string value, bool required = true)
        {
            decimal? decimalValue = null;
            decimal temp;

            if (!required)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return null;
                }
                else
                {
                    if (decimal.TryParse(value, out temp))
                    {
                        decimalValue = temp;
                    }
                    else
                    {
                        throw new ConecException(String.Format("Error en el campo {0} fila {1}", field, rowIndex));
                    }
                }
            }
            else
            {
                if (decimal.TryParse(value, out temp))
                {
                    decimalValue = temp;
                }
                else
                {
                    throw new ConecException(String.Format("Error en el campo {0} fila {1}", field, rowIndex));
                }
            }


            return decimalValue;
        }

       

    }
}
