using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MachineLearning.PlayGround.Samples.AssociationRules
{
    public class Database
    {
        public SortedSet<Product>[] TransactionSet { get; }

        public Database(SortedSet<Product>[] transactionSet)
        {
            TransactionSet = transactionSet;
        }

        public static Database Initialize(string datasetPath)
        {
            var rawData = File.ReadAllLines(datasetPath);

            var data = rawData
                .Select(row => row.Split(','))
                .Select(row =>
                    new SortedSet<Product>(new List<Product>(row.Select(Enum.Parse<Product>))))
                    .ToArray();
            return new Database(data);
        }

        public void Print()
        {
            Console.WriteLine("Transactions:");
            foreach (var transaction in TransactionSet)
            {
                foreach (var i in transaction)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------");
        }
    }
}
