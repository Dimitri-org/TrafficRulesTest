namespace UniThingy
{
    partial class EnterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.uniThingyDatabaseDataSet = new UniThingy.UniThingyDatabaseDataSet();
            this.questionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.questionsTableAdapter = new UniThingy.UniThingyDatabaseDataSetTableAdapters.QuestionsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.uniThingyDatabaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(357, 375);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Continue";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // uniThingyDatabaseDataSet
            // 
            this.uniThingyDatabaseDataSet.DataSetName = "UniThingyDatabaseDataSet";
            this.uniThingyDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // questionsBindingSource
            // 
            this.questionsBindingSource.DataMember = "Questions";
            this.questionsBindingSource.DataSource = this.uniThingyDatabaseDataSet;
            // 
            // questionsTableAdapter
            // 
            this.questionsTableAdapter.ClearBeforeFill = true;
            // 
            // EnterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 448);
            this.Controls.Add(this.button1);
            this.Name = "EnterForm";
            this.Text = "EnterForm";
            this.Load += new System.EventHandler(this.EnterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uniThingyDatabaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private UniThingyDatabaseDataSet uniThingyDatabaseDataSet;
        private System.Windows.Forms.BindingSource questionsBindingSource;
        private UniThingyDatabaseDataSetTableAdapters.QuestionsTableAdapter questionsTableAdapter;
    }
}