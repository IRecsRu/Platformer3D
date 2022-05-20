using System.Collections.Generic;
using Modules.Core.Infrastructure.Services.PersistentProgress;

namespace Modules.Core.Infrastructure.Services.SaveLoad
{
	public interface IStorageProgressWriters
	{
		public IReadOnlyList<ISavedProgress> ProgressWriters { get; }

		public void RegisterWriters(ISavedProgress savedProgress);
	}
}