using System.Numerics;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
public static class DataCompresser
{
    public static byte[] Compress(object obj) 
    {
        MemoryStream ms = new MemoryStream(); 
        GZipStream zs = new GZipStream(ms, CompressionMode.Compress);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(zs, obj);
        zs.Close();
        ms.Close();
        
        return ms.ToArray();
    }

    public static CommandGroup DeCompress(byte[] data)
    {
        MemoryStream ms = new MemoryStream(data); 
        GZipStream zs = new GZipStream(ms, CompressionMode.Decompress);
        BinaryFormatter bf = new BinaryFormatter();
        CommandGroup tempGroup = bf.Deserialize(zs) as CommandGroup; 
        zs.Close();
        ms.Close();
        return tempGroup;
    }
}