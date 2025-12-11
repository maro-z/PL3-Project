using TextAnalyzer;
namespace TextAnalyzerGuI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string test(string str)
        {
            var result = TextAnalyzer.TextAnalyzer.analyzeText(str);
            var json = TextAnalyzer.TextAnalyzer.generateJsonReport(result);
            return json;
        }
    }
}
