using System;
using System.Collections.Generic;

public class V4MainCollection
{
    private List<V4Data> val = new List<V4Data>();

    public int Count { get { return val.Count; } }

    public V4Data this[int i]
    {
        get { return val[i]; }
    }

    public bool Contains(string ID)
    {
        foreach (V4Data item in val) 
            if (item.type == ID)
                return true;
        return false;
    }

    public bool Add(V4Data v4Data)
    {
        if (this.Contains(v4Data.type))
            return false;
        val.Add(v4Data);
        return true;
    }

    public string ToLongString(string format)
    {
        string res = "";
        foreach (V4Data k in this.val)
            res += k.ToLongString(format) + "\n";

        return res;
    }

    public override string ToString()
    {
        string res = "";
        foreach (V4Data k in this.val)
            res += k.ToString() + "\n";

        return res; 
    }
}
