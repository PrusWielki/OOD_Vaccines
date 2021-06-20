using System;
using System.Collections.Generic;

namespace Task3
{
    internal class DecoratorVirusData : Iiterator
    {
        private Iiterator itr;
        private Func<VirusData, bool> filter;

        public DecoratorVirusData(Iiterator _itr, Func<VirusData, bool> _filter)
        {
            itr = _itr;
            filter = _filter;
        }

        public string Current()
        {
            if (GetVirusData().VirusName != "")
                return GetVirusData().ToString() + '\n';
            else
                return String.Empty;
        }

        public IReadOnlyList<GenomeData> GetGenomes()
        {
            return itr.GetGenomes();
        }

        public VirusData GetVirusData()
        {
            if (filter(itr.GetVirusData()))
            {
                return itr.GetVirusData();
            }
            else
            {
                return new VirusData("", 0, 0, new List<GenomeData>());
            }
        }

        public bool Next()
        {
            return itr.Next();
        }

        public void Reset()
        {
            itr.Reset();
        }
    }

    internal class MapperVirusData : Iiterator
    {
        private Iiterator itr;
        private Func<VirusData, VirusData> filter;

        public MapperVirusData(Iiterator _itr, Func<VirusData, VirusData> _filter)
        {
            itr = _itr;
            filter = _filter;
        }

        public string Current()
        {
            if (GetVirusData().VirusName != "")
                return GetVirusData().ToString() + '\n';
            else
                return String.Empty;
        }

        public IReadOnlyList<GenomeData> GetGenomes()
        {
            return itr.GetGenomes();
        }

        public VirusData GetVirusData()
        {
            return filter(itr.GetVirusData());
        }

        public bool Next()
        {
            return itr.Next();
        }

        public void Reset()
        {
            itr.Reset();
        }
    }
    internal class ConcatDatabaseItr : Iiterator
    {
        Iiterator itr;
        //Iiterator itr1;
        Iiterator itr2;
        bool swapped;
        public ConcatDatabaseItr(Iiterator _itr1, Iiterator _itr2)
        {
            itr = _itr1;
            itr2 = _itr2;
            swapped = false;
        }

        public string Current()
        {
            return itr.Current();
        }

        public IReadOnlyList<GenomeData> GetGenomes()
        {
            return itr.GetGenomes();
        }

        public VirusData GetVirusData()
        {
            return itr.GetVirusData();
        }

        public bool Next()
        {
            if (!itr.Next())
            {
                if (!swapped)
                {
                    swapped = true;
                    itr = itr2;
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }

        public void Reset()
        {
            itr.Reset();
        }
    }
    //internal class DeathRateFilterBigger : Iiterator
    //{
    //    // private VirusData virusData;
    //    private Iiterator itr;

    //    private int x;

    //    public DeathRateFilterBigger(Iiterator _itr, int _x)
    //    {
    //        itr = _itr;
    //        x = _x;
    //    }

    //    public string Current()
    //    {
    //        //if ()
    //        //return itr.Current();
    //        if (GetVirusData() != null)
    //            return GetVirusData().ToString() + '\n';
    //        else
    //            return String.Empty;
    //    }

    //    public IReadOnlyList<GenomeData> GetGenomes()
    //    {
    //        return itr.GetGenomes();
    //    }

    //    public VirusData GetVirusData()
    //    {
    //        if (itr.GetVirusData()?.DeathRate > x)
    //            return itr.GetVirusData();
    //        else
    //            return null;
    //    }

    //    public bool Next()
    //    {
    //        return itr.Next();
    //    }

    //    public void Reset()
    //    {
    //        itr.Reset();
    //    }
    //}

    //internal class DeathRateFilterLower : Iiterator
    //{
    //    // private VirusData virusData;
    //    private Iiterator itr;

    //    private int x;

    //    public DeathRateFilterLower(Iiterator _itr, int _x)
    //    {
    //        itr = _itr;
    //        x = _x;
    //    }

    //    public string Current()
    //    {
    //        //if ()
    //        //return itr.Current();
    //        if (GetVirusData() != null)
    //            return GetVirusData().ToString() + '\n';
    //        else
    //            return String.Empty;
    //    }

    //    public IReadOnlyList<GenomeData> GetGenomes()
    //    {
    //        return itr.GetGenomes();
    //    }

    //    public VirusData GetVirusData()
    //    {
    //        if (itr.GetVirusData().DeathRate < x)
    //            return itr.GetVirusData();
    //        else
    //            return null;
    //    }

    //    public bool Next()
    //    {
    //        return itr.Next();
    //    }

    //    public void Reset()
    //    {
    //        itr.Reset();
    //    }
    //}


    //internal class VirusDataDecorator : Iiterator
    //{
    //    private VirusData virusData;
    //    private Iiterator itr;

    //    public VirusDataDecorator(Iiterator _itr)
    //    {
    //        itr = _itr;
    //    }

    //    public string Current()
    //    {
    //        //if ()
    //        //return itr.Current();
    //        if (GetVirusData() != null)
    //            return GetVirusData().ToString() + '\n';
    //        else
    //            return String.Empty;
    //    }

    //    public IReadOnlyList<GenomeData> GetGenomes()
    //    {
    //        return itr.GetGenomes();
    //    }

    //    public VirusData GetVirusData()
    //    {
    //        if (itr.GetVirusData() != null)
    //        {
    //            VirusData temp = itr.GetVirusData();
    //            return new VirusData(temp.VirusName, temp.DeathRate + x, temp.InfectionRate, temp.Genomes);
    //        }
    //        else
    //            return null;
    //    }
    //    public VirusData Modify(VirusData modifiedVirusData)
    //    {
    //    }
    //    public bool Next()
    //    {
    //        return itr.Next();
    //    }
    //}
}