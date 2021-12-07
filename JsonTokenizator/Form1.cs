using JsonTokenizator.Tools;

namespace JsonTokenizator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var parser = new ParserClass();
            var json = richTextBox1.Text;
            if (String.IsNullOrEmpty(json))
                json = parser.ReadJson();
            var token = parser.Parse(json);
            var deparser = new DeparserClass();
            var result = deparser.Deparse(token);
            richTextBox2.Text = result;
        }
    }
}