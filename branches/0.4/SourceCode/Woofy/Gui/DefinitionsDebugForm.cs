using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

using log4net.Core;

using Woofy.Core;

namespace Woofy.Gui
{
    public partial class DefinitionsDebugForm : Form
    {
        #region Instance Members
        private ComicsProvider comicsProvider;
        private string currentUrl;
        private TestMode currentMode = TestMode.StandBy;
        #endregion

        #region .ctor
        public DefinitionsDebugForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events - Form
        private void DefinitionsDebugForm_Load(object sender, EventArgs e)
        {
            InitControls();
            this.currentMode = TestMode.StandBy;
            DisplayAppropriateControlsForCurrentMode();

            Logger.ClearDebugMessages();
            Logger.IsDebugging = true;
        }

        private void DefinitionsDebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AbortDebugging();

            Logger.ClearDebugMessages();
            Logger.IsDebugging = false;
        }
        #endregion

        #region Events - clicks
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startDebugButton_Click(object sender, EventArgs e)
        {
            StartDebugging();
        }

        private void comicDefinitionsList_DoubleClick(object sender, EventArgs e)
        {
            StartDebugging();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (this.currentMode == TestMode.Running)
                PauseDebugging();
            else
                StartDebugging();
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            AbortDebugging();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(eventsRichTextBox.SelectedText);
        }

        private void chkOverrideStartUrl_CheckedChanged(object sender, EventArgs e)
        {
            txtOverrideStartUrl.Enabled = chkOverrideStartUrl.Checked;
        }

        private void eventsRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        #endregion

        #region Helper Methods
        private void InitControls()
        {
            foreach (ComicDefinition comicDefinition in ComicDefinition.GetAvailableComicDefinitions())
            {
                ListViewItem item = new ListViewItem(new string[] { comicDefinition.FriendlyName, comicDefinition.Author });
                item.Tag = comicDefinition.ComicInfoFile;

                comicDefinitionsList.Items.Add(item);
            }
            comicDefinitionsList.SelectedIndices.Add(0);

            SetFoundComics(0);
        }

        private void PauseDebugging()
        {
            this.currentMode = TestMode.Paused;
            DisplayAppropriateControlsForCurrentMode();

            this.comicsProvider.StopDownload();
            CheckForLatestDebugMessages();
        }

        private void AbortDebugging()
        {
            this.currentMode = TestMode.StandBy;
            DisplayAppropriateControlsForCurrentMode();

            if (this.comicsProvider != null)
                this.comicsProvider.StopDownload();
            CheckForLatestDebugMessages();
        }

        private void StartDebugging()
        {
            if (comicDefinitionsList.SelectedItems.Count == 0)
            {
                MessageBox.Show("You have to select a comic definition.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (chkOverrideStartUrl.Checked && string.IsNullOrEmpty(txtOverrideStartUrl.Text))
            {
                MessageBox.Show("You have to specify a start url.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            lblFoundStrips.Visible = true;

            if (this.currentMode == TestMode.StandBy)
            {
                eventsRichTextBox.Clear();
                SetFoundComics(0);
            }

            eventsRichTextBox.Focus();

            string selectedFile = (string)comicDefinitionsList.SelectedItems[0].Tag;
            ComicDefinition comicDefinition = new ComicDefinition(selectedFile);

            string startupUrl;
            if (this.currentMode == TestMode.Paused)
                startupUrl = this.currentUrl;
            else
                startupUrl = chkOverrideStartUrl.Checked ? txtOverrideStartUrl.Text : comicDefinition.StartUrl;

            this.currentMode = TestMode.Running;
            DisplayAppropriateControlsForCurrentMode();

            ThreadPool.UnsafeQueueUserWorkItem(
                delegate
                {
                    MonitorDebugMessages();
                }
            , null);

            RunComicTest(comicDefinition, startupUrl);
        }

        private void DisplayAppropriateControlsForCurrentMode()
        {
            switch (this.currentMode)
            {
                case TestMode.StandBy:
                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        startButton.Visible =
                                            closeButton.Visible = true;

                                        pauseButton.Visible =
                                            abortButton.Visible = false;

                                        chkOverrideStartUrl.Enabled =
                                            comicDefinitionsList.Enabled = true;

                                        txtOverrideStartUrl.Enabled = chkOverrideStartUrl.Checked;
                                    }));

                    break;
                case TestMode.Running:
                    this.Invoke(new MethodInvoker(delegate
                                                      {
                                                          startButton.Visible =
                                                              closeButton.Visible = false;

                                                          pauseButton.Visible =
                                                              abortButton.Visible = true;

                                                          chkOverrideStartUrl.Enabled =
                                                              txtOverrideStartUrl.Enabled =
                                                              comicDefinitionsList.Enabled = false;

                                                          pauseButton.Text = "Pause";
                                                      })); 
                    break;
                case TestMode.Paused:
                    this.Invoke(new MethodInvoker(delegate
                                                      {
                                                          startButton.Visible =
                                                              closeButton.Visible = false;

                                                          pauseButton.Visible =
                                                              abortButton.Visible = true;

                                                          chkOverrideStartUrl.Enabled =
                                                              txtOverrideStartUrl.Enabled =
                                                              comicDefinitionsList.Enabled = false;

                                                          pauseButton.Text = "Resume";
                                                      })); 
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private void MonitorDebugMessages()
        {
            try
            {
                while (this.currentMode == TestMode.Running)
                {
                    CheckForLatestDebugMessages();

                    Thread.Sleep(500);
                }

                CheckForLatestDebugMessages();
            }
            catch (InvalidOperationException)
            {
                //like when closing the form in the middle of a debug session
            }
        }

        private void CheckForLatestDebugMessages()
        {
            LoggingEvent[] latestEvents = Logger.GetLatestDebugMessages();
            

            this.Invoke(new MethodInvoker(
                delegate
                {
                    foreach (LoggingEvent loggingEvent in latestEvents)
                    {
                        if (loggingEvent.MessageObject is int)
                            SetFoundComics((int)loggingEvent.MessageObject);
                        else
                            eventsRichTextBox.AppendText(string.Format("[{0:T}] {1}\n", loggingEvent.TimeStamp,
                                                                   loggingEvent.MessageObject));
                    }
                }
            ));
        }

        private void RunComicTest(ComicDefinition comicDefinition, string startupUrl)
        {
            ThreadPool.UnsafeQueueUserWorkItem(
                delegate
                {
                    CountingFileDownloader countingFileDownloader = new CountingFileDownloader();
                    this.comicsProvider = new ComicsProvider(comicDefinition, countingFileDownloader);
                    this.comicsProvider.DownloadComicCompleted += comicsProvider_DownloadComicCompleted;

                    DownloadOutcome downloadOutcome = this.comicsProvider.DownloadComics(ComicsProvider.AllAvailableComics, startupUrl);

                    string[] comics = countingFileDownloader.ComicLinks;

                    switch (downloadOutcome)
                    {
                        case DownloadOutcome.NoStripMatchesRuleBroken:
                            Logger.Debug("The comic definition doesn't allow pages with missing strips. Download aborted.");
                            break;
                        case DownloadOutcome.MultipleStripMatchesRuleBroken:
                            Logger.Debug("The comic definition doesn't allow pages with multiple strips. Download aborted.");
                            break;
                        case DownloadOutcome.Cancelled:
                            break;
                        case DownloadOutcome.Error:
                            break;
                        case DownloadOutcome.Successful:
                            if (comics[comics.Length - 1] == comicDefinition.FirstIssue)
                                Logger.Debug("The first strip has been reached, so the comic definition works as expected.");
                            else
                                Logger.Debug("The first strip has not been reached, the definition needs some more work.");
                            break;
                        default:
                            throw new InvalidEnumArgumentException();
                    }

                    if (downloadOutcome != DownloadOutcome.Cancelled)
                    {
                        this.currentMode = TestMode.StandBy;
                        DisplayAppropriateControlsForCurrentMode();
                    }
                }
            , null);
        }

        private void SetFoundComics(int foundComics)
        {
            lblFoundStrips.Text = string.Format("I've found <{0:d3}> strips.", foundComics);
        }

        #endregion

        #region Events - comicsProvider
        void comicsProvider_DownloadComicCompleted(object sender, DownloadStripCompletedEventArgs e)
        {
            this.currentUrl = e.CurrentUrl;
        }
        #endregion

        #region Enum - TestMode
        private enum TestMode
        {
            StandBy,
            Running,
            Paused
        }
        #endregion
        
    }
}