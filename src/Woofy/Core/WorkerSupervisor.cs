using System.Collections.Generic;
using System.Threading;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using MoreLinq;

namespace Woofy.Core
{
	public interface IWorkerSupervisor
	{
	}

	public class WorkerSupervisor : IWorkerSupervisor, ICommandHandler<StartAllDownloads>
	{
	    private readonly IList<Comic> comics;

	    public WorkerSupervisor(IComicStore comicStore)
		{
			comics = comicStore.GetActiveComics();
		}

        #region OBSOLETE

	    private void DownloadComicCompletedCallback(object sender, DownloadStripCompletedEventArgs e)
        {
            //synchronizationContext.Post(o =>
            //                                {
            //                                    var comic = ((Bot)sender).Comic;
            //                                    if (!bots.ContainsKey(comic)) //in case the comic has already been removed.
            //                                        return;

            //                                    comic.DownloadedComics++;
            //                                    comic.CurrentPage = e.CurrentPage;
            //                                    comicRepository.PersistComics();

            //                                    ResetComicsBindings();
            //                                }, null);
        }

        private void DownloadComicsCompletedCallback(object sender, DownloadCompletedEventArgs e)
        {
            //synchronizationContext.Post(o =>
            //                                {
            //                                    var comic = ((Bot)sender).Comic;
            //                                    if (!bots.ContainsKey(comic)) //in case the comic has already been removed.
            //                                        return;

            //                                    comic.Status = e.DownloadOutcome == DownloadOutcome.Cancelled
            //                                                    ? WorkerStatus.Stopped
            //                                                    : WorkerStatus.Finished;

            //                                    //only set the currentUrl to null if the outcome is successful
            //                                    if (e.DownloadOutcome == DownloadOutcome.Successful)
            //                                        comic.CurrentPage = null;

            //                                    comic.DownloadOutcome = e.DownloadOutcome;
            //                                    comicRepository.PersistComics();

            //                                    ResetComicsBindings();

            //                                    if (!UserSettingsOld.CloseWhenAllComicsHaveFinished)
            //                                        return;

            //                                    var allTasksHaveFinished = true;
            //                                    foreach (var _task in Comics)
            //                                    {
            //                                        if (_task.Status != WorkerStatus.Running)
            //                                            continue;

            //                                        allTasksHaveFinished = false;
            //                                        break;
            //                                    }

            //                                    if (allTasksHaveFinished)
            //                                        Application.Exit();
            //                                }, null);
        } 
        #endregion

	    public void Handle(StartAllDownloads command)
	    {
            comics.ForEach(c => ThreadPool.QueueUserWorkItem(o => c.Definition.Run()));
	    }
	}

    public class StartAllDownloads : ICommand
    {
    }
}