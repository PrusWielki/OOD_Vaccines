using System;
using System.Collections.Generic;
using Task3.Subjects;
using Task3.Vaccines;

//I declare that this piece of work which is the basis for recognition of achieving learning outcomes in the course was completed on my own.
//Patryk Prusak 305794

namespace Task3
{
    internal class Program
    {
        public class MediaOutlet
        {
            public void Publish(Iiterator iterator)
            {
                do
                {
                    Console.Write(iterator.Current());
                } while (iterator.Next());
            }
        }



        //public static Iterator_SD GetIterator(SimpleDatabase simpleDatabase)
        //{
        //    return new Iterator_SD()
        //}
        public class Tester
        {
            public void Test(MediaOutlet mediaOutlet, Iiterator iterator)
            {
                var vaccines = new List<IVaccine>() { new AvadaVaccine(), new Vaccinator3000(), new ReverseVaccine() };

                foreach (var vaccine in vaccines)
                {
                    Console.WriteLine($"Testing {vaccine}");
                    var subjects = new List<ISubject>();
                    int n = 5;

                    for (int i = 0; i < n; i++)
                    {
                        subjects.Add(new Cat($"{i}"));
                        subjects.Add(new Dog($"{i}"));
                        subjects.Add(new Pig($"{i}"));
                    }

                    foreach (var subject in subjects)
                    {
                        subject.GetVaccinated(vaccine);
                        //avadaVaccine.Vaccinate(subject);
                    }

                    //var genomeDatabase = Generators.PrepareGenomes();
                    //var simpleDatabase = Generators.PrepareSimpleDatabase(genomeDatabase);
                    //var excellDatabase = Generators.PrepareExcellDatabase(genomeDatabase);
                    //var overcomplicatedDatabase = Generators.PrepareOvercomplicatedDatabase(genomeDatabase);
                    //var iterator = new Iterator_OD(overcomplicatedDatabase, genomeDatabase);
                    //var iterator_simpleDatabase = new Iterator_SD(simpleDatabase, genomeDatabase);
                    //var itr = new DeathRateFilterBigger(iterator, 15);

                    //var itr = new ConcatDatabaseItr(iterator, iterator2);
                    //var itr = new VirusDataIncrementDeathRate(iterator, -10);
                    //mediaOutlet.Publish(iterator2);
                    //iterator2.Reset();
                    //mediaOutlet.Publish(iterator2);

                    // iteration over SimpleGenomeDatabase using solution from 1)
                    // subjects should be tested here using GetTested function

                    // iterating over simpleDatabase
                    mediaOutlet.Publish(iterator);
                    iterator.Reset();

                    foreach (var subject in subjects)
                    {
                        do
                        {
                            //if (iterator.GetVirusData() != null)
                            subject.GetTested(iterator.GetVirusData());
                        } while (iterator.Next());
                        iterator.Reset();
                    }

                    int aliveCount = 0;
                    foreach (var subject in subjects)
                    {
                        if (subject.Alive) aliveCount++;
                    }
                    Console.WriteLine($"{aliveCount} alive!");
                }
            }
        }

        public static void Main(string[] args)
        {
            var genomeDatabase = Generators.PrepareGenomes();
            var simpleDatabase = Generators.PrepareSimpleDatabase(genomeDatabase);
            var excellDatabase = Generators.PrepareExcellDatabase(genomeDatabase);
            var overcomplicatedDatabase = Generators.PrepareOvercomplicatedDatabase(genomeDatabase);
            var mediaOutlet = new MediaOutlet();


            // var iterator = new DecoratorVirusData(new DecoratorVirusData(new Iterator_ED(excellDatabase, genomeDatabase), f => f.DeathRate > 15),f=>f.DeathRate<30);
           // var iterator = new DecoratorVirusData(new MapperVirusData(new Iterator_ED(excellDatabase, genomeDatabase), f => new VirusData(f.VirusName, f.DeathRate + 10, f.InfectionRate, f.Genomes)), f => f.DeathRate > 15);
            var task2_1 = new DecoratorVirusData(new Iterator_ED(excellDatabase, genomeDatabase), f => f.DeathRate > 15); 
            var task2_2= new DecoratorVirusData(new MapperVirusData(new Iterator_ED(excellDatabase, genomeDatabase), f => new VirusData(f.VirusName, f.DeathRate + 10, f.InfectionRate, f.Genomes)), f => f.DeathRate > 15);
            var task2_3 = new ConcatDatabaseItr(new Iterator_ED(excellDatabase, genomeDatabase), new Iterator_OD(overcomplicatedDatabase, genomeDatabase));
            //var iterator = new DecoratorVirusData(new Iterator_SD(simpleDatabase, genomeDatabase), f => f.DeathRate < 0.1);
            // testing animals
            var tester = new Tester();
            Console.WriteLine("Task2_1:");
            mediaOutlet.Publish(task2_1);
            Console.WriteLine("Task2_2:");
            mediaOutlet.Publish(task2_2);
            Console.WriteLine("Task2_3:");
            mediaOutlet.Publish(task2_3);

           // mediaOutlet.Publish(iterator);
            //tester.Test(mediaOutlet, iterator);
            tester.Test(mediaOutlet, new Iterator_SD(simpleDatabase, genomeDatabase));
            //tester.Test(mediaOutlet, new Iterator_ED(excellDatabase, genomeDatabase));
            //tester.Test(mediaOutlet, new Iterator_OD(overcomplicatedDatabase, genomeDatabase));
        }
    }
}