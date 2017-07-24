using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitlesDatabase.Models;

namespace TitlesDatabase.Repos.Compositions
{
    public class TitleCompositeRepo
    {
        public TitleComposite getById(int id)
        {
            var tRepo = new TitleRepo();
            var titleQuery = tRepo.getById(id).FirstOrDefault();
            var tc = new TitleComposite();
            Parallel.Invoke(() => tc.Awards = tRepo.getById(id).SelectMany(t => t.Awards).Select(a => a).ToList(),
                () => tc.StoryLines = tRepo.getById(id).SelectMany(t => t.StoryLines).ToList(),
                () =>
                {
                    List<TitleParticipantInfoModel> tpims = new List<TitleParticipantInfoModel>();
                    List<Participant> ps = new List<Participant>();
                    List<TitleParticipant> tps = new List<TitleParticipant>();
                    Parallel.Invoke(
                        () => ps = tRepo.getById(id).SelectMany(t => t.TitleParticipants).Select(p => p.Participant).ToList(),
                        () => tps = tRepo.getById(id).SelectMany(t => t.TitleParticipants).Select(tp => tp).ToList()
                    );
                    var tpsDict = tps.GroupBy(tp => tp.ParticipantId).ToDictionary(tpg => tpg.Key, tpg => tpg.FirstOrDefault());
                    foreach(var p in ps)
                    {
                        tpims.Add(new TitleParticipantInfoModel() { titleParticipant = tpsDict[p.Id], participant = p });
                    }
                    tc.TitleParticipantInfo = tpims;
                },
                () => tc.Genres = tRepo.getById(id).SelectMany(t => t.TitleGenres).Select(tg => tg.Genre).ToList(),
                () => tc.OtherNames = tRepo.getById(id).SelectMany(t => t.OtherNames).ToList(),
                () => tc.Title = tRepo.getById(id).FirstOrDefault()
            );
            return tc;
        }
    }
}
