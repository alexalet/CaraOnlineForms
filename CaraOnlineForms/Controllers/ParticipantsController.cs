using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;

namespace CaraOnlineForms.Controllers
{
    public class ParticipantsController : ControllerBase
    {


        public ActionResult Participants()
        {
            List<Participant> model = new ParticipantRepository().GetParticipants(this.CurrentFilm.FilmSubmissionId);
            return View(model);
        }

    }
}
