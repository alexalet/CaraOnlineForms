using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaraEntites;

namespace Repository
{
    public class ParticipantRepository:RepositoryBase
    {

        public List<Participant> GetParticipants(int filmId)
        {
            return _context.Participants.Where(x => x.FilmSubmissionId == filmId).ToList();
        }

        public bool SaveParticipants(int filmId, List<string> actors, List<string> producers, List<string> directors, List<string> writers, int userid)
        {
            List<Participant> oldParticipants = GetParticipants(filmId);
            foreach (Participant p in oldParticipants)
            {
                _context.Participants.Remove(p);
            }
            _context.SaveChanges();

            // participantTypeId: see table FilmRole in CARA2012
            SaveParticipant(filmId, actors, 1, userid);
            SaveParticipant(filmId, directors, 2, userid);
            SaveParticipant(filmId, producers, 3, userid);
            SaveParticipant(filmId, writers, 4, userid);

            return true;
        }

        private bool SaveParticipant(int filmId, List<string> participants, int participantTypeId, int userId)
        {
            if (participants == null) // just in case...
                return true;

            foreach (string p in participants)
            {
                _context.Participants.Add(new Participant { FilmSubmissionId = filmId, ParticipantTypeID = participantTypeId, ParticipantName=p, ModifiedBy=userId, ModifiedDate=DateTime.Now });
                _context.SaveChanges();
            }

            return true;
        }
    }
}
