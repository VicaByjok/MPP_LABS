using System.Collections.Generic;

namespace TestTracer
{
	abstract class ISeril
	{
		protected List<Tracer.Tracer.TracerLog> TracerLog;

		public abstract string Save();

	}
}
