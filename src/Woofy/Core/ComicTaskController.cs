using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Woofy.Settings;

namespace Woofy.Core
{
    public class ComicTasksController
    {
        #region Instance Members
        private readonly List<ComicsProvider> comicProviders = new List<ComicsProvider>();
        private readonly DataGridView tasksGrid;

        #endregion

        #region Public Properties

    	public BindingList<ComicTask> Tasks { get; private set; }

    	#endregion

        #region .ctor
        public ComicTasksController(DataGridView tasksGrid)
        {
            this.tasksGrid = tasksGrid;
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            Tasks = new BindingList<ComicTask>(ComicTask.RetrieveAllTasks());

            foreach (ComicTask task in Tasks)
            {
                AddComicsProviderAndStartDownload(task);
            }
        }

        /// <summary>
        /// Adds a new task to the tasks list and database. Also starts its download.
        /// </summary>
        /// <returns>True if the task has been added successfully, false otherwise.</returns>
        public bool AddNewTask(ComicTask task)
        {
            if (ComicTask.RetrieveActiveTasksByComicInfoFile(task.ComicInfoFile).Count > 0)
                return false;

            task.Create();
            Tasks.Add(task);
            AddComicsProviderAndStartDownload(task);

            return true;
        }

        /// <summary>
        /// Stops the specified task's download and deletes it from the database.
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(ComicTask task)
        {
            int index = Tasks.IndexOf(task);
            ComicsProvider comicsProvider = comicProviders[index];
            if (task.Status == TaskStatus.Running)
                comicsProvider.StopDownload();

            task.Delete();

            Tasks.RemoveAt(index);
            comicProviders.RemoveAt(index);
        }

        /// <summary>
        /// Pauses/unpauses a task, depending on its current state.
        /// </summary>
        public void ToggleTaskState(ComicTask task, bool resetTasksBindings)
        {
            switch (task.Status)
            {
                case TaskStatus.Stopped:
                    StartTask(task);
                    break;
                case TaskStatus.Running:
                    StopTask(task);
                    break;
            }

            if (resetTasksBindings)
                ResetTasksBindings();
        }

        /// <summary>
        /// Stops the specified comic task.
        /// </summary>
        /// <param name="task">Comic task to stop.</param>
        public void StopTask(ComicTask task)
        {
            if (task.Status != TaskStatus.Running)
                return;

            int index = Tasks.IndexOf(task);
            ComicsProvider comicsProvider = comicProviders[index];

            task.Status = TaskStatus.Stopped;
            comicsProvider.StopDownload();

            task.Update();
        }

        /// <summary>
        /// Start the specified comic task.
        /// </summary>
        /// <param name="task">Comic task to start.</param>
        public void StartTask(ComicTask task)
        {
            if (task.Status != TaskStatus.Stopped)
                return;

            int index = Tasks.IndexOf(task);
            ComicsProvider comicsProvider = comicProviders[index];

            task.Status = TaskStatus.Running;
            comicsProvider.DownloadComicsAsync(task.CurrentUrl);

            task.Update();
        }

        /// <summary>
        /// Opens the folder associated with the specified task, using Windows Explorer.
        /// </summary>
        public void OpenTaskFolder(ComicTask task)
        {
            string downloadFolder = (Path.IsPathRooted(task.DownloadFolder) ? task.DownloadFolder : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, task.DownloadFolder));
            if (Directory.Exists(downloadFolder))
                Process.Start(downloadFolder);
        }

        public void ResetTasksBindings()
        {
            Tasks.ResetBindings();
        }
        #endregion

        #region Helper Methods
        private void AddComicsProviderAndStartDownload(ComicTask task)
        {
            var comicInfo = new ComicDefinition(task.ComicInfoFile);

            var comicsProvider = new ComicsProvider(comicInfo, task.DownloadFolder, task.RandomPausesBetweenRequests);
            comicProviders.Add(comicsProvider);

            comicsProvider.DownloadComicCompleted += DownloadComicCompletedCallback;
            comicsProvider.DownloadCompleted += DownloadComicsCompletedCallback;

            if (task.Status == TaskStatus.Finished)
            {
                task.Status = TaskStatus.Running;
                task.Update();
            }

            if (task.Status != TaskStatus.Running) 
                return;

            if (string.IsNullOrEmpty(task.CurrentUrl))
                comicsProvider.DownloadComicsAsync();
            else
                comicsProvider.DownloadComicsAsync(task.CurrentUrl);
        }        
        #endregion

        #region Callback Methods
        private void DownloadComicCompletedCallback(object sender, DownloadStripCompletedEventArgs e)
        {
            tasksGrid.Invoke(new MethodInvoker(
                delegate
                    {
                    var provider = (ComicsProvider)sender;

                    int index = comicProviders.IndexOf(provider);
                    if (index == -1)    //in case the task has already been deleted.
                        return;

                    ComicTask task = Tasks[index];
                    task.DownloadedComics++;
                    task.CurrentUrl = e.CurrentUrl;
                    task.Update();

                    ResetTasksBindings();
                }
            ));
        }

        private void DownloadComicsCompletedCallback(object sender, DownloadCompletedEventArgs e)
        {
            tasksGrid.Invoke(new MethodInvoker(
                delegate
                {
                    var comicsProvider = (ComicsProvider)sender;

                    var index = comicProviders.IndexOf(comicsProvider);
                    if (index == -1)    //in case the task has already been deleted.
                        return;

                    var task = Tasks[index];
                    task.Status = e.DownloadOutcome == DownloadOutcome.Cancelled ? TaskStatus.Stopped : TaskStatus.Finished;

                    //only set the currentUrl to null if the outcome is successful
                    if (e.DownloadOutcome == DownloadOutcome.Successful)
                        task.CurrentUrl = null;

                    task.DownloadOutcome = e.DownloadOutcome;
                    task.Update();

                    ResetTasksBindings();

                    if (!UserSettings.CloseWhenAllComicsHaveFinished)
                        return;

                    var allTasksHaveFinished = true;
                    foreach (var _task in Tasks)
                    {
                        if (_task.Status != TaskStatus.Running) 
                            continue;

                        allTasksHaveFinished = false;
                        break;
                    }

                    if (allTasksHaveFinished)
                        Application.Exit();
                }
            ));
        }
        #endregion
    }
}