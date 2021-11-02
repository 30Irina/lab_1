using System;
using System.Collections.Generic;
public abstract class V4Data
{
    public string type { get; set; }
    public System.DateTime createdAt { get; set; }
    public abstract int Count { get; }
    public abstract float MaxFromOrigin { get; }
    public abstract string ToLongString(string format);

    public abstract override string ToString();

    public V4Data(string type, System.DateTime createdAt)
    {
        this.type = type;
        this.createdAt = createdAt;
    }
}

public class V4DataList : V4Data
{
    public List<DataItem> list { get; }

    public V4DataList(string dataType, System.DateTime time) : base(dataType, time)
    {
        list = new List<DataItem>();
    }

    public bool Add(DataItem newItem)
    {
        foreach (DataItem item in list)
        {
            if (item.point.X == newItem.point.X && item.point.Y == newItem.point.Y)
                return false;
        }
        list.Add(newItem);
        return true;
    }

    public int AddDefaults(int nItems, Fv2Vector2 F)
    {
        int n = 0;
        var randdom = new System.Random();
        var v1 = new System.Numerics.Vector2();
        for (int i = 0; i < nItems; i++)
        {
            v1.X = ((float)(randdom.NextDouble())) * nItems;
            v1.Y = ((float)(randdom.NextDouble())) * nItems;
            DataItem DtItm = new DataItem(v1, F(v1));
            n += Convert.ToInt32(this.Add(DtItm));
        }
        return n;
    }

    public override int Count
    {
        get
        {
            return list.Count;
        }
    }

    public override float MaxFromOrigin
    {
        get
        {
            float rasst = 0;
            System.Numerics.Vector2 z;
            System.Numerics.Vector2 v;
            float maxrasst = 0;

            if (Count != 0)
            {
                foreach (DataItem i in this.list)
                {
                    z = System.Numerics.Vector2.Zero;
                    v = i.point;
                    rasst = System.Numerics.Vector2.Distance(z, v);
                    if (rasst > maxrasst)
                    {
                        maxrasst = rasst;
                    }
                }
                return maxrasst;
            }
            return 0;

        }
    }

    public override string ToString()
    {
        string res = "Тип: " + type + "\n" +
                    "Count = " + list.Count + "\n";
        foreach (DataItem item in list)
            res += item.ToString();
        return res;
    }

    public override string ToLongString(string format)
    {
        string res = "Тип: " + type + "\n" +
            "Count = " + list.Count + "\n";

        foreach (DataItem item in list)
        {
            res += item.ToLongString(format);
            res += "x = " + item.point.X + "\n";
            res += "y = " + item.point.Y + "\n";
            res += "значение = " + item.value + "\n";
        }
        
        return res;
    }
}

public class V4DataArray : V4Data
{
    public int Ox { get; }
    public int Oy { get; }
    public System.Numerics.Vector2 step { get; }
    public System.Numerics.Vector2[,] matrix { get; }
    public V4DataArray(string dataType, System.DateTime time) : base(dataType, time)
    {
        this.matrix = new System.Numerics.Vector2[0, 0];
    }

    public V4DataArray(string dataType, System.DateTime time, int x, int y, System.Numerics.Vector2 Step, Fv2Vector2 F) : base(dataType, time)
    {
        this.Ox = x;
        this.Oy = y;
        this.step = Step;
        this.matrix = new System.Numerics.Vector2[x, y];
        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                this.matrix[i, j] = F(new System.Numerics.Vector2(i * step.X, j * step.Y));
            }
    }
    public override int Count { get { return Ox * Oy; } }

    public override float MaxFromOrigin
    {
        get
        {
            if (Count != 0) return System.Numerics.Vector2.Distance(System.Numerics.Vector2.Zero, new System.Numerics.Vector2((Ox - 1) * step.X, (Oy - 1) * step.Y));
            else return 0;
        }
    }

    public override string ToString()
    {
        return "Тип: " + type + "\n" +
                    "Ox: " + Ox + ", Oy: " + Oy + "\n" +
                    "step.x: " + step.X + " step.y: " + step.Y + "\n";
    }
    public override string ToLongString(string format)
    {
        string res = this.ToString();
        for (int i = 0; i < this.Ox; i++)
            for (int j = 0; j < this.Oy; j++)
            {
                res += "(x, y) = (" + String.Format(format, i * step.X) + ", " +
                   String.Format(format, j * step.Y) + ")\n";
                res += "значение = " + String.Format(format, matrix[i, j]) + "\n" +
                    " значение модуля поля = " + String.Format(format, System.Numerics.Vector2.Abs(matrix[i, j])) + "\n";
            }
        return res;
    }
    public V4DataList Transformation()
    {
        V4DataList list = new V4DataList(this.type, this.createdAt);
        var v_1 = new System.Numerics.Vector2();
        var v_2 = new System.Numerics.Vector2();
        for (int i = 0; i < this.Ox; i++)
            for (int j = 0; j < this.Oy; j++)
            {
                v_1.X = i * step.X;
                v_1.Y = j * step.Y;
                v_2.X = matrix[i, j].X;
                v_2.Y = matrix[i, j].Y;
                list.Add(new DataItem(v_1, v_2));
            }
        return list;
    }
}
