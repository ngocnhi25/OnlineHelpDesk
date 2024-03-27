using System;
namespace SharedKernel
{
	public class RequestCountRespone
	{
        public int All { get; set; }
        public int Open { get; set; }
        public int Assigned { get; set; }
        public int WorkInProgress { get; set; }
        public int Rejected { get; set; }
        public int Complete { get; set; }
        public int NeedMoreInfo { get; set; }

        public int Pending { get; set; }
    }
}

