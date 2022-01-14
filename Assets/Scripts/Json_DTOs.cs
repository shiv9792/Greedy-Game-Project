using System;
public class Json_DTOs{}


[Serializable]
public class Positions
{
    public float x;
    public float y;
    public float width;
    public float height;
}

[Serializable]
public class Placement
{
    public Positions position;
}

[Serializable]
public class Operations
{
    public string name;
    public string argument;
}

[Serializable]
public class Layers
{
    public string type;
    public string path;
    public Placement[] placement;
    public Operations[] operations;

}

[Serializable]
public class AllLayersList
{
    public Layers[] layers;
}

