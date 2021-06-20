using System;
using Task3.Subjects;

namespace Task3.Vaccines
{
    internal class AvadaVaccine : IVaccine
    {
        public string Immunity => "ACTAGAACTAGGAGACCA";

        public double DeathRate => 0.2f;

        private Random randomElement = new Random(0);

        public override string ToString()
        {
            return "AvadaVaccine";
        }

        public void Vaccinate(Cat subject)
        {
            if (randomElement.NextDouble() < DeathRate)
            {
                subject.Alive = false;
                Console.WriteLine($"Cat {subject.ID} has unfortunetly died when vaccinated with {this.ToString()}");
            }
            else
                subject.Immunity = Immunity.Substring(3);
        }

        public void Vaccinate(Dog subject)
        {
            subject.Immunity = Immunity;
        }

        public void Vaccinate(Pig subject)
        {
            subject.Alive = false;
            Console.WriteLine($"Pig {subject.ID} has unfortunetly died when vaccinated with {this.ToString()}");
        }
    }
}