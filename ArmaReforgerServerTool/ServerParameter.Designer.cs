﻿namespace ReforgerServerApp
{
    partial class ServerParameter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            parameterName = new Label();
            SuspendLayout();
            // 
            // parameterName
            // 
            parameterName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            parameterName.AutoSize = true;
            parameterName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            parameterName.Location = new Point(15, 4);
            parameterName.Name = "parameterName";
            parameterName.Size = new Size(132, 21);
            parameterName.TabIndex = 0;
            parameterName.Text = "";
            parameterName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ServerParameter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(parameterName);
            Name = "ServerParameter";
            Size = new Size(400, 30);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label parameterName;
    }
}
