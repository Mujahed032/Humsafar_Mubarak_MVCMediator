using Application.Candidates.Queries.ListOfCandidates.GetAllCandidatesList;
using Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MVC_Humsafar_Mubarak.Interface;

namespace MVC_Humsafar_Mubarak.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public HomeController(ICandidateRepository candidateRepository, IHttpContextAccessor httpContextAccessor)
        {
            _candidateRepository = candidateRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var candidatesX = await _mediator.Send(new GetAllCandidatesListMediatR.Query());           
            return View(candidatesX);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var profile = await _candidateRepository.GetProfileByIdAsNoTracking(id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }
    }
}