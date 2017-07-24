using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitlesDatabase.Models
{
    public class TitleParticipantInfoModel
    {
        public Participant participant { get; set; }
        public TitleParticipant titleParticipant { get; set; }
    }
}
