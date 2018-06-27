using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace APU.Herramientas
{
    public class ExcelHelper
    {
        internal class ColumnCaption
        {
            private static string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            private static ColumnCaption instance = null;
            private List<string> cellHeaders = null;
            public static ColumnCaption Instance
            {
                get
                {
                    if (instance == null)
                        return new ColumnCaption();
                    else return ColumnCaption.Instance;
                }
            }

            public ColumnCaption()
            {
                this.InitCollection();
            }

            private void InitCollection()
            {
                cellHeaders = new List<string>();

                foreach (string sItem in Alphabets)
                    cellHeaders.Add(sItem);

                foreach (string item in Alphabets)
                    foreach (string sItem in Alphabets)
                        cellHeaders.Add(item + sItem);
            }

            /// <summary>
            /// Returns the column caption for the given row & column index.
            /// </summary>
            /// <param name="rowIndex">Index of the row.</param>
            /// <param name="columnIndex">Index of the column.</param>
            /// <returns></returns>
            internal string Get(int rowIndex, int columnIndex)
            {
                return this.cellHeaders.ElementAt(columnIndex) + (rowIndex + 1).ToString();
            }
        }

        public string ExportToExcel(DataTable table)
        {
            string excelfile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            using (SpreadsheetDocument excelDoc = SpreadsheetDocument.Create(excelfile, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                CreateExcelParts(excelDoc, table);
            }
            return excelfile;
        }

        public string ExportToExcel<T>(List<T> list)
        {
            DataTable table = ListToDataTable(list);

            string excelfile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".xlsx";
            using (SpreadsheetDocument excelDoc = SpreadsheetDocument.Create(excelfile, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                CreateExcelParts(excelDoc, table);
            }
            return excelfile;
        }

        private void CreateExcelParts(SpreadsheetDocument spreadsheetDoc, DataTable data)
        {
            WorkbookPart workbookPart = spreadsheetDoc.AddWorkbookPart();
            CreateWorkbookPart(workbookPart);

            int workBookPartCount = 1;

            WorkbookStylesPart workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>("rId" + (workBookPartCount++).ToString());
            CreateWorkbookStylesPart(workbookStylesPart);

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + (101).ToString());
            CreateWorksheetPart(workbookPart.WorksheetParts.ElementAt(0), data);

            SharedStringTablePart sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>("rId" + (workBookPartCount++).ToString());
            CreateSharedStringTablePart(sharedStringTablePart, data);

            workbookPart.Workbook.Save();
        }

        /// <summary>
        /// Creates the shared string table part.
        /// </summary>
        /// <param name="sharedStringTablePart">The shared string table part.</param>
        /// <param name="sheetData">The sheet data.</param>
        private void CreateSharedStringTablePart(SharedStringTablePart sharedStringTablePart, DataTable sheetData)
        {
            UInt32Value stringCount = Convert.ToUInt32(sheetData.Rows.Count) + Convert.ToUInt32(sheetData.Columns.Count);

            SharedStringTable sharedStringTable = new SharedStringTable()
            {
                Count = stringCount,
                UniqueCount = stringCount
            };

            for (int columnIndex = 0; columnIndex < sheetData.Columns.Count; columnIndex++)
            {
                SharedStringItem sharedStringItem = new SharedStringItem();
                Text text = new Text();
                text.Text = sheetData.Columns[columnIndex].ColumnName;
                sharedStringItem.Append(text);
                sharedStringTable.Append(sharedStringItem);
            }

            for (int rowIndex = 0; rowIndex < sheetData.Rows.Count; rowIndex++)
            {
                SharedStringItem sharedStringItem = new SharedStringItem();
                Text text = new Text();
                text.Text = sheetData.Rows[rowIndex][0].ToString();
                sharedStringItem.Append(text);
                sharedStringTable.Append(sharedStringItem);
            }

            sharedStringTablePart.SharedStringTable = sharedStringTable;
        }

        /// <summary>
        /// Creates the worksheet part.
        /// </summary>
        /// <param name="worksheetPart">The worksheet part.</param>
        /// <param name="data">The data.</param>
        private void CreateWorksheetPart(WorksheetPart worksheetPart, DataTable data)
        {
            Worksheet worksheet = new Worksheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            worksheet.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");


            SheetViews sheetViews = new SheetViews();
            SheetView sheetView = new SheetView() { WorkbookViewId = (UInt32Value)0U };
            Selection selection = new Selection() { ActiveCell = "A1" };
            sheetView.Append(selection);
            sheetViews.Append(sheetView);

            PageMargins pageMargins = new PageMargins()
            {
                Left = 0.7D,
                Right = 0.7D,
                Top = 0.75D,
                Bottom = 0.75D,
                Header = 0.3D,
                Footer = 0.3D
            };

            SheetFormatProperties sheetFormatPr = new SheetFormatProperties()
            {
                DefaultRowHeight = 15D,
                DyDescent = 0.25D
            };

            SheetData sheetData = new SheetData();

            UInt32Value rowIndex = 1U;

            Row row1 = new Row()
            {
                RowIndex = rowIndex++,
                Spans = new ListValue<StringValue>() { InnerText = "1:3" },
                DyDescent = 0.25D
            };

            for (int columnIndex = 0; columnIndex < data.Columns.Count; columnIndex++)
            {
                Cell cell = new Cell() { CellReference = ExcelHelper.ColumnCaption.Instance.Get((Convert.ToInt32((UInt32)rowIndex) - 2), columnIndex), DataType = CellValues.String };
                CellValue cellValue = new CellValue();
                cellValue.Text = data.Columns[columnIndex].ColumnName.ToString().FormatCode();
                cell.Append(cellValue);

                row1.Append(cell);
            }
            sheetData.Append(row1);

            for (int rIndex = 0; rIndex < data.Rows.Count; rIndex++)
            {
                Row row = new Row()
                {
                    RowIndex = rowIndex++,
                    Spans = new ListValue<StringValue>() { InnerText = "1:3" },
                    DyDescent = 0.25D
                };

                for (int cIndex = 0; cIndex < data.Columns.Count; cIndex++)
                {
                    if (cIndex == 0)
                    {
                        Cell cell = new Cell() { CellReference = ExcelHelper.ColumnCaption.Instance.Get((Convert.ToInt32((UInt32)rowIndex) - 2), cIndex), DataType = CellValues.String };
                        CellValue cellValue = new CellValue();
                        cellValue.Text = data.Rows[rIndex][cIndex].ToString();
                        cell.Append(cellValue);

                        row.Append(cell);
                    }
                    else
                    {
                        Cell cell = new Cell() { CellReference = ExcelHelper.ColumnCaption.Instance.Get((Convert.ToInt32((UInt32)rowIndex) - 2), cIndex), DataType = CellValues.String };
                        CellValue cellValue = new CellValue();
                        cellValue.Text = data.Rows[rIndex][cIndex].ToString();
                        cell.Append(cellValue);

                        row.Append(cell);
                    }
                }
                sheetData.Append(row);
            }

            worksheet.Append(sheetViews);
            worksheet.Append(sheetFormatPr);
            worksheet.Append(sheetData);
            worksheet.Append(pageMargins);
            worksheetPart.Worksheet = worksheet;
        }

        /// <summary>
        /// Creates the workbook styles part.
        /// </summary>
        /// <param name="workbookStylesPart">The workbook styles part.</param>
        private void CreateWorkbookStylesPart(WorkbookStylesPart workbookStylesPart)
        {
            Stylesheet stylesheet = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            StylesheetExtensionList stylesheetExtensionList = new StylesheetExtensionList();
            StylesheetExtension stylesheetExtension = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            DocumentFormat.OpenXml.Office2010.Excel.SlicerStyles slicerStyles = new DocumentFormat.OpenXml.Office2010.Excel.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };
            stylesheetExtension.Append(slicerStyles);
            stylesheetExtensionList.Append(stylesheetExtension);

            stylesheet.Append(stylesheetExtensionList);

            workbookStylesPart.Stylesheet = stylesheet;
        }

        /// <summary>
        /// Creates the workbook part.
        /// </summary>
        /// <param name="workbookPart">The workbook part.</param>
        private void CreateWorkbookPart(WorkbookPart workbookPart)
        {
            Workbook workbook = new Workbook();
            Sheets sheets = new Sheets();

            Sheet sheet = new Sheet()
            {
                Name = "Book" + 1,
                SheetId = Convert.ToUInt32(101),
                Id = "rId" + (101).ToString()
            };
            sheets.Append(sheet);

            CalculationProperties calculationProperties = new CalculationProperties()
            {
                CalculationId = (UInt32Value)123456U  // some default Int32Value
            };

            workbook.Append(sheets);
            workbook.Append(calculationProperties);

            workbookPart.Workbook = workbook;
        }

        static void SetColumnWidth(Worksheet worksheet, uint Index, DoubleValue dwidth)
        {
            DocumentFormat.OpenXml.Spreadsheet.Columns cs = worksheet.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Columns>();
            if (cs != null)
            {
                IEnumerable<DocumentFormat.OpenXml.Spreadsheet.Column> ic = cs.Elements<DocumentFormat.OpenXml.Spreadsheet.Column>().Where(r => r.Min == Index).Where(r => r.Max == Index);
                if (ic.Count() > 0)
                {
                    DocumentFormat.OpenXml.Spreadsheet.Column c = ic.First();
                    c.Width = dwidth;
                }
                else
                {
                    DocumentFormat.OpenXml.Spreadsheet.Column c = new DocumentFormat.OpenXml.Spreadsheet.Column() { Min = Index, Max = Index, Width = dwidth, CustomWidth = true };
                    cs.Append(c);
                }
            }
            else
            {
                cs = new DocumentFormat.OpenXml.Spreadsheet.Columns();
                DocumentFormat.OpenXml.Spreadsheet.Column c = new DocumentFormat.OpenXml.Spreadsheet.Column() { Min = Index, Max = Index, Width = dwidth, CustomWidth = true };
                cs.Append(c);
                worksheet.InsertAfter(cs, worksheet.GetFirstChild<SheetFormatProperties>());
            }
        }

        #region HELPER_FUNCTIONS
        //  This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
        //  My thanks to Carl Quirion, for making it "nullable-friendly".
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }
        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) || type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }
        #endregion
    }

    public static class Extensions
    {
        public static string FormatCode(this string sourceString)
        {
            if (sourceString.Contains("<"))
                sourceString = sourceString.Replace("<", "&lt;");

            if (sourceString.Contains(">"))
                sourceString = sourceString.Replace(">", "&gt;");

            return sourceString;
        }
    }
}