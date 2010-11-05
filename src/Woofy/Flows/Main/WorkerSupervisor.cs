using System.Collections.Generic;
using System.Threading;
using Woofy.Core.ComicManagement;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using MoreLinq;
using System.Linq;

namespace Woofy.Flows.Main
{
    public class StartAllDownloads : ICommand
    {
    }

    public class WorkerSupervisor : ICommandHandler<StartAllDownloads>, ICommandHandler<StartDownload>, ICommandHandler<PauseDownload>
    {
        private readonly IList<Comic> comics;

        public WorkerSupervisor(IComicStore comicStore)
        {
            comics = comicStore.GetActiveComics();
        }

        public void Handle(StartAllDownloads command)
        {
            comics
                .Where(c => c.Status != WorkerStatus.Paused)
                .ForEach(c => ThreadPool.QueueUserWorkItem(o => c.Definition.Run()));
        }

        public void Handle(StartDownload command)
        {
            ThreadPool.QueueUserWorkItem(o => command.Comic.Definition.Run());
        }

        public void Handle(PauseDownload command)
        {
			command.Comic.Definition.Stop();
        }
    }
}