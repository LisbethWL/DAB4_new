﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAB4_new_console;
using Newtonsoft.Json;


namespace DAB4_new_console
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Prosumer> positiveList = new List<Prosumer>();
            List<Prosumer> negativeList = new List<Prosumer>();

            while (true)
            {
                RunProsumers();

                Console.WriteLine("Positive list: ");
                foreach (var v in positiveList)
                {
                    Console.Write(v.DifferencekW + " ");
                }

                Console.WriteLine("\nNegative list: ");
                foreach (var v in negativeList)
                {
                    Console.Write(v.DifferencekW + " ");
                }

                ProsumerTrades();
                Console.ReadKey();
                Console.Clear();
            }

            void ProsumerTrades()
            {
                for (int i = 0; i < negativeList.Count; i++)
                {
                    int mangler = negativeList[i].DifferencekW;
                    for (int j = 0; j < positiveList.Count; j++)
                    {
                        if (positiveList[j].DifferencekW > mangler && mangler!=0)
                        {
                            Console.WriteLine(positiveList[j].Id + " traded " + mangler + " with " + negativeList[i].Id);
                            TraderInfoRestConsumer.Post(new TradeInfoModel()
                            {
                                Token = mangler,
                                BitCoin = 0,
                                sellerId = negativeList[i].Id,
                                buyerId = positiveList[j].Id,
                                amount = 0
                            }).Wait();
                            positiveList[j].DifferencekW = positiveList[j].DifferencekW - mangler;
                            mangler = 0;
                        }
                        else
                        {
                            Console.WriteLine(positiveList[j].Id + " traded " + positiveList[j].DifferencekW + " with " + negativeList[i].Id);
                            TraderInfoRestConsumer.Post(new TradeInfoModel()
                            {
                                Token = mangler,
                                BitCoin = 0,
                                sellerId = negativeList[i].Id,
                                buyerId = positiveList[j].Id,
                                amount = 0
                            }).Wait();
                            mangler = mangler - positiveList[j].DifferencekW;
                            positiveList[j].DifferencekW = 0;
                        }
                    }
                }

                int negativeSum = negativeList.Sum(x => x.DifferencekW);
                int positiveSum = positiveList.Sum(x => x.DifferencekW);
            }

            void RunProsumers()
            {
                foreach (var v in ProsumerRestConsumer.GetAll().Result)
                {
                    ProsumerRestConsumer.Delete(v.Id).Wait();
                }

                int privateProduced = 0;
                int privateConsumed = 0;
                int businessProduced = 0;
                int businessConsumed = 0;
                int villageProduced = 0;
                int villageConsumed = 0;
                int villageDifference = 0;

                List<Prosumer> pl = new List<Prosumer>();
                List<Prosumer> bl = new List<Prosumer>();

                Random rnd = new Random();

                
                Prosumer nationalProsumer = new Prosumer(45, "National power grid");

                //Set up private prosumers
                for (int i = 0; i < 33; i++)
                {
                    Prosumer privateProsumer = new Prosumer(i, "Private");
                    privateProsumer.ConsumedkW = rnd.Next(1, 100);
                    privateProsumer.ProducedkW = rnd.Next(1, 100);
                    privateProsumer.DifferencekW = privateProsumer.ProducedkW - privateProsumer.ConsumedkW;
                    pl.Add(privateProsumer);

                    if (privateProsumer.DifferencekW < 0)
                    {
                        negativeList.Add(privateProsumer);
                    }
                    else
                    {
                        positiveList.Add(privateProsumer);
                    }
                    ProsumerRestConsumer.Post(privateProsumer).Wait();
                }

                //Set up business prosumers
                for (int j = 33; j < 45; j++)
                {
                    Prosumer businessProsumer = new Prosumer(j, "Business");
                    businessProsumer.ConsumedkW = rnd.Next(1, 500);
                    businessProsumer.ProducedkW = rnd.Next(1, 500);
                    businessProsumer.DifferencekW = businessProsumer.ProducedkW - businessProsumer.ConsumedkW;

                    if (businessProsumer.DifferencekW < 0)
                    {
                        negativeList.Add(businessProsumer);
                    }
                    else
                    {
                        positiveList.Add(businessProsumer);
                    }
                    bl.Add(businessProsumer);
                    ProsumerRestConsumer.Post(businessProsumer).Wait();
                    
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

                ProsumerRestConsumer.Post(nationalProsumer).Wait();
            }
        }
    }
}
