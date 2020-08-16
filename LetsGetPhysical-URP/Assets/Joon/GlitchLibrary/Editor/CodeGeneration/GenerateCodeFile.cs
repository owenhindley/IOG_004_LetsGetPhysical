using UnityEngine;
using System.IO;

public static class GenerateCodeFile
{
	public static void CreateFile(string template, string outputFilePath, string entryTemplate, string[] strings, int[] values = null)
	{
		string result = "";
		var i = 0;
		foreach (string literal in strings)
		{
			var safe = literal.Replace(" ", "");
			var value = 0;
			if (values != null)
			{
				value = values[i];
			}
			var entry = entryTemplate
				.Replace("#name#", literal)
				.Replace("#safename#", safe)
				.Replace("#value#", value + "");
			i++;
			result += entry;
		}

		result = template.Replace("#generateme#", result);
        System.IO.File.WriteAllText(Path.Combine(Application.dataPath,outputFilePath), result);
		Debug.Log("Regenerated " + Path.GetFileName(outputFilePath) + " (" + strings.Length + " entries)"); 
	}
}

