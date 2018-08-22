namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCAttrUser2Org {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing) {
            if (disposing && (this.components != null)) {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary> 
        ///        
        private void InitializeComponent() {
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label5 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cb_operator = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label3 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label4 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cb_operator.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x56, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "所属组织";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 0x2d);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0x3b, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "隶属关系:";
            this.cb_operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_operator.Properties.Items.AddRange(new object[] { "等同", "包含", "属于", "包含或等同", "属于或等同" });
            this.cb_operator.Location = new System.Drawing.Point(0x4f, 0x2d);
            this.cb_operator.Name = "cb_operator";
            this.cb_operator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cb_operator.Size = new System.Drawing.Size(0x90, 20);
            this.cb_operator.TabIndex = 14;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x4d, 0x52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0x35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "所属组织";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x41, 12);
            this.label2.TabIndex = 0x13;
            this.label2.Text = "对象创建人";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 0x52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0x35, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "登录用户";
            base.AutoScaleMode =  System.Windows.Forms.AutoScaleMode.Inherit;
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.cb_operator);
            base.Controls.Add(this.label1);
            base.Name = "UCAttrUser2Org";
            base.Size = new System.Drawing.Size(0xf5, 0x73);
            this.cb_operator.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }  
         
        #endregion  
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cb_operator;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label3;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label4;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label5;
    }
}