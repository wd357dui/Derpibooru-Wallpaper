using DerpibooruWallpaper.Resources;

namespace DerpibooruWallpaper
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            APIKeyTextBox = new TextBox();
            SaveAPIKeyButton = new Button();
            SearchParamTextBox = new TextBox();
            SaveSearchParamButton = new Button();
            DaysRadioButton = new RadioButton();
            HoursRadioButton = new RadioButton();
            MinutesRadioButton = new RadioButton();
            WallpaperChangeIntervalTextBox = new TextBox();
            SaveWallpaperChangeIntervalButton = new Button();
            OverrideExistingImageFilesCheckBox = new CheckBox();
            LaunchOnSystemStartupCheckBox = new CheckBox();
            UseHttpsCheckBox = new CheckBox();
            UseSystemProxyCheckBox = new CheckBox();
            UseTrixiebooruMirrorCheckBox = new CheckBox();
            APIKeyGroupBox = new GroupBox();
            SearchParamGroupBox = new GroupBox();
            WallpaperChangeIntervalGroupBox = new GroupBox();
            ProgramSettingsGroupBox = new GroupBox();
            ConnectionSettingsGroupBox = new GroupBox();
            OKButton = new Button();
            APIKeyGroupBox.SuspendLayout();
            SearchParamGroupBox.SuspendLayout();
            WallpaperChangeIntervalGroupBox.SuspendLayout();
            ProgramSettingsGroupBox.SuspendLayout();
            ConnectionSettingsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // APIKeyTextBox
            // 
            resources.ApplyResources(APIKeyTextBox, "APIKeyTextBox");
            APIKeyTextBox.Name = "APIKeyTextBox";
            APIKeyTextBox.UseSystemPasswordChar = true;
            APIKeyTextBox.TextChanged += APIKeyTextBox_TextChanged;
            APIKeyTextBox.MouseDoubleClick += APIKeyTextBox_MouseDoubleClick;
            // 
            // SaveAPIKeyButton
            // 
            resources.ApplyResources(SaveAPIKeyButton, "SaveAPIKeyButton");
            SaveAPIKeyButton.Name = "SaveAPIKeyButton";
            SaveAPIKeyButton.UseVisualStyleBackColor = true;
            SaveAPIKeyButton.Click += SaveAPIKeyButton_Click;
            // 
            // SearchParamTextBox
            // 
            resources.ApplyResources(SearchParamTextBox, "SearchParamTextBox");
            SearchParamTextBox.Name = "SearchParamTextBox";
            SearchParamTextBox.TextChanged += SearchParamTextBox_TextChanged;
            // 
            // SaveSearchParamButton
            // 
            resources.ApplyResources(SaveSearchParamButton, "SaveSearchParamButton");
            SaveSearchParamButton.Name = "SaveSearchParamButton";
            SaveSearchParamButton.UseVisualStyleBackColor = true;
            SaveSearchParamButton.Click += SaveSearchButton_Click;
            // 
            // DaysRadioButton
            // 
            resources.ApplyResources(DaysRadioButton, "DaysRadioButton");
            DaysRadioButton.Name = "DaysRadioButton";
            DaysRadioButton.TabStop = true;
            DaysRadioButton.UseVisualStyleBackColor = true;
            DaysRadioButton.CheckedChanged += DaysRadioButton_CheckedChanged;
            // 
            // HoursRadioButton
            // 
            resources.ApplyResources(HoursRadioButton, "HoursRadioButton");
            HoursRadioButton.Name = "HoursRadioButton";
            HoursRadioButton.TabStop = true;
            HoursRadioButton.UseVisualStyleBackColor = true;
            HoursRadioButton.CheckedChanged += HoursRadioButton_CheckedChanged;
            // 
            // MinutesRadioButton
            // 
            resources.ApplyResources(MinutesRadioButton, "MinutesRadioButton");
            MinutesRadioButton.Name = "MinutesRadioButton";
            MinutesRadioButton.TabStop = true;
            MinutesRadioButton.UseVisualStyleBackColor = true;
            MinutesRadioButton.CheckedChanged += MinutesRadioButton_CheckedChanged;
            // 
            // WallpaperChangeIntervalTextBox
            // 
            resources.ApplyResources(WallpaperChangeIntervalTextBox, "WallpaperChangeIntervalTextBox");
            WallpaperChangeIntervalTextBox.Name = "WallpaperChangeIntervalTextBox";
            WallpaperChangeIntervalTextBox.TextChanged += WallpaperChangeIntervalTextBox_TextChanged;
            // 
            // SaveWallpaperChangeIntervalButton
            // 
            resources.ApplyResources(SaveWallpaperChangeIntervalButton, "SaveWallpaperChangeIntervalButton");
            SaveWallpaperChangeIntervalButton.Name = "SaveWallpaperChangeIntervalButton";
            SaveWallpaperChangeIntervalButton.UseVisualStyleBackColor = true;
            SaveWallpaperChangeIntervalButton.Click += SaveWallpaperChangeIntervalButton_Click;
            // 
            // OverrideExistingImageFilesCheckBox
            // 
            resources.ApplyResources(OverrideExistingImageFilesCheckBox, "OverrideExistingImageFilesCheckBox");
            OverrideExistingImageFilesCheckBox.Name = "OverrideExistingImageFilesCheckBox";
            OverrideExistingImageFilesCheckBox.UseVisualStyleBackColor = true;
            OverrideExistingImageFilesCheckBox.CheckedChanged += OverrideExistingFilesCheckBox_CheckedChanged;
            // 
            // LaunchOnSystemStartupCheckBox
            // 
            resources.ApplyResources(LaunchOnSystemStartupCheckBox, "LaunchOnSystemStartupCheckBox");
            LaunchOnSystemStartupCheckBox.Name = "LaunchOnSystemStartupCheckBox";
            LaunchOnSystemStartupCheckBox.UseVisualStyleBackColor = true;
            LaunchOnSystemStartupCheckBox.CheckedChanged += LaunchOnSystemStartupCheckBox_CheckedChanged;
            // 
            // UseHttpsCheckBox
            // 
            resources.ApplyResources(UseHttpsCheckBox, "UseHttpsCheckBox");
            UseHttpsCheckBox.Checked = true;
            UseHttpsCheckBox.CheckState = CheckState.Checked;
            UseHttpsCheckBox.Name = "UseHttpsCheckBox";
            UseHttpsCheckBox.UseVisualStyleBackColor = true;
            UseHttpsCheckBox.CheckedChanged += UseHttpsCheckBox_CheckedChanged;
            // 
            // UseSystemProxyCheckBox
            // 
            resources.ApplyResources(UseSystemProxyCheckBox, "UseSystemProxyCheckBox");
            UseSystemProxyCheckBox.Checked = true;
            UseSystemProxyCheckBox.CheckState = CheckState.Checked;
            UseSystemProxyCheckBox.Name = "UseSystemProxyCheckBox";
            UseSystemProxyCheckBox.UseVisualStyleBackColor = true;
            UseSystemProxyCheckBox.CheckedChanged += UseSystemProxyCheckBox_CheckedChanged;
            // 
            // UseTrixiebooruMirrorCheckBox
            // 
            resources.ApplyResources(UseTrixiebooruMirrorCheckBox, "UseTrixiebooruMirrorCheckBox");
            UseTrixiebooruMirrorCheckBox.Checked = true;
            UseTrixiebooruMirrorCheckBox.CheckState = CheckState.Checked;
            UseTrixiebooruMirrorCheckBox.Name = "UseTrixiebooruMirrorCheckBox";
            UseTrixiebooruMirrorCheckBox.UseVisualStyleBackColor = true;
            UseTrixiebooruMirrorCheckBox.CheckedChanged += UseTrixiebooruMirrorCheckBox_CheckedChanged;
            // 
            // APIKeyGroupBox
            // 
            resources.ApplyResources(APIKeyGroupBox, "APIKeyGroupBox");
            APIKeyGroupBox.BackColor = Color.Transparent;
            APIKeyGroupBox.Controls.Add(APIKeyTextBox);
            APIKeyGroupBox.Controls.Add(SaveAPIKeyButton);
            APIKeyGroupBox.Name = "APIKeyGroupBox";
            APIKeyGroupBox.TabStop = false;
            // 
            // SearchParamGroupBox
            // 
            resources.ApplyResources(SearchParamGroupBox, "SearchParamGroupBox");
            SearchParamGroupBox.BackColor = Color.Transparent;
            SearchParamGroupBox.Controls.Add(SearchParamTextBox);
            SearchParamGroupBox.Controls.Add(SaveSearchParamButton);
            SearchParamGroupBox.Name = "SearchParamGroupBox";
            SearchParamGroupBox.TabStop = false;
            // 
            // WallpaperChangeIntervalGroupBox
            // 
            resources.ApplyResources(WallpaperChangeIntervalGroupBox, "WallpaperChangeIntervalGroupBox");
            WallpaperChangeIntervalGroupBox.BackColor = Color.Transparent;
            WallpaperChangeIntervalGroupBox.Controls.Add(SaveWallpaperChangeIntervalButton);
            WallpaperChangeIntervalGroupBox.Controls.Add(WallpaperChangeIntervalTextBox);
            WallpaperChangeIntervalGroupBox.Controls.Add(MinutesRadioButton);
            WallpaperChangeIntervalGroupBox.Controls.Add(HoursRadioButton);
            WallpaperChangeIntervalGroupBox.Controls.Add(DaysRadioButton);
            WallpaperChangeIntervalGroupBox.Name = "WallpaperChangeIntervalGroupBox";
            WallpaperChangeIntervalGroupBox.TabStop = false;
            // 
            // ProgramSettingsGroupBox
            // 
            resources.ApplyResources(ProgramSettingsGroupBox, "ProgramSettingsGroupBox");
            ProgramSettingsGroupBox.BackColor = Color.Transparent;
            ProgramSettingsGroupBox.Controls.Add(LaunchOnSystemStartupCheckBox);
            ProgramSettingsGroupBox.Controls.Add(OverrideExistingImageFilesCheckBox);
            ProgramSettingsGroupBox.Name = "ProgramSettingsGroupBox";
            ProgramSettingsGroupBox.TabStop = false;
            // 
            // ConnectionSettingsGroupBox
            // 
            resources.ApplyResources(ConnectionSettingsGroupBox, "ConnectionSettingsGroupBox");
            ConnectionSettingsGroupBox.BackColor = Color.Transparent;
            ConnectionSettingsGroupBox.Controls.Add(UseHttpsCheckBox);
            ConnectionSettingsGroupBox.Controls.Add(UseSystemProxyCheckBox);
            ConnectionSettingsGroupBox.Controls.Add(UseTrixiebooruMirrorCheckBox);
            ConnectionSettingsGroupBox.Name = "ConnectionSettingsGroupBox";
            ConnectionSettingsGroupBox.TabStop = false;
            // 
            // OKButton
            // 
            resources.ApplyResources(OKButton, "OKButton");
            OKButton.Name = "OKButton";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resources.Resources.Watermark;
            Controls.Add(WallpaperChangeIntervalGroupBox);
            Controls.Add(ProgramSettingsGroupBox);
            Controls.Add(SearchParamGroupBox);
            Controls.Add(APIKeyGroupBox);
            Controls.Add(ConnectionSettingsGroupBox);
            Controls.Add(OKButton);
            DoubleBuffered = true;
            Icon = Resources.Resources.Derpibooru;
            Name = "Settings";
            FormClosing += Settings_FormClosing;
            FormClosed += Settings_FormClosed;
            Load += Settings_Load;
            APIKeyGroupBox.ResumeLayout(false);
            APIKeyGroupBox.PerformLayout();
            SearchParamGroupBox.ResumeLayout(false);
            SearchParamGroupBox.PerformLayout();
            WallpaperChangeIntervalGroupBox.ResumeLayout(false);
            WallpaperChangeIntervalGroupBox.PerformLayout();
            ProgramSettingsGroupBox.ResumeLayout(false);
            ProgramSettingsGroupBox.PerformLayout();
            ConnectionSettingsGroupBox.ResumeLayout(false);
            ConnectionSettingsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox APIKeyTextBox;
        private Button SaveAPIKeyButton;
        private TextBox SearchParamTextBox;
        private Button SaveSearchParamButton;
        private RadioButton DaysRadioButton;
        private RadioButton HoursRadioButton;
        private RadioButton MinutesRadioButton;
        private TextBox WallpaperChangeIntervalTextBox;
        private Button SaveWallpaperChangeIntervalButton;
        private CheckBox OverrideExistingImageFilesCheckBox;
        private CheckBox LaunchOnSystemStartupCheckBox;
        private CheckBox UseHttpsCheckBox;
        private CheckBox UseSystemProxyCheckBox;
        private CheckBox UseTrixiebooruMirrorCheckBox;
        private GroupBox APIKeyGroupBox;
        private GroupBox SearchParamGroupBox;
        private GroupBox WallpaperChangeIntervalGroupBox;
        private GroupBox ProgramSettingsGroupBox;
        private GroupBox ConnectionSettingsGroupBox;
        private Button OKButton;
    }
}