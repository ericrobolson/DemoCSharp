using System;
using System.Runtime.Serialization;

namespace Cybo;


[Serializable]
public enum AssetType
{
    Texture
}

/// <summary>
/// An asset that should be active for use.
/// </summary>
[Serializable]
public struct Asset
{
    [DataMember]
    public string FilePath { get; set; }

    [DataMember]
    public AssetType Type { get; set; }
}