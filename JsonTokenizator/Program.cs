using JsonTokenizator.Tools;

namespace JsonTokenizator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            var parser = new ParserClass();
            var json = parser.ReadJson();
            //var jArray = parser.GetJArray(json.Substring(127));
            var token = parser.Parse(json);
            Console.WriteLine(token);
        }
    }
}