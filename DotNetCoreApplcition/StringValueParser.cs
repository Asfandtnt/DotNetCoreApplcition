namespace DotNetCoreApplcition
{
    public static class StringValueParser
    {
        public static string ParseValue(this object val)
        {
            if (val == null)
                return string.Empty;
            return val.ToString();
        }

    }
}
