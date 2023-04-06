using System.Text.RegularExpressions;

Console.Write("Rename ALL PSA files of current directory? (y): ");
var input = Console.ReadKey();
Console.WriteLine();

// const string PSA = "PSA";
const string resolution1080p = "1080p";
const string resolution720p = "720p";
var regex = new Regex(@"[Ss]\d{2}[Ee]\d{2}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

if (input.Key == ConsoleKey.Y)
{
    var directory = Directory.GetCurrentDirectory();
    var filenames = Directory.GetFiles(directory)
        .Where(filename => filename.Contains(resolution720p, StringComparison.OrdinalIgnoreCase)
                           || filename.Contains(resolution1080p, StringComparison.OrdinalIgnoreCase))
        .ToList();

    foreach (var filename in filenames)
    {
        var file = new FileInfo(filename);

        _ = filename switch
        {
            not null when filename.Contains(resolution720p, StringComparison.OrdinalIgnoreCase) =>
                Rename(resolution720p, file),
            not null when filename.Contains(resolution1080p, StringComparison.OrdinalIgnoreCase) =>
                Rename(resolution1080p, file),
            _ => true,
        };
    }
}

bool Rename(string resolution, FileInfo file)
{
    var SXXEXX = regex.Match(file.FullName) ?? throw new ArgumentNullException($"File not found: {file.FullName}");

    var filename = file.Name.Split(resolution, StringSplitOptions.TrimEntries).First();
    var newFilename = filename.Replace(".", " ").Trim();

    var indexOfSXXEXX = newFilename.LastIndexOf(SXXEXX.Value, StringComparison.OrdinalIgnoreCase);
    var lastIndexOfSXXEXX = indexOfSXXEXX + 6;

    if (newFilename.Length > lastIndexOfSXXEXX)
    {
        newFilename = newFilename.Insert(lastIndexOfSXXEXX, " -");
    }

    Console.WriteLine("======================================================================");
    Console.WriteLine(file.Name);
    file.MoveTo($"{file.DirectoryName}{Path.DirectorySeparatorChar}{newFilename}{file.Extension}");
    Console.WriteLine(newFilename);
    Console.WriteLine("======================================================================");

    return false;
}