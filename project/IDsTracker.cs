using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TuyaMCUAnalyzer
{
    //class IDTrackerInteger
    //{

    //}
    //class IDTrackerString
    //{

    //}
    class IDTracker
    {
        public TuyaType type;
        public int id;
        public List<int> intValues = new List<int>();
        public List<string> strValues = new List<string>();

        public void addValue(int i)
        {
            if(intValues.IndexOf(i)==-1)
            {
                intValues.Add(i);
            }
        }
        public void addValue(string i)
        {
            if (strValues.IndexOf(i) == -1)
            {
                strValues.Add(i);
            }
        }
    }
    class IDsTracker
    {
        public void addValueInt(int id, TuyaType type, int value, Dictionary<int, IDTracker> vars)
        {
            IDTracker tr;
            vars.TryGetValue(id, out tr);
            if(tr == null)
            {
                tr = new IDTracker();
                tr.id = id;
                tr.type = type;
                vars.Add(id, tr);
            }
            tr.addValue(value);
        }
        public void addValueStr(int id, TuyaType type, string value, Dictionary<int, IDTracker> vars)
        {
            IDTracker tr;
            vars.TryGetValue(id, out tr);
            if (tr == null)
            {
                tr = new IDTracker();
                tr.id = id;
                tr.type = type;
                vars.Add(id, tr);
            }
            tr.addValue(value);
        }

        internal void display(ListView tg, Dictionary<int, IDTracker> vars)
        {
            tg.Items.Clear();
            foreach(var v in vars)
            {
                IDTracker tr = v.Value;
                ListViewItem it = new ListViewItem(""+tr.id);
                string typeStr = tr.type + " (" + (int)tr.type + ")";
                it.SubItems.Add(""+ typeStr);

                if(tr.intValues.Count > 0)
                {
                    it.SubItems.Add("" + tr.intValues.Count);
                    string vals = "";
                    if(tr.intValues.Count < 5)
                    {
                        for (int j = 0; j < tr.intValues.Count; j++)
                        {
                            if (j != 0)
                                vals += ", ";
                            vals += tr.intValues[j].ToString();
                        }
                    }
                    else
                    {
                        int min = ArrayUtils.FindMin(tr.intValues);
                        int max = ArrayUtils.FindMax(tr.intValues);
                        vals += "Min=" + min + ", max=" + max;
                    }
                    it.SubItems.Add("" + vals);
                }
                else if(tr.strValues.Count > 0)
                {
                    it.SubItems.Add("" + tr.strValues.Count);
                    string vals = "";
                    for (int j = 0; j < tr.strValues.Count && vals.Length < 15; j++)
                    {
                        if (j != 0)
                            vals += ", ";
                        vals += tr.strValues[j].ToString();
                    }
                    it.SubItems.Add("" + vals);
                }
                else
                {
                    it.SubItems.Add("0");
                }
                tg.Items.Add(it);
            }
        }
    }
}
