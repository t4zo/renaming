using System.Text.RegularExpressions;

Console.WriteLine("Rename all PSA files of current directory? (y)");
var input = Console.ReadKey();
Console.WriteLine();
if(input.Key == ConsoleKey.Y)
{
    var directory = Directory.GetCurrentDirectory();
    var fileames = Directory.GetFiles(directory);
    foreach(var filename in fileames)
    {
        var PSA = "PSA";
        var resolution1080p = "1080p";
        var resolution720p = "720p";
        var file = new FileInfo(filename);
        var regex = new Regex(@"[Ss]\d{2}[Ee]\d{2}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        if (filename.Contains(PSA, StringComparison.OrdinalIgnoreCase))
        {
            if (filename.Contains(resolution1080p, StringComparison.OrdinalIgnoreCase))
            {
                var SXXEXX = regex.Match(filename);

                var firstFilename = filename.Split(resolution1080p)[0];
                var newFilename = firstFilename.Replace(".", " ").Trim();
                var indexOfSXEXX = newFilename.IndexOf(SXXEXX.Value, StringComparison.OrdinalIgnoreCase);
                var finalFilename = newFilename.Insert(indexOfSXEXX+6, " -");

                Console.WriteLine("======================================================================");
                Console.WriteLine(file);
                file.MoveTo($"{finalFilename}{file.Extension}");
                Console.WriteLine(finalFilename);
                Console.WriteLine("======================================================================");
            }

            if (filename.Contains(resolution720p, StringComparison.OrdinalIgnoreCase))
            {
                var SXXEXX = regex.Match(filename);

                var firstFilename = filename.Split(resolution720p)[0];
                var newFilename = firstFilename.Replace(".", " ").Trim();
                var indexOfSXEXX = newFilename.IndexOf(SXXEXX.Value, StringComparison.OrdinalIgnoreCase);
                var finalFilename = newFilename.Insert(indexOfSXEXX+6, "- ");

                Console.WriteLine(file);
                file.MoveTo($"{finalFilename}{file.Extension}");
                Console.WriteLine(newFilename);
            }
        }
    }
}
