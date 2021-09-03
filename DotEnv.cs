namespace SimpleMongo
{
    using System;
    using System.IO;

    public static class DotEnv
    {
        public static void Load(string filePath)
        {

            if (!File.Exists(filePath))
            {
                return;
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                var index = line.IndexOf("=");
                var parts = new string[] { line.Substring(0, index), line.Substring(index + 1) };
                if (parts.Length != 2)
                {
                    continue;
                }
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}