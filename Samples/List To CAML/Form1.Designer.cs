namespace SharePoint_Hierachie
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tvStructure = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtURL = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbViews = new System.Windows.Forms.ComboBox();
            this.btnGetCAML = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tvStructure
            // 
            this.tvStructure.ImageIndex = 0;
            this.tvStructure.ImageList = this.imageList1;
            this.tvStructure.Location = new System.Drawing.Point(38, 92);
            this.tvStructure.Name = "tvStructure";
            this.tvStructure.SelectedImageIndex = 0;
            this.tvStructure.Size = new System.Drawing.Size(281, 319);
            this.tvStructure.TabIndex = 0;
            this.tvStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.NodeSelected);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "earth.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(38, 48);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(198, 20);
            this.txtURL.TabIndex = 1;
            this.txtURL.Text = "http://chiron";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Anzeigen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Site Collection";
            // 
            // cbViews
            // 
            this.cbViews.FormattingEnabled = true;
            this.cbViews.Location = new System.Drawing.Point(38, 452);
            this.cbViews.Name = "cbViews";
            this.cbViews.Size = new System.Drawing.Size(198, 21);
            this.cbViews.TabIndex = 4;
            // 
            // btnGetCAML
            // 
            this.btnGetCAML.Location = new System.Drawing.Point(244, 452);
            this.btnGetCAML.Name = "btnGetCAML";
            this.btnGetCAML.Size = new System.Drawing.Size(75, 23);
            this.btnGetCAML.TabIndex = 5;
            this.btnGetCAML.Text = "Copy";
            this.btnGetCAML.UseVisualStyleBackColor = true;
            this.btnGetCAML.Click += new System.EventHandler(this.GetCAML);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 425);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Views";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 512);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetCAML);
            this.Controls.Add(this.cbViews);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.tvStructure);
            this.Name = "Form1";
            this.Text = "View 2 CAML";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvStructure;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbViews;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetCAML;
    }
}

