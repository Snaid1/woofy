using System;
using System.Drawing;
using System.Windows.Forms;
using Woofy.Core;
using Woofy.Enums;
using Woofy.Gui;
using Woofy.Properties;
using Woofy.Settings;

namespace Woofy.Flows.Main
{
	public partial class MainForm : Form
	{
		public IMainPresenter Presenter { get; set; }
        
		public MainForm()
		{
			InitializeComponent();

			RegisterCommands();
		}

		private void RegisterCommands()
		{
			tsbAddComic.Click += (o, e) => Presenter.AddComic();
			tsbSettings.Click += (o, e) =>
			{
				using (var settingsForm = new SettingsForm())
					settingsForm.ShowDialog();
			};
			txtAppLog.LinkClicked += (o, e) => Presenter.Open(e.LinkText);
			txtAppLog.TextChanged += (o, e) =>
			{
				txtAppLog.SelectionStart = txtAppLog.Text.Length;
				txtAppLog.ScrollToCaret();
			};

            tsbOpenFolder.Click += (o, e) => {
                var comic = GetSelectedComic();
                if (comic == null)
                    return;
                
                Presenter.OpenFolder(comic.Id);
            };

			tsbDonate.Click += (o, e) => Presenter.Donate();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			Icon = Resources.ApplicationIcon;
			dgvwTasks.AutoGenerateColumns = false;
			txtAppLog.DataBindings.Add(new Binding("Text", Presenter, "AppLog"));

			Presenter.Initialize(this);
			dgvwTasks.DataSource = Presenter.Comics;
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.N)
				Presenter.AddComic();
		}

		#region OBSOLETE
		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (!UserSettingsOld.MinimizeToTray)
				return;

			if (WindowState == FormWindowState.Minimized)
				Hide();
		}

		private void DisplayDownloadOutcome(DataGridViewRow row, DownloadOutcome downloadOutcome, string url)
		{
			Bitmap icon;
			string toolTip;

			switch (downloadOutcome)
			{
				case DownloadOutcome.Successful:
					icon = Resources.Finished;
					toolTip = "";
					break;
				case DownloadOutcome.NoStripMatchesRuleBroken:
					icon = Resources.Warning;
					toolTip = string.Format("No strips have been found on {0}. In order to allow missing strips, use the allowMissingStrips attribute in the comic definition.", url);
					break;
				case DownloadOutcome.MultipleStripMatchesRuleBroken:
					icon = Resources.Warning;
					toolTip = string.Format("Multiple strips have been found on {0}. In order to allow multiple strips, use the allowMultipleStrips attribute in the comic definition.", url);
					break;
				case DownloadOutcome.Error:
					icon = Resources.Error;
					toolTip = string.IsNullOrEmpty(url) ?
						"An error has occurred while downloading the latest strip." :
						string.Format("An error has occurred while downloading the strip at {0}.", url);
					break;
				default:
					throw new System.ComponentModel.InvalidEnumArgumentException("downloadOutcome", (int)downloadOutcome, typeof(DownloadOutcome));
			}

			row.Cells["TaskStatusColumn"].Value = icon;
			foreach (DataGridViewCell cell in row.Cells)
			{
				cell.ToolTipText = toolTip;
			}
		}

		public void HideOrShow()
		{
			if (Visible)
			{
				WindowState = FormWindowState.Minimized;
				Visible = false;
			}
			else
			{
				Visible = true;
				WindowState = FormWindowState.Normal;
			}
		}

		/// <summary>
		/// Initializes the download progress form for downloading updates, on the UI thread. Also, hides itself.
		/// </summary>
		/// <param name="downloadFileSize">The size of the file to download. Needed for initializing the download progress form.</param>
		public void InitializeUpdatesDownloadProgressForm(int downloadFileSize)
		{
			Invoke(new MethodInvoker(
				delegate
				{
					var downloadProgressForm = new DownloadProgressForm(downloadFileSize);
					downloadProgressForm.Show();

					Hide();
				}
			));
		}
		
		#endregion

		private void OnGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
#warning I should extract these indexes and place them in some instance members
			if (e.ColumnIndex != dgvwTasks.Columns.IndexOf(colStatus) || e.Value == null)
				return;

			var status = (ComicViewModel.ComicStatus)e.Value;
			switch (status)
			{
				case ComicViewModel.ComicStatus.Paused:
					e.Value = Resources.Paused;
					break;
				case ComicViewModel.ComicStatus.Running:
					e.Value = Resources.Running;
					break;
				case ComicViewModel.ComicStatus.Finished:
					e.Value = Resources.Finished;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnGridCellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != dgvwTasks.Columns.IndexOf(colCurrentPage))
				return;

			var value = dgvwTasks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
			if (value == null)
				return;

			Presenter.Open(value);
		}

		private void OnToggleComicState(object sender, EventArgs e)
		{
			var comic = GetSelectedComic();
			if (comic == null)
				return;

			Presenter.ToggleComicState(comic.Id);
		}

		private void OnRemoveComic(object sender, EventArgs e)
		{
			var comic = GetSelectedComic();
			if (comic == null)
				return;

			var remove = MessageBox.Show("Are you sure you want to remove {0}?".FormatTo(comic.Name), "Woofy", MessageBoxButtons.YesNo);
			if (remove != DialogResult.Yes)
				return;

			Presenter.Remove(comic.Id);
		}

		private ComicViewModel GetSelectedComic()
		{
			if (dgvwTasks.SelectedRows.Count == 0)
				return null;
			return (ComicViewModel)dgvwTasks.SelectedRows[0].DataBoundItem;
		}
	}
}