using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAB4.Prosumers;


namespace DAB4
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ReadKey();
                Console.Clear();
                RunProsumers();
            }

            void RunProsumers()
            {
                int privateProduced = 0;
                int privateConsumed = 0;
                int businessProduced = 0;
                int businessConsumed = 0;
                int villageProduced = 0;
                int villageConsumed = 0;
                int villageDifference = 0;

                Random rnd = new Random();

                List<Prosumer> pl = new List<Prosumer>();
                List<Prosumer> bl = new List<Prosumer>();
                Prosumer nationalProsumer = new Prosumer(45, "National power grid");

                //Set up private prosumers
                for (int i = 0; i < 33; i++)
                {
                    Prosumer privateProsumer = new Prosumer(i, "Private");
                    privateProsumer.ConsumedkW = rnd.Next(0, 100);
                    privateProsumer.ProducedkW = rnd.Next(0, 100);
                    privateProsumer.DifferencekW = privateProsumer.ProducedkW - privateProsumer.ConsumedkW;
                    pl.Add(privateProsumer);
                }

                //Set up business prosumers
                for (int j = 33; j < 45; j++)
                {
                    Prosumer businessProsumer = new Prosumer(j, "Business");
                    businessProsumer.ConsumedkW = rnd.Next(0, 500);
                    businessProsumer.ProducedkW = rnd.Next(0, 500);
                    businessProsumer.DifferencekW = businessProsumer.ProducedkW - businessProsumer.ConsumedkW;
                    bl.Add(businessProsumer);
                }


                //should be whatever is leftover from the prosumers
                nationalProsumer.ConsumedkW = 0;

                //should be whatever the prosumers need
                nationalProsumer.ProducedkW = 0;

                pl.ForEach(item => Console.WriteLine(item.Type + " with id: " + "\t" + item.Id + "\t" + " consumed: " + "\t" + item.ConsumedkW + "\t" + " and produced: " + "\t" + item.ProducedkW + "\t" + "difference is: " + item.DifferencekW));
                pl.ForEach(item => privateConsumed += item.ConsumedkW);
                pl.ForEach(item => privateProduced += item.ProducedkW);

                Console.WriteLine();

                bl.ForEach(item => Console.WriteLine(item.Type + " with id: " + "\t" + item.Id + "\t" + " consumed: " + "\t" + item.ConsumedkW + "\t" + " and produced: " + "\t" + item.ProducedkW + "\t" + "difference is: " + item.DifferencekW));
                bl.ForEach(item => businessConsumed += item.ConsumedkW);
                bl.ForEach(item => businessProduced += item.ProducedkW);

                Console.WriteLine("\nPrivate sector consumed: " + privateConsumed);
                Console.WriteLine("Private sector produced: " + privateProduced);
                Console.WriteLine("\nBusiness sector consumed: " + businessConsumed);
                Console.WriteLine("Business sector produced: " + businessProduced);

                villageConsumed = privateConsumed + businessConsumed;
                villageProduced = privateProduced + businessProduced;

                if (villageConsumed < villageProduced)
                {
                    nationalProsumer.ProducedkW = villageProduced - villageConsumed;
                }

                if (villageConsumed > villageProduced)
                {
                    nationalProsumer.ConsumedkW = villageConsumed - villageProduced;
                }

                Console.WriteLine("\nNational power grid consumed: " + nationalProsumer.ConsumedkW + " and produced: " + nationalProsumer.ProducedkW);

                villageDifference = villageProduced - villageConsumed;

                Console.WriteLine(villageDifference);
            }
        }
    }
}
