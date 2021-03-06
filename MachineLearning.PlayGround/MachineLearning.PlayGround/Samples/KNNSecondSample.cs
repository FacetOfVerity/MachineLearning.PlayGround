﻿using System;
using System.IO;
using System.Linq;
using Accord.MachineLearning;

namespace MachineLearning.PlayGround.Samples
{
    public class KnnSecondSample
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

            var train = data.Take(12).ToArray();
            var test = data.Skip(12).Take(6).ToArray();

            var values = train.Select(a => new double[] { a.Height, a.Weight }).ToArray();
            var cases = train.Select(a => (int)a.Gender).ToArray();

            var classifier = new KNearestNeighbors(1);
            classifier.Learn(values, cases);

            Console.WriteLine("\nТест:");
            foreach (var row in test)
            {
                Console.WriteLine(row);
                var tr = new double[] { row.Height, row.Weight };

                var decision = classifier.Decide(tr);
                Console.WriteLine($"Ответ классификатора: {(Gender)decision}");
            }
        }
    }
}
