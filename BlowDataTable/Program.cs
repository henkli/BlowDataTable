using System;
using System.Data;

namespace BlowDataTable
{
    class Program
    {
        // Change below two rows
        //public static Type ValueType = typeof(string);
        public static Type ValueType = typeof(int);
        public static double MillionsOfRows = 3; 


        static void Main(string[] args)
        {
            BlowSomethingUp();
        }

        private static void BlowSomethingUp()
        {
            var accountingDocumentNumbers = new DataTable("BKPF");
            accountingDocumentNumbers.Columns.Add(new DataColumn("MANDT", ValueType));
            accountingDocumentNumbers.Columns.Add(new DataColumn("BUKRS", ValueType));
            accountingDocumentNumbers.Columns.Add(new DataColumn("GJAHR", ValueType));
            accountingDocumentNumbers.Columns.Add(new DataColumn("BELNR", ValueType));

            var dv = accountingDocumentNumbers.DefaultView;

            dv.Sort = "MANDT, BUKRS, GJAHR, BELNR";

            AddRows(accountingDocumentNumbers, Convert.ToInt32(Math.Floor(MillionsOfRows*1000*1000)));

            Console.WriteLine("\nDone adding rows. Sorting... ");
            var target = dv.ToTable().Rows;
            if (target == null)
            {
                Console.WriteLine("table is null");
            }

            Console.WriteLine("\nDone. Nothing crashed, too bad ... Press any key.");
            Console.ReadKey();

        }

        private static void AddRows(DataTable table, int numberRows)
        {
            for (var i = 0; i < numberRows; i++)
            {
                AddRow(table);

                if (i % 100000 == 0)
                {
                    Console.WriteLine("Added row {0}", i);
                }
            }
        }

        private static void AddRow(DataTable table)
        {
            var newRow = table.NewRow();

            var value = GetValue();

            newRow["MANDT"] = value;
            newRow["BUKRS"] = value;
            newRow["GJAHR"] = value;
            newRow["BELNR"] = value;

            table.Rows.Add(newRow);
        }

        private static string Int32MaxAsString = Int32.MaxValue.ToString();

        private static object GetValue()
        {
            if (ValueType == typeof(int))
            {
                return Int32.MaxValue;
            }
            else
            {
                return Int32MaxAsString;
            }
        }
    }
}
