namespace TextAnalyzerGuI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtManualInput = new TextBox();
            btnAnalyze = new Button();
            btnExportJson = new Button();
            lblWordCount = new Label();
            lblSentenceCount = new Label();
            lblParagraphCount = new Label();
            lblAvgSentenceLength = new Label();
            lblReadabilityScore = new Label();
            lstTopWords = new ListBox();
            lblManualTextEntry = new Label();
            lblTopWordFrequency = new Label();
            btnLoadFile = new Button();
            btnClear = new Button();
            SuspendLayout();
            // 
            // txtManualInput
            // 
            txtManualInput.BackColor = Color.FromArgb(30, 30, 30);
            txtManualInput.BorderStyle = BorderStyle.None;
            txtManualInput.Font = new Font("Segoe UI", 10.2F);
            txtManualInput.ForeColor = Color.FromArgb(220, 220, 220);
            txtManualInput.Location = new Point(25, 75);
            txtManualInput.Margin = new Padding(10);
            txtManualInput.Multiline = true;
            txtManualInput.Name = "txtManualInput";
            txtManualInput.ScrollBars = ScrollBars.Vertical;
            txtManualInput.Size = new Size(630, 465);
            txtManualInput.TabIndex = 0;
            // 
            // btnAnalyze
            // 
            btnAnalyze.BackColor = Color.FromArgb(0, 120, 212);
            btnAnalyze.Cursor = Cursors.Hand;
            btnAnalyze.FlatAppearance.BorderSize = 0;
            btnAnalyze.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 158);
            btnAnalyze.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 135, 224);
            btnAnalyze.FlatStyle = FlatStyle.Flat;
            btnAnalyze.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnAnalyze.ForeColor = Color.White;
            btnAnalyze.Location = new Point(675, 111);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(200, 42);
            btnAnalyze.TabIndex = 1;
            btnAnalyze.Text = "Analyze";
            btnAnalyze.UseVisualStyleBackColor = false;
            btnAnalyze.Click += btnAnalyze_Click;
            // 
            // btnExportJson
            // 
            btnExportJson.BackColor = Color.FromArgb(30, 30, 30);
            btnExportJson.Cursor = Cursors.Hand;
            btnExportJson.FlatAppearance.BorderSize = 0;
            btnExportJson.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 158);
            btnExportJson.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 135, 224);
            btnExportJson.FlatStyle = FlatStyle.Flat;
            btnExportJson.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnExportJson.ForeColor = Color.White;
            btnExportJson.Location = new Point(675, 194);
            btnExportJson.Name = "btnExportJson";
            btnExportJson.Size = new Size(200, 42);
            btnExportJson.TabIndex = 2;
            btnExportJson.Text = "Export JSON";
            btnExportJson.UseVisualStyleBackColor = false;
            btnExportJson.Click += btnExportJson_Click;
            // 
            // lblWordCount
            // 
            lblWordCount.AutoSize = true;
            lblWordCount.Font = new Font("Segoe UI", 10.2F);
            lblWordCount.ForeColor = Color.FromArgb(200, 200, 200);
            lblWordCount.Location = new Point(668, 285);
            lblWordCount.Name = "lblWordCount";
            lblWordCount.Size = new Size(107, 23);
            lblWordCount.TabIndex = 3;
            lblWordCount.Text = "Word Count:";
            // 
            // lblSentenceCount
            // 
            lblSentenceCount.AutoSize = true;
            lblSentenceCount.Font = new Font("Segoe UI", 10.2F);
            lblSentenceCount.ForeColor = Color.FromArgb(200, 200, 200);
            lblSentenceCount.Location = new Point(668, 325);
            lblSentenceCount.Name = "lblSentenceCount";
            lblSentenceCount.Size = new Size(136, 23);
            lblSentenceCount.TabIndex = 4;
            lblSentenceCount.Text = "Sentence Count:";
            // 
            // lblParagraphCount
            // 
            lblParagraphCount.AutoSize = true;
            lblParagraphCount.Font = new Font("Segoe UI", 10.2F);
            lblParagraphCount.ForeColor = Color.FromArgb(200, 200, 200);
            lblParagraphCount.Location = new Point(668, 366);
            lblParagraphCount.Name = "lblParagraphCount";
            lblParagraphCount.Size = new Size(144, 23);
            lblParagraphCount.TabIndex = 5;
            lblParagraphCount.Text = "Paragraph Count:";
            // 
            // lblAvgSentenceLength
            // 
            lblAvgSentenceLength.AutoSize = true;
            lblAvgSentenceLength.Font = new Font("Segoe UI", 10.2F);
            lblAvgSentenceLength.ForeColor = Color.FromArgb(200, 200, 200);
            lblAvgSentenceLength.Location = new Point(668, 415);
            lblAvgSentenceLength.Name = "lblAvgSentenceLength";
            lblAvgSentenceLength.Size = new Size(180, 23);
            lblAvgSentenceLength.TabIndex = 6;
            lblAvgSentenceLength.Text = "Avg. Sentence Length:";
            // 
            // lblReadabilityScore
            // 
            lblReadabilityScore.AutoSize = true;
            lblReadabilityScore.Font = new Font("Segoe UI", 10.2F);
            lblReadabilityScore.ForeColor = Color.FromArgb(200, 200, 200);
            lblReadabilityScore.Location = new Point(668, 471);
            lblReadabilityScore.Name = "lblReadabilityScore";
            lblReadabilityScore.Size = new Size(144, 23);
            lblReadabilityScore.TabIndex = 7;
            lblReadabilityScore.Text = "Readability Score:";
            // 
            // lstTopWords
            // 
            lstTopWords.BackColor = Color.FromArgb(30, 30, 30);
            lstTopWords.BorderStyle = BorderStyle.None;
            lstTopWords.Font = new Font("Consolas", 10.2F);
            lstTopWords.ForeColor = Color.FromArgb(220, 220, 220);
            lstTopWords.FormattingEnabled = true;
            lstTopWords.Location = new Point(920, 75);
            lstTopWords.Name = "lstTopWords";
            lstTopWords.Size = new Size(229, 540);
            lstTopWords.TabIndex = 8;
            // 
            // lblManualTextEntry
            // 
            lblManualTextEntry.AutoSize = true;
            lblManualTextEntry.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblManualTextEntry.ForeColor = Color.FromArgb(220, 220, 220);
            lblManualTextEntry.Location = new Point(25, 30);
            lblManualTextEntry.Name = "lblManualTextEntry";
            lblManualTextEntry.Size = new Size(209, 31);
            lblManualTextEntry.TabIndex = 9;
            lblManualTextEntry.Text = "Manual Text Entry";
            // 
            // lblTopWordFrequency
            // 
            lblTopWordFrequency.AutoSize = true;
            lblTopWordFrequency.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTopWordFrequency.ForeColor = Color.FromArgb(220, 220, 220);
            lblTopWordFrequency.Location = new Point(904, 30);
            lblTopWordFrequency.Name = "lblTopWordFrequency";
            lblTopWordFrequency.Size = new Size(266, 31);
            lblTopWordFrequency.TabIndex = 10;
            lblTopWordFrequency.Text = "Top 10 Word Frequency";
            // 
            // btnLoadFile
            // 
            btnLoadFile.BackColor = Color.FromArgb(50, 50, 50);
            btnLoadFile.Cursor = Cursors.Hand;
            btnLoadFile.FlatAppearance.BorderSize = 0;
            btnLoadFile.FlatAppearance.MouseDownBackColor = Color.FromArgb(35, 35, 35);
            btnLoadFile.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            btnLoadFile.FlatStyle = FlatStyle.Flat;
            btnLoadFile.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnLoadFile.ForeColor = Color.FromArgb(220, 220, 220);
            btnLoadFile.Location = new Point(25, 555);
            btnLoadFile.Name = "btnLoadFile";
            btnLoadFile.Size = new Size(315, 42);
            btnLoadFile.TabIndex = 11;
            btnLoadFile.Text = "Load .txt File";
            btnLoadFile.UseVisualStyleBackColor = false;
            btnLoadFile.Click += btnLoadFile_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(50, 50, 50);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatAppearance.MouseDownBackColor = Color.FromArgb(35, 35, 35);
            btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnClear.ForeColor = Color.FromArgb(220, 220, 220);
            btnClear.Location = new Point(350, 555);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(305, 42);
            btnClear.TabIndex = 12;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(1170, 620);
            Controls.Add(btnClear);
            Controls.Add(btnLoadFile);
            Controls.Add(lblTopWordFrequency);
            Controls.Add(lblManualTextEntry);
            Controls.Add(lstTopWords);
            Controls.Add(lblReadabilityScore);
            Controls.Add(lblAvgSentenceLength);
            Controls.Add(lblParagraphCount);
            Controls.Add(lblSentenceCount);
            Controls.Add(lblWordCount);
            Controls.Add(btnExportJson);
            Controls.Add(btnAnalyze);
            Controls.Add(txtManualInput);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "F# Text Analyzer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtManualInput;
        private Button btnAnalyze;
        private Button btnExportJson;
        private Label lblWordCount;
        private Label lblSentenceCount;
        private Label lblParagraphCount;
        private Label lblAvgSentenceLength;
        private Label lblReadabilityScore;
        private ListBox lstTopWords;
        private Label lblManualTextEntry;
        private Label lblTopWordFrequency;
        private Button btnLoadFile;
        private Button btnClear;
    }
}
