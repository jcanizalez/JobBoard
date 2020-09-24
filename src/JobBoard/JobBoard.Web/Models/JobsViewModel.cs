using JobBoard.Model;
using System.Collections.Generic;
namespace JobBoard.Web.Models
{
    public class JobsViewModel
    {
        public Job Job { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
