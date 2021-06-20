using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class ReverseVaccine : IVaccine
    {
        public string Immunity => "ACTGAGACAT";

        public double DeathRate => 0.05f;

        Dictionary<string, int> timesused=new Dictionary<string, int>();

        private Random randomElement = new Random(0);

        
        public override string ToString()
        {
            return "ReverseVaccine";
        }

        public void Vaccinate(Cat subject)
        {
            if(!timesused.ContainsKey(subject.ID))
            {
                timesused.Add(subject.ID, 0);
            }
            subject.Alive = false;
            Console.WriteLine($"Cat {subject.ID} has unfortunetly died when vaccinated with {this}");
            timesused[subject.ID]++;
           
        }

        public void Vaccinate(Dog subject)
        {
            if (!timesused.ContainsKey(subject.ID))
            {
                timesused.Add(subject.ID, 0);
            }
            char[] toreverse = Immunity.ToCharArray();
            Array.Reverse(toreverse);
            subject.Immunity = new string(toreverse);
            timesused[subject.ID]++;
        }

        public void Vaccinate(Pig subject)
        {
            if (!timesused.ContainsKey(subject.ID))
            {
                timesused.Add(subject.ID, 0);
            }
            if (randomElement.NextDouble() < DeathRate * timesused[subject.ID])
            {
                subject.Alive = false;
                Console.WriteLine($"Pig {subject.ID} has unfortunetly died when vaccinated with {this}");
            }
            else
            {
                char[] toreverse = Immunity.ToCharArray();
                Array.Reverse(toreverse);
                subject.Immunity = Immunity + new string(toreverse);
            }
            timesused[subject.ID]++;
        }
    }
}
