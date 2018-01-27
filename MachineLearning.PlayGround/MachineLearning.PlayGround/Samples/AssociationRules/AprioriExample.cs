using System;
using System.Collections.Generic;
using Accord.MachineLearning.Rules;

namespace MachineLearning.PlayGround.Samples.AssociationRules
{
    public class AprioriExample
    {
        public static void Start()
        {
            var db = Database.Initialize("transactions.csv");
            db.Print();

            var apriori = new Apriori<Product>(threshold: 3, confidence: .5);
            var classifier = apriori.Learn(db.TransactionSet);

            Console.WriteLine("Rules:");
            foreach (var associationRule in classifier.Rules)
            {
                Console.WriteLine(associationRule);
            }
        }
    }
}
