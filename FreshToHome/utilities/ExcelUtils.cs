
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.utilities
{
    internal class ExcelUtils
    {
        public static List<SearchProduct> ReadCreateAccountExcelData(string excelFilePath, string sheetName)
        {
            List<SearchProduct> excelDataList = new List<SearchProduct>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            SearchProduct cAExcelData = new SearchProduct()
                            {
                                ProductName = GetValueOrDefault(row, "searchText"),
                                ProductPosition = GetValueOrDefault(row,"productPosition"),
                                PhoneNumber = GetValueOrDefault(row,"mobileNumber")
                            };

                            excelDataList.Add(cAExcelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return excelDataList;
        }

        static string? GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }

}

