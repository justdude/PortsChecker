/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 19.10.2014
 */
using System;

namespace Data
{
	/// <summary>
	/// Description of PortInfo.
	/// </summary>
    [Serializable]
	public class PortInfo
    {
        public int PortNumber { get; set; }
        public string Local { get; set; }
        public string Remote { get; set; }
        public string State { get; set; }

        public PortInfo(int i, string local, string remote, string state)
        {
            PortNumber = i;
            Local = local;
            Remote = remote;
            State = state;
        }
    }
}
