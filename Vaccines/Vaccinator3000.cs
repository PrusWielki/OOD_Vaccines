using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class Vaccinator3000 : IVaccine
    {
        public string Immunity => "ACTG";
        public double DeathRate => 0.1f;

        private Random randomElement = new Random(0);
        public override string ToString()
        {
            return "Vaccinator3000";
        }

        public void Vaccinate(Cat subject)
        {
            if (randomElement.NextDouble() < DeathRate)
            {
                subject.Alive = false;
                Console.WriteLine($"Cat {subject.ID} has unfortunetly died when vaccinated with {this}");
            }
            else
            {
                for (int i = 0; i < 300; i++)
                {
                    subject.Immunity += Immunity[randomElement.Next() % Immunity.Length];
                }
            }
        }

        public void Vaccinate(Dog subject)
        {
            if (randomElement.NextDouble() < DeathRate)
            {
                subject.Alive = false;
                Console.WriteLine($"Dog {subject.ID} has unfortunetly died when vaccinated with {this}");
            }
            else
            {
                for(int i = 0; i < 3000; i++)
                {
                    subject.Immunity += Immunity[randomElement.Next() % Immunity.Length];
                }
            }
        }

        public void Vaccinate(Pig subject)
        {
            if (randomElement.NextDouble() < 3*DeathRate)
            {
                subject.Alive = false;
                Console.WriteLine($"Pig {subject.ID} has unfortunetly died when vaccinated with {this}");
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    subject.Immunity += Immunity[randomElement.Next() % Immunity.Length];
                }
            }
        }
    }
}
