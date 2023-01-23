Console.WriteLine("WASM based DocumentProcessor");

var input = System.Environment.GetEnvironmentVariable("input-file");
var output = System.Environment.GetEnvironmentVariable("output-file");

Console.WriteLine($"Writing file {input} to {output}");

if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(output))
{
    try
    {
        System.IO.File.Copy(input, output);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }
    Console.WriteLine("Files processed!");
}
else
{
    Console.WriteLine("Please provide two file names to copy");
}