public static class StringExtension
{
    public static bool ContainsCaseInsensitive(this string parent, string toFind)
    {
        return parent.ToLowerInvariant().Contains(toFind.ToLowerInvariant());
    }

    public static bool EqualsCaseInsensitive(this string parent, string toFind)
    {
        return parent.ToLowerInvariant().Equals(toFind.ToLowerInvariant());
    }

    public static string CleanCLone(this string parent)
    {
        return parent.Replace("(Clone)","");
    }
}
