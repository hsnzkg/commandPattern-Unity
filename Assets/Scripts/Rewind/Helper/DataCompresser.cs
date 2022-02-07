using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
public static class DataCompresser
{
    public static byte[] Compress(this object obj) 
    {
        MemoryStream ms = new MemoryStream(); 
        GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(zs, obj);
        return ms.ToArray();
    }

    public static T Decompress<T>(this byte[] data)
    {
        MemoryStream ms = new MemoryStream(data); 
        GZipStream zs = new GZipStream(ms, CompressionMode.Decompress, true);
        BinaryFormatter bf = new BinaryFormatter();
        return (T)bf.Deserialize(zs);
    }
}