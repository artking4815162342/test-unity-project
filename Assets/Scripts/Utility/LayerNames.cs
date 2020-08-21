using UnityEngine;

public static class LayerNames
{
    public static readonly string EntityName = "Entity";
    public static readonly string TerrainName = "Terrain";
    public static readonly string PlayerName = "Player";

    public static int LayerEntity = LayerMask.NameToLayer(EntityName);
    public static int LayerTerrain = LayerMask.NameToLayer(EntityName);
    public static int LayerPlayer = LayerMask.NameToLayer(EntityName);
}