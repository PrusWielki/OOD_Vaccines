using System;
using System.Collections.Generic;

namespace Task3
{
    public interface Iiterator
    {
        string Current();

        VirusData GetVirusData();

        void Reset();

        IReadOnlyList<GenomeData> GetGenomes();

        bool Next();
    }

    //enum DatabaseType
    //{
    //    SimpleDatabase,
    //    ExcelDatabse,
    //    OvercomplicatedDatabse
    //}
    //internal abstract class Iterator : Iiterator
    //{
    //    int index=0;
    //    public Iterator()
    //    {
    //    }

    //    abstract public IReadOnlyList<GenomeData> GetGenomes();

    //    abstract public bool Next();

    //    abstract public string Current();

    //    abstract public VirusData GetVirusData();

    //    abstract public void Reset();
    //}

    internal class Iterator_SGD : Iiterator //Iterator
    {
        private int index = 0;
        private readonly SimpleGenomeDatabase simpleGenomeDatabase;

        public Iterator_SGD(SimpleGenomeDatabase _simpleGenomeDatabase)
        {
            simpleGenomeDatabase = _simpleGenomeDatabase;
        }

        public bool Next()
        {
            if (index < simpleGenomeDatabase.genomeDatas.Count - 1)
            {
                index++;
                return true;
            }
            else return false;
        }

        public string Current()
        {
            return simpleGenomeDatabase.genomeDatas[index].ToString() + '\n';
        }

        public IReadOnlyList<GenomeData> GetGenomes()
        {
            return simpleGenomeDatabase.genomeDatas;
        }

        public VirusData GetVirusData()
        {
            return null;
        }

        public void Reset()
        {
            index = 0;
        }
    }

    internal class Iterator_SD : Iiterator //Iterator
    {
        private readonly SimpleDatabase simpleDatabase;
        private readonly SimpleGenomeDatabase simpleGenomeDatabase;
        private List<VirusData> virusDatas;
        private int index = 0;

        public Iterator_SD(SimpleDatabase _simpleDatabase) : base()
        {
            simpleDatabase = _simpleDatabase; simpleGenomeDatabase = null;
            virusDatas = new List<VirusData>();
            virusDatas.Add(GetVirusData());
        }

        public Iterator_SD(SimpleDatabase _simpleDatabase, SimpleGenomeDatabase _simpleGenomeDatabase) : base()
        {
            simpleDatabase = _simpleDatabase; simpleGenomeDatabase = _simpleGenomeDatabase;
            virusDatas = new List<VirusData>();
            virusDatas.Add(GetVirusData());
        }

        public bool Next()
        {
            if (index < simpleDatabase.Rows.Count - 1)
            {
                index++;
                virusDatas.Add(GetVirusData());
                return true;
            }
            else return false;
        }

        public string Current()
        {
            if (simpleGenomeDatabase == null)
                return new string($"Row: {index}:\n\tVirus name: {simpleDatabase.Rows[index].VirusName}\n\tInfection rate: {simpleDatabase.Rows[index].InfectionRate}\n\tDeath rate: {simpleDatabase.Rows[index].DeathRate}\n\tGenome Id: {simpleDatabase.Rows[index].GenomeId}\n");
            else
            {
                //var temp = new String($"Row: {index}:\n\tVirus name: {simpleDatabase.Rows[index].VirusName}\n\tInfection rate: {simpleDatabase.Rows[index].InfectionRate}\n\tDeath rate: {simpleDatabase.Rows[index].DeathRate}\n\t");
                //for (int i = 0; i < simpleGenomeDatabase.genomeDatas.Count; i++)
                //{
                //    if (simpleGenomeDatabase.genomeDatas[i].Id == simpleDatabase.Rows[index].GenomeId)
                //        temp += simpleGenomeDatabase.genomeDatas[i].ToString();
                //}

                return virusDatas[index].ToString() + '\n';
            }
        }

        public IReadOnlyList<GenomeData> GetGenomes()
        {
            List<GenomeData> genomes = new List<GenomeData>();
            for (int i = 0; i < simpleGenomeDatabase.genomeDatas.Count; i++)
            {
                if (simpleGenomeDatabase.genomeDatas[i].Id == simpleDatabase.Rows[index].GenomeId)
                    genomes.Add(simpleGenomeDatabase.genomeDatas[i]);//  temp += simpleGenomeDatabase.genomeDatas[i].ToString();
            }
            return genomes;
        }

        public VirusData GetVirusData()
        {
            return new VirusData(simpleDatabase.Rows[index].VirusName, simpleDatabase.Rows[index].DeathRate, simpleDatabase.Rows[index].InfectionRate, GetGenomes());
        }

        public void Reset()
        {
            index = 0;
            virusDatas.Clear();
            virusDatas.Add(GetVirusData());
        }
    }

    internal class Iterator_ED : Iiterator //Iterator
    {
        private ExcellDatabase excellDatabase;
        private int index = 0;
        private readonly int count;
        private readonly string[] names;
        private readonly string[] deathRates;
        private readonly string[] infectionRates;
        private readonly string[] genomeIDs;
        private List<VirusData> virusDatas;
        private readonly SimpleGenomeDatabase simpleGenomeDatabase;

        public Iterator_ED(ExcellDatabase _excellDatabase) : base()
        {
            excellDatabase = _excellDatabase;
            count = 0;
            foreach (char c in excellDatabase.Names)
                if (c == ';') count++;
            count++;
            names = excellDatabase.Names.Split(';');
            deathRates = excellDatabase.DeathRates.Split(';');
            infectionRates = excellDatabase.InfectionRates.Split(';');
            genomeIDs = excellDatabase.GenomeIds.Split(';');
            simpleGenomeDatabase = null;
            virusDatas = new List<VirusData>();
            virusDatas.Add(GetVirusData());
        }

        public Iterator_ED(ExcellDatabase _excellDatabase, SimpleGenomeDatabase _simpleGenomeDatabase) : base()
        {
            excellDatabase = _excellDatabase;
            count = 0;
            foreach (char c in excellDatabase.Names)
                if (c == ';') count++;
            count++;
            names = excellDatabase.Names.Split(';');
            deathRates = excellDatabase.DeathRates.Split(';');
            infectionRates = excellDatabase.InfectionRates.Split(';');
            genomeIDs = excellDatabase.GenomeIds.Split(';');
            simpleGenomeDatabase = _simpleGenomeDatabase;
            virusDatas = new List<VirusData>();
            virusDatas.Add(GetVirusData());
        }

        public bool Next()
        {
            if (index < count - 1)
            {
                index++;
                virusDatas.Add(GetVirusData());
                return true;
            }
            else
                return false;
        }

        public string Current()
        {
            if (simpleGenomeDatabase == null)
                return new String($"Row: {index}:\n\tVirus name: {names[index]}\n\tInfection rate: {infectionRates[index]}\n\tDeath rate: {deathRates[index]}\n\tGenome Id: {genomeIDs[index]}\n");
            else
            {
                //string temp = new string($"Row: {index}:\n\tVirus name: {names[index]}\n\tInfection rate: {infectionRates[index]}\n\tDeath rate: {deathRates[index]}\n\t");
                //for (int i = 0; i < simpleGenomeDatabase.genomeDatas.Count; i++)
                //{
                //    if (simpleGenomeDatabase.genomeDatas[i].Id.ToString() == genomeIDs[index])
                //        temp += simpleGenomeDatabase.genomeDatas[i].ToString();
                //}
                return virusDatas[index].ToString() + '\n';// temp;
            }
        }

        public IReadOnlyList<GenomeData> GetGenomes()
        {
            List<GenomeData> genomes = new List<GenomeData>();
            for (int i = 0; i < simpleGenomeDatabase.genomeDatas.Count; i++)
            {
                if (simpleGenomeDatabase.genomeDatas[i].Id.ToString() == genomeIDs[index])
                    genomes.Add(simpleGenomeDatabase.genomeDatas[i]);//  temp += simpleGenomeDatabase.genomeDatas[i].ToString();
            }
            return genomes;
        }

        public VirusData GetVirusData()
        {
            return new VirusData(names[index], Double.Parse(deathRates[index]), Double.Parse(infectionRates[index]), GetGenomes());
        }

        public void Reset()
        {
            index = 0;
            virusDatas.Clear();
            virusDatas.Add(GetVirusData());
        }
    }

    internal class Iterator_OD : Iiterator //Iterator
    {
        private INode node;
        private INode root;
        private readonly SimpleGenomeDatabase simpleGenomeDatabase;
        private Stack<INode> untraversed;
        private List<VirusData> virusDatas;
        private int index = 0;

        public Iterator_OD(OvercomplicatedDatabase _overcomplicatedDatabase) : base()
        {
            node = _overcomplicatedDatabase.Root;
            root = _overcomplicatedDatabase.Root;
            simpleGenomeDatabase = null;
            untraversed = new Stack<INode>();
            virusDatas = new List<VirusData>();
            virusDatas.Add(GetVirusData());
        }

        public Iterator_OD(OvercomplicatedDatabase _overcomplicatedDatabase, SimpleGenomeDatabase _simpleGenomeDatabase) : base()
        {
            untraversed = new Stack<INode>();
            node = _overcomplicatedDatabase.Root;
            root = _overcomplicatedDatabase.Root;
            simpleGenomeDatabase = _simpleGenomeDatabase;
            virusDatas = new List<VirusData>();
            virusDatas.Add(GetVirusData());
        }

        public bool Next()
        {
            if (node.Children.Count == 0 && untraversed.Count == 0)
                return false;
            else if (node.Children.Count == 0 && untraversed.Count != 0)
            {
                index++;
                node = untraversed.Pop();
                virusDatas.Add(GetVirusData());
                return true;
            }
            else if (node.Children.Count != 0)
            {
                index++;

                node = node.Children[0];
                virusDatas.Add(GetVirusData());

                for (int i = 1; i < node.Children.Count; i++)
                    untraversed.Push(node.Children[i]);

                return true;
            }
            else
            {
                return false;
            }
        }

        public string Current()
        {
            if (simpleGenomeDatabase == null)
                return new string($"\tVirus name: {node.VirusName}\n\tInfection rate: {node.InfectionRate}\n\tDeath rate: {node.DeathRate}\n\tGenome Tag: {node.GenomeTag}\n");
            else
            {
                //var temp = new String($"\tVirus name: {node.VirusName}\n\tInfection rate: {node.InfectionRate}\n\tDeath rate: {node.DeathRate}\n\tGenome Tag: {node.GenomeTag}\n\t");
                //for (int i = 0; i < simpleGenomeDatabase.genomeDatas.Count; i++)
                //{
                //    for (int j = 0; j < simpleGenomeDatabase.genomeDatas[i].Tags.Count; j++)
                //        if (simpleGenomeDatabase.genomeDatas[i].Tags[j] == node.GenomeTag)
                //        {
                //            temp += simpleGenomeDatabase.genomeDatas[i].ToString() + '\n' + '\t';
                //            break;
                //        }
                //}
                return virusDatas[index].ToString() + '\n';// temp;
            }
        }

        public IReadOnlyList<GenomeData> GetGenomes()
        {
            List<GenomeData> genomes = new List<GenomeData>();
            for (int i = 0; i < simpleGenomeDatabase.genomeDatas.Count; i++)
            {
                for (int j = 0; j < simpleGenomeDatabase.genomeDatas[i].Tags.Count; j++)
                    if (simpleGenomeDatabase.genomeDatas[i].Tags[j] == node.GenomeTag)
                    {
                        genomes.Add(simpleGenomeDatabase.genomeDatas[i]);//  temp += simpleGenomeDatabase.genomeDatas[i].ToString();
                        break;
                    }
            }
            return genomes;
        }

        public VirusData GetVirusData()
        {
            return new VirusData(node.VirusName, node.InfectionRate, node.DeathRate, GetGenomes());
        }

        public void Reset()
        {
            index = 0;
            node = root;
            virusDatas.Clear();
            virusDatas.Add(GetVirusData());
        }
    }
}