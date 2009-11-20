using System;
using System.Windows.Forms;
using System.IO;

using Woofy.Core;
using Woofy.Settings;

namespace Woofy.Gui
{
    public partial class TaskDetailsForm : Form
    {
        #region Instance Members
        private readonly ComicTasksController _tasksController;
        #endregion

        #region .ctor
        public TaskDetailsForm(ComicTasksController tasksController)
        {
            InitializeComponent();

            _tasksController = tasksController;
        }
        #endregion

        #region Events - Form
        private void TaskDetails_Load(object sender, EventArgs e)
        {
            cbComics.DataSource = ComicDefinition.GetAvailableComicDefinitions();

            if (!string.IsNullOrEmpty(UsrSettings.LastUsedComicDefinitionFile))
            {
                int i = 0;
                foreach (ComicDefinition comicInfo in cbComics.Items)
                {
                    if (comicInfo.ComicInfoFile.Equals(UsrSettings.LastUsedComicDefinitionFile, StringComparison.OrdinalIgnoreCase))
                    {
                        cbComics.SelectedIndex = i;
                        break;
                    }

                    i++;
                }
            }

            if (UsrSettings.LastNumberOfComicsToDownload.HasValue)
            {
                rbDownloadLast.Checked = true;
                numComicsToDownload.Value = (decimal)UsrSettings.LastNumberOfComicsToDownload;
            }
            else
            {
                rbDownloadOnlyNew.Checked = true;
            }

            UpdateDownloadFolder();            
        }        
        #endregion

        #region Events - Clicks
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbComics.SelectedItem == null)
                return;

            ComicDefinition comicInfo = (ComicDefinition)cbComics.SelectedItem;
            long? comicsToDownload;
            if (rbDownloadLast.Checked)
                comicsToDownload = (long)numComicsToDownload.Value;
            else
                comicsToDownload = null;
            string downloadFolder = txtDownloadFolder.Text;
            string startUrl = chkOverrideStartUrl.Checked ? txtOverrideStartUrl.Text : comicInfo.StartUrl;

            ComicTask task = new ComicTask(comicInfo.FriendlyName, comicInfo.ComicInfoFile, comicsToDownload, downloadFolder, startUrl);
            bool taskAdded = _tasksController.AddNewTask(task);

            if (!taskAdded)
            {
                errorProvider.SetError(cbComics, "A task for this comic is already running or paused.");
                return;
            }

            UpdateUserSettings();
            this.DialogResult = DialogResult.OK;
        }

        private void numComicsToDownload_Click(object sender, EventArgs e)
        {
            rbDownloadLast.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UsrSettings.LoadData();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
                txtDownloadFolder.Text = folderBrowser.SelectedPath;
        }
        #endregion

        #region Helper Methods
        private void UpdateUserSettings()
        {
            UsrSettings.LastUsedComicDefinitionFile = ((ComicDefinition)cbComics.SelectedValue).ComicInfoFile;

            if (rbDownloadOnlyNew.Checked)
                UsrSettings.LastNumberOfComicsToDownload = null;
            else
                UsrSettings.LastNumberOfComicsToDownload = (long)numComicsToDownload.Value;

            UsrSettings.SaveData();
        }

        private void UpdateDownloadFolder()
        {
            ComicDefinition comicInfo = (ComicDefinition)cbComics.SelectedValue;
            txtDownloadFolder.Text = Path.Combine(UsrSettings.DefaultDownloadFolder, comicInfo.FriendlyName);
        }
        #endregion

        #region Events - cbComics
        private void cbComics_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDownloadFolder();
        } 
        #endregion                

        #region Events - chkOverrideStartUrl
        private void chkOverrideStartUrl_CheckedChanged(object sender, EventArgs e)
        {
            txtOverrideStartUrl.Enabled = chkOverrideStartUrl.Checked;
        } 
        #endregion
    }
}