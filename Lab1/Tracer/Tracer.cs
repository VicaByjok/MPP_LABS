using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Tracer
{
	public static class Tracer
	{
		public struct CurrentTraceInfo
		{
			public int ThreadId;
			public DateTime TraceTime;
			public double DeltaTime;
			public string MethodName;
			public string ClassName;
			public string FileName;
			public bool IsOpened;
		}

		public struct TracerLog
		{
			public CurrentTraceInfo Info;
			public List<TracerLog> TraceChilds;
		}

		private struct TimeLaps
		{
			public int BeginCount;
			public int EndCount;
		}

		static List<CurrentTraceInfo> traceStack;
		static Dictionary<int, List<CurrentTraceInfo>> traceStackDict;
		static Dictionary<int, TimeLaps> traceStackCount;
		static List<TracerLog> traceLog;

		static readonly object Locker = new object();
		static bool isStarted;

		

		public static void Start()
		{
			lock (Locker)
			{
				if (!isStarted)
				{
					isStarted = true;
					traceStack = new List<CurrentTraceInfo>();
					traceLog = new List<TracerLog>();
				}
				else
					throw new Exception("Вызов старта до окончания работы предудыщего");
			}
		}

		public static List<TracerLog> Stop()
		{

			lock (Locker)
			{
				traceStackDict = new Dictionary<int, List<CurrentTraceInfo>>();
				traceStackCount = new Dictionary<int, TimeLaps>();

				foreach (CurrentTraceInfo cti in traceStack)
				{
					List<CurrentTraceInfo> obj;
					if (!traceStackDict.TryGetValue(cti.ThreadId, out obj))
					{
						traceStackDict.Add(cti.ThreadId, new List<CurrentTraceInfo>());
						traceStackCount.Add(cti.ThreadId, new TimeLaps());
					}
				}

				foreach (CurrentTraceInfo cti in traceStack)
				{
					traceStackDict[cti.ThreadId].Add(cti);
					if (cti.IsOpened)
					{
						TimeLaps tempTl = new TimeLaps
							{
								BeginCount = traceStackCount[cti.ThreadId].BeginCount + 1,
								EndCount = traceStackCount[cti.ThreadId].EndCount
							};
						traceStackCount[cti.ThreadId] = tempTl;
					}
					else
					{
						TimeLaps tempTl = new TimeLaps
							{
								BeginCount = traceStackCount[cti.ThreadId].BeginCount,
								EndCount = traceStackCount[cti.ThreadId].EndCount + 1
							};
						traceStackCount[cti.ThreadId] = tempTl;
					}
				}

				foreach (KeyValuePair<int, List<CurrentTraceInfo>> cti in traceStackDict)
				{
					if (traceStackCount[cti.Key].BeginCount != traceStackCount[cti.Key].EndCount)
					{
						for (var i = 0; i < Math.Abs(traceStackCount[cti.Key].EndCount - traceStackCount[cti.Key].BeginCount); i++)
						{
							var temp = new CurrentTraceInfo {IsOpened = false, ThreadId = Thread.CurrentThread.ManagedThreadId};
							traceStackDict[cti.Key].Add(temp);
						}
					}
				}

				//построения дерева
				traceLog = new List<TracerLog>();
				foreach (KeyValuePair<int, List<CurrentTraceInfo>> kvp in traceStackDict)
				{
					var j = 0;
					var x = new TracerLog {TraceChilds = new List<TracerLog>()};
					AddTreeElement(ref x.TraceChilds, kvp.Value, ref j);
					traceLog.Add(x);
				}
				isStarted = false;
			}
			return traceLog;
		}

		private static void AddTreeElement(ref List<TracerLog> tempLog, List<CurrentTraceInfo> traceInfo, ref int i) {
			int j;
			while (i < traceInfo.Count)
			{
				if (traceInfo[i].IsOpened)
				{
					j = i;
					TracerLog x = new TracerLog();
					x.Info = traceInfo[i];
					x.TraceChilds = new List<TracerLog>();
					i++;
					AddTreeElement(ref x.TraceChilds, traceInfo, ref i);
					x.Info.DeltaTime = (traceInfo[i-1].TraceTime - traceInfo[j].TraceTime).TotalMilliseconds;
					tempLog.Add(x);
				}
				else
				{
					i++;
					return;
				}
			}
		}

		public static void BeginTrace()
		{
			lock (Locker)
			{
				CurrentTraceInfo traceInfo = GetMethodDescription();
				traceInfo.IsOpened = true;
				traceStack.Add(traceInfo);
			}
		}

		public static void EndTrace()
		{
			lock (Locker)
			{
				CurrentTraceInfo traceInfo = GetMethodDescription();
				traceInfo.IsOpened = false;
				traceStack.Add(traceInfo);
			}
		}

		private static CurrentTraceInfo GetMethodDescription()
		{
			CurrentTraceInfo tempInfo = new CurrentTraceInfo();
			tempInfo.TraceTime = DateTime.UtcNow;
            int threadid= Thread.CurrentThread.ManagedThreadId;
                                                                                                                                                                                                                                                              if(threadid > 1)  threadid--; 
            tempInfo.ThreadId = threadid;

			try
			{
				StackTrace st = new StackTrace(true);
				StackFrame frame;

				for (int i = 0; i < st.FrameCount; i++)
				{
					frame = st.GetFrame(i);
					var declaringType = frame.GetMethod().DeclaringType;
					if (declaringType != null)
					{
						string name = declaringType.Name;
						if (name != typeof(Tracer).Name)
						{
							tempInfo.FileName = frame.GetMethod().Module.Name;
							tempInfo.MethodName = frame.GetMethod().Name;
							tempInfo.ClassName = declaringType.FullName;
							return tempInfo;
						}
					}
				}
				return tempInfo;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return tempInfo;
			}
		}
	}
}
