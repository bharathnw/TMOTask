using System.Collections;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using TmoTask.DTO;
using TmoTask.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TmoTask.DataAccess
{
    public class DataHandler : IDataHandler
    {
        private readonly string _dataSourcePath;
        public DataHandler(IConfiguration config)
        {
            string? dataSourcePath = config["DataSourcePath"];
            if (dataSourcePath == null)
            {
                throw new ArgumentNullException("DataSourcePath is empty");
            }
            _dataSourcePath = dataSourcePath;

        }

        private CsvReader GetCsvReader()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            try
            {
                StreamReader reader = new StreamReader(_dataSourcePath);
                return new CsvReader(reader, config);
            }
            catch (FileNotFoundException ex)
            {
                throw new InvalidOperationException($"Could not find the file at the path", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while accessing the file", ex);
            }
        }

        public async Task<List<string>> GetBranchesAsync()
        {
            using (var csv = GetCsvReader())
            {
                var branches = new HashSet<string>();
                await csv.ReadAsync();
                csv.ReadHeader();
                while (await csv.ReadAsync())
                {
                    string? branch = csv.GetField<string>("Branch");
                    if (!string.IsNullOrEmpty(branch)) 
                        branches.Add(branch.Trim());
                }
                return branches.ToList();
            }
        }
        public async Task<Dictionary<(string Seller, string Month), (int OrdersCount, double TotalPrice)>> GetTopSellersAsync(string? branch = null)
        {
            using (var csv = GetCsvReader())
            {
                var sellersDict = new Dictionary<(string Seller, string Month), (int OrdersCount, double TotalPrice)>();

                //We can also use GetRecords and use LinQ GroupBy or pass Expression as parameter but it loads all data into the memory,
                //so using this approach to optimize the memory
                var data = csv.GetRecordsAsync<OrderDto>();

                await foreach (var record in data)
                {
                    if (!string.IsNullOrEmpty(branch) && record.Branch != branch)
                    {
                        continue;
                    }
                    (string seller, string month) key = (record.Seller, record.OrderDate.ToString("MMMM"));

                    if (sellersDict.ContainsKey(key))
                    {
                        var existingData = sellersDict[key];
                        sellersDict[key] = (existingData.OrdersCount + 1, existingData.TotalPrice + record.Price);
                    }
                    else
                    {
                        sellersDict[key] = (1, record.Price);
                    }
                }
                return sellersDict;
            }

        }
    }
}

