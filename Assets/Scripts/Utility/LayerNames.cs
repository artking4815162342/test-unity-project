using UnityEngine;

public static class LayerNames
{
    public static readonly string EntityName = "Entity";
    public static readonly string TerrainName = "Terrain";

    public static readonly int Entity = LayerMask.NameToLayer(EntityName);
    public static readonly int Terrain = LayerMask.NameToLayer(TerrainName);
}