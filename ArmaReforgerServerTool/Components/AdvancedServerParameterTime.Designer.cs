﻿namespace ReforgerServerApp.Components
{
    partial class AdvancedServerParameterTime
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
            parameterValue = new DateTimePicker();
            SuspendLayout();
            // 
            // parameterValue
            // 
            parameterValue.Location = new Point(4, 28);
            parameterValue.Name = "parameterValue";
            parameterValue.Size = new Size(191, 23);
            parameterValue.TabIndex = 2;
            parameterValue.Format = DateTimePickerFormat.Custom;
            parameterValue.CustomFormat = "HH:mm";
            parameterValue.ShowUpDown = true;
            // 
            // AdvancedServerParameterTime
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(parameterValue);
            Name = "AdvancedServerParameterTime";
            Controls.SetChildIndex(parameterValue, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker parameterValue;
    }
}
