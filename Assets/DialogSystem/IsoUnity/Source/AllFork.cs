﻿using UnityEngine;
using System.Collections;

[NodeContent("Fork/Group/All", 2)]
public class AllFork : ForkGroup
{
    public override bool check()
    {
        return forks.TrueForAll(f => f.check());
    }

    public override string ToString()
    {
        return string.Join(" && ", forks.ConvertAll(f => "( " + f.ToString() + " )").ToArray());
    }
}

