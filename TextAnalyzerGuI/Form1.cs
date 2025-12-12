using TextAnalyzer;
using Microsoft.FSharp.Core;

namespace TextAnalyzerGuI
{
    public partial class Form1 : Form
    {
        private TextAnalyzer.TextAnalyzer.AnalysisReport? currentReport = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            try
            {
                string inputText = txtManualInput.Text;

                if (string.IsNullOrWhiteSpace(inputText))
                {
                    MessageBox.Show("Please enter text to analyze.", "Input Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate and normalize input
                var validationResult = TextAnalyzer.TextAnalyzer.handleManualInput(inputText);

                if (validationResult.IsOk)
                {
                    var cleanText = validationResult.ResultValue;

                    // Analyze the text
                    currentReport = TextAnalyzer.TextAnalyzer.analyzeText(cleanText);

                    // Display results
                    DisplayAnalysisResults(currentReport);
                }
                else
                {
                    var error = validationResult.ErrorValue;
                    MessageBox.Show(error, "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Analysis failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAnalysisResults(TextAnalyzer.TextAnalyzer.AnalysisReport report)
        {
            // Display statistics
            lblWordCount.Text = $"Word Count: {report.WordCount}";
            lblSentenceCount.Text = $"Sentence Count: {report.SentenceCount}";
            lblParagraphCount.Text = $"Paragraph Count: {report.ParagraphCount}";
            lblAvgSentenceLength.Text = $"Avg. Sentence Length: {report.ASL:F1}";
            lblReadabilityScore.Text = $"Readability Score: {report.FRE:F1} ({report.FKGLDetails.ReadingLevel})";

            // Display top 10 words
            lstTopWords.Items.Clear();
            foreach (var wordFreq in report.TopWords)
            {
                lstTopWords.Items.Add($"{wordFreq.Word} ({wordFreq.Count})");
            }
        }

        private void btnExportJson_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReport == null)
                {
                    MessageBox.Show("Please analyze text first before exporting.", "No Analysis",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveDialog.DefaultExt = "json";
                    saveDialog.FileName = "analysis_report.json";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string json = TextAnalyzer.TextAnalyzer.generateJsonReport(currentReport);
                        var saveResult = TextAnalyzer.TextAnalyzer.saveJsonToFile(saveDialog.FileName, json);

                        if (saveResult.IsOk)
                        {
                            MessageBox.Show($"JSON report saved successfully to:\n{saveDialog.FileName}",
                                "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            var error = saveResult.ErrorValue;
                            MessageBox.Show($"Failed to save JSON: {error}", "Export Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openDialog = new OpenFileDialog())
                {
                    openDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    openDialog.Title = "Select a text file to analyze";

                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        var loadResult = TextAnalyzer.TextAnalyzer.loadFile(openDialog.FileName);
                        var extension = Path.GetExtension(openDialog.FileName);
                        if(extension != ".txt")
                        {
                            MessageBox.Show("please enter a file with txt extension","Load failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (loadResult.IsOk)
                        {
                            var text = loadResult.ResultValue;
                            txtManualInput.Text = text;

                            // Automatically analyze the loaded text
                            currentReport = TextAnalyzer.TextAnalyzer.analyzeText(text);
                            DisplayAnalysisResults(currentReport);
                        }
                        else
                        {
                            var error = loadResult.ErrorValue;
                            MessageBox.Show(error, "Load Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear input text
            txtManualInput.Clear();

            // Clear results
            currentReport = null;
            lblWordCount.Text = "Word Count:";
            lblSentenceCount.Text = "Sentence Count:";
            lblParagraphCount.Text = "Paragraph Count:";
            lblAvgSentenceLength.Text = "Avg. Sentence Length:";
            lblReadabilityScore.Text = "Readability Score:";
            lstTopWords.Items.Clear();
        }
    }
}
