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
            var token = parser.Parse(json);
            var deparser = new DeparserClass();
            var result = deparser.Deparse(token);
            Console.WriteLine(result);
        }
    }
}