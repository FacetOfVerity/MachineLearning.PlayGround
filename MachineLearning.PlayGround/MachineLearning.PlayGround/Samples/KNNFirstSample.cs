using System;
using System.IO;
using System.Linq;
using Accord.MachineLearning;

namespace MachineLearning.PlayGround.Samples
{
    public class KnnFirstSample
    {
        public static void Start()
        {
            var rawData = File.ReadAllLines("myData.csv");

            var data = rawData.Skip(1).Select(row => row.Split(',')).Select(row => new Row
            {
                Gender = Enum.Parse<Gender>(row[0]),
                Height = double.Parse(row[1]),
                Weight = double.Parse(row[2])
            }).ToArray();

            Console.WriteLine("Данные:");
            foreach (var row in data)
            {
                Console.WriteLine(row);
            }

            var classifier = new KNearestNeighbors(1);
            var values = data.Select(a => new double[] { a.Height, a.Weight }).ToArray();
            var cases = data.Select(a => (int)a.Gender).ToArray();
            classifier.Learn(values, cases);

            Console.WriteLine("\nТест:");
            var decision = classifier.Decide(new double[] { 160, 65 });
            Console.WriteLine($"Ответ классификатора: {(Gender)decision}");
        }
    }
}
