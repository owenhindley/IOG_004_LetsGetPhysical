using System.IO;

public static class PathExtension
{
    public static string Combine(params string[] path)
    {
        if (path.Length == 0) return "";
        var res = path[0];
        for (int i = 1; i < path.Length; i++)
        {
            res = Path.Combine(res, path[i]);
        }
        return res;
    } 
}
