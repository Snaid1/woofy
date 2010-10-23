namespace Woofy.Core.Engine
{
#warning should be renamed to Worker, for clarity purposes
	public abstract class Definition
	{
        public abstract string Comic { get; }
        public abstract string StartAt { get; }

		protected abstract void RunImpl(Context context);

		/// <summary>
		/// Basically the definition's filename without the extension.
		/// </summary>
		public string Id { get; set; }

		protected Definition()
		{
			Id = GetType().Name.Substring(1);
		}

		public void Run()
		{
			var context = new Context();
			RunImpl(context);
		}
	}
}