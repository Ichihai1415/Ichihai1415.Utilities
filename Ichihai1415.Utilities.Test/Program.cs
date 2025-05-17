namespace Ichihai1415.Utilities.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var gz = GZipConverter.String2GZip(File.ReadAllText(@"C:\Ichihai1415\source\vs\WorldQuakeViewer\WorldQuakeViewer\bin\Debug\Resources\PB2002_steps.geojson"));
            File.WriteAllText(@"C:\Ichihai1415\source\vs\WorldQuakeViewer\WorldQuakeViewer\bin\Debug\Resources\PB2002_steps.geojson.gz", gz);
            var gz2 = GZipConverter.GZip2String(gz);
            File.WriteAllText(@"C:\Ichihai1415\source\vs\WorldQuakeViewer\WorldQuakeViewer\bin\Debug\Resources\PB2002_steps.geojson.gz.geojson", gz2);
        }
    }
}
