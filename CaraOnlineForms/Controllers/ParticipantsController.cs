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

        string _message = "Partisipants have been saved";
        public ActionResult Participants()
        {
            List<Participant> model = new ParticipantRepository().GetParticipants(this.CurrentFilm.FilmSubmissionId);
            ViewBag.Message = _message;
            return View(model);
        }

        public ActionResult SaveParticipants(List<string> Actor, List<string> Director, List<string> Producer, List<string> Writer, string NextPage)
        {
            bool res = new ParticipantRepository().SaveParticipants(this.CurrentFilm.FilmSubmissionId, Actor, Director, Producer, Writer, this.User.UserId);
            if (!String.IsNullOrEmpty(NextPage))
            {
                return Redirect(NextPage);
            }
            return RedirectToAction("Participants",new {message="Partisipants have been saved"});
         
        }
    }
}
