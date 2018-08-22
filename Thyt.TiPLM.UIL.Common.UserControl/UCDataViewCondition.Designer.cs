namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCDataViewCondition {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing) {
            Delegates.Instance.D_AfterNodeSelected = (PLMSimpleDelegate2)System.Delegate.Remove(Delegates.Instance.D_AfterNodeSelected, new PLMSimpleDelegate2(this.NodeSelected));
            Delegates.Instance.D_AfterNodeDeleted = (PLMSimpleDelegate2)System.Delegate.Remove(Delegates.Instance.D_AfterNodeDeleted, new PLMSimpleDelegate2(this.NodeDeleted));
            Delegates.Instance.D_AfterRightClassChanged = (PLMSimpleDelegate2)System.Delegate.Remove(Delegates.Instance.D_AfterRightClassChanged, new PLMSimpleDelegate2(this.NodeRightClassChanged));
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
            this.GbxFilter = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.tabCon_Filter = new Thyt.TiPLM.UIL.Controls.TabControlPLM();
            this.tabPage_AttrOption = new Thyt.TiPLM.UIL.Controls.TabPagePLM();
            this.pnlFilter = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.tabPage_ClsRule = new Thyt.TiPLM.UIL.Controls.TabPagePLM();
            this.pnlClsFunc = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.tabPage_AttrRule = new Thyt.TiPLM.UIL.Controls.TabPagePLM();
            this.pnlAttrFunc = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.rbtClass = new System.Windows.Forms.RadioButton();
            this.btnSetConditionFilter = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.tvwConditionTreeFilter = new System.Windows.Forms.TreeView();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.btnSetParaFilter = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.rbtRelation = new System.Windows.Forms.RadioButton();
            this.GbxFilter.SuspendLayout();
            this.tabCon_Filter.SuspendLayout();
            this.tabPage_AttrOption.SuspendLayout();
            this.tabPage_ClsRule.SuspendLayout();
            this.tabPage_AttrRule.SuspendLayout();
            base.SuspendLayout();
            this.GbxFilter.Controls.Add(this.tabCon_Filter);
            this.GbxFilter.Controls.Add(this.rbtClass);
            this.GbxFilter.Controls.Add(this.btnSetConditionFilter);
            this.GbxFilter.Controls.Add(this.tvwConditionTreeFilter);
            this.GbxFilter.Controls.Add(this.label1);
            this.GbxFilter.Controls.Add(this.label2);
            this.GbxFilter.Controls.Add(this.btnSetParaFilter);
            this.GbxFilter.Controls.Add(this.rbtRelation);
            this.GbxFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GbxFilter.Location = new System.Drawing.Point(0, 0);
            this.GbxFilter.Name = "GbxFilter";
            this.GbxFilter.Size = new System.Drawing.Size(730, 0xfc);
            this.GbxFilter.TabIndex = 1;
            this.GbxFilter.TabStop = false;
            this.GbxFilter.Text = "设置过滤条件";
            this.tabCon_Filter.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.tabCon_Filter.TabPages.Add(this.tabPage_AttrOption);
            this.tabCon_Filter.TabPages.Add(this.tabPage_ClsRule);
            this.tabCon_Filter.TabPages.Add(this.tabPage_AttrRule);
            this.tabCon_Filter.Location = new System.Drawing.Point(0x180, 0x58);
            this.tabCon_Filter.Name = "tabCon_Filter";
            this.tabCon_Filter.SelectedIndex = 0;
            this.tabCon_Filter.Size = new System.Drawing.Size(0x150, 120);
            this.tabCon_Filter.TabIndex = 8;
            this.tabPage_AttrOption.Controls.Add(this.pnlFilter);
            this.tabPage_AttrOption.Location = new System.Drawing.Point(4, 0x16);
            this.tabPage_AttrOption.Name = "tabPage_AttrOption";
            this.tabPage_AttrOption.Size = new System.Drawing.Size(0x148, 0x5e);
            this.tabPage_AttrOption.TabIndex = 0;
            this.tabPage_AttrOption.Text = "属性条件";
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(0x148, 0x5e);
            this.pnlFilter.TabIndex = 0;
            this.tabPage_ClsRule.Controls.Add(this.pnlClsFunc);
            this.tabPage_ClsRule.Location = new System.Drawing.Point(4, 0x16);
            this.tabPage_ClsRule.Name = "tabPage_ClsRule";
            this.tabPage_ClsRule.Size = new System.Drawing.Size(0x148, 0x5e);
            this.tabPage_ClsRule.TabIndex = 1;
            this.tabPage_ClsRule.Text = "类函数";
            this.pnlClsFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClsFunc.Location = new System.Drawing.Point(0, 0);
            this.pnlClsFunc.Name = "pnlClsFunc";
            this.pnlClsFunc.Size = new System.Drawing.Size(0x148, 0x5f);
            this.pnlClsFunc.TabIndex = 0;
            this.tabPage_AttrRule.Controls.Add(this.pnlAttrFunc);
            this.tabPage_AttrRule.Location = new System.Drawing.Point(4, 0x16);
            this.tabPage_AttrRule.Name = "tabPage_AttrRule";
            this.tabPage_AttrRule.Size = new System.Drawing.Size(0x148, 0x5e);
            this.tabPage_AttrRule.TabIndex = 2;
            this.tabPage_AttrRule.Text = "属性函数";
            this.pnlAttrFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAttrFunc.Location = new System.Drawing.Point(0, 0);
            this.pnlAttrFunc.Name = "pnlAttrFunc";
            this.pnlAttrFunc.Size = new System.Drawing.Size(0x148, 0x5f);
            this.pnlAttrFunc.TabIndex = 0;
            this.rbtClass.Checked = true;
            this.rbtClass.Location = new System.Drawing.Point(0x178, 0x40);
            this.rbtClass.Name = "rbtClass";
            this.rbtClass.Size = new System.Drawing.Size(0x148, 0x18);
            this.rbtClass.TabIndex = 7;
            this.rbtClass.TabStop = true;
            this.rbtClass.Text = "类";
            this.rbtClass.Click += new System.EventHandler(this.rbtClass_Click);
            this.btnSetConditionFilter.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSetConditionFilter.Location = new System.Drawing.Point(0x27a, 0xd7);
            this.btnSetConditionFilter.Name = "btnSetConditionFilter";
            this.btnSetConditionFilter.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnSetConditionFilter.TabIndex = 2;
            this.btnSetConditionFilter.Text = "保存条件";
            this.btnSetConditionFilter.Click += new System.EventHandler(this.btnSetConditionFilter_Click);
            this.tvwConditionTreeFilter.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.tvwConditionTreeFilter.CheckBoxes = true;
            this.tvwConditionTreeFilter.HideSelection = false;
            this.tvwConditionTreeFilter.Location = new System.Drawing.Point(0x10, 40);
            this.tvwConditionTreeFilter.Name = "tvwConditionTreeFilter";
            this.tvwConditionTreeFilter.Size = new System.Drawing.Size(0x158, 0xc7);
            this.tvwConditionTreeFilter.TabIndex = 3;
            this.tvwConditionTreeFilter.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwConditionTreeFilter_BeforeCheck);
            this.tvwConditionTreeFilter.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwConditionTreeFilter_AfterSelect);
            this.tvwConditionTreeFilter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwConditionTreeFilter_MouseUp);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x2f, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "条件树:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x178, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "当前选中类/关联:";
            this.btnSetParaFilter.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSetParaFilter.Location = new System.Drawing.Point(0x178, 0xd7);
            this.btnSetParaFilter.Name = "btnSetParaFilter";
            this.btnSetParaFilter.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnSetParaFilter.TabIndex = 4;
            this.btnSetParaFilter.Text = "设为参数";
            this.btnSetParaFilter.Visible = false;
            this.btnSetParaFilter.Click += new System.EventHandler(this.btnSetParaFilter_Click);
            this.rbtRelation.Location = new System.Drawing.Point(0x178, 40);
            this.rbtRelation.Name = "rbtRelation";
            this.rbtRelation.Size = new System.Drawing.Size(0x148, 0x18);
            this.rbtRelation.TabIndex = 7;
            this.rbtRelation.Text = "关联";
            this.rbtRelation.Click += new System.EventHandler(this.rbtRelation_Click);
            base.Controls.Add(this.GbxFilter);
            base.Name = "UCDataViewCondition";
            base.Size = new System.Drawing.Size(730, 0xfc);
            this.GbxFilter.ResumeLayout(false);
            this.GbxFilter.PerformLayout();
            this.tabCon_Filter.ResumeLayout(false);
            this.tabPage_AttrOption.ResumeLayout(false);
            this.tabPage_ClsRule.ResumeLayout(false);
            this.tabPage_AttrRule.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion  
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnSetConditionFilter;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnSetParaFilter;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM GbxFilter;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlAttrFunc;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlClsFunc;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlFilter;
        private System.Windows.Forms.RadioButton rbtClass;
        private System.Windows.Forms.RadioButton rbtRelation;
        private Thyt.TiPLM.UIL.Controls.TabControlPLM tabCon_Filter;
        private Thyt.TiPLM.UIL.Controls.TabPagePLM tabPage_AttrOption;
        private Thyt.TiPLM.UIL.Controls.TabPagePLM tabPage_AttrRule;
        private Thyt.TiPLM.UIL.Controls.TabPagePLM tabPage_ClsRule;
        public System.Windows.Forms.TreeView tvwConditionTreeFilter;
    }
}