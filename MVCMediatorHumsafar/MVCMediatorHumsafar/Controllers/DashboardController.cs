using Microsoft.AspNetCore.Mvc;
using MVC_Humsafar_Mubarak.Interface;
using MVC_Humsafar_Mubarak.ViewModel;

namespace MVC_Humsafar_Mubarak.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IDashboardRepository _dashboardRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashBoardController(IDashboardRepository dashboardRepository, IProfileRepository profileRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {

            _dashboardRepository = dashboardRepository;
            _profileRepository = profileRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> Create()
        {//Candidate
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userProfile = await _dashboardRepository.GetProfileByUserId(curUserId);

            if (userProfile != null)
            {
                return RedirectToAction("Index}", "Profile"); // Redirect to profile if already created
            }

            //new ContactDTO(candidateId = Candidate.Id)

            var createProfileViewModel = new CreateProfileViewModel { AppUserId = curUserId };
            return View(createProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProfileViewModel profileVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid form submission.");
                return View(profileVM);
            }

            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _profileRepository.GetProfileByUserId(curUserId);
            if (user != null)
            {
                return RedirectToAction("Index");
            }

            // Photo upload logic
            var result = await _photoService.AddPhotoAsync(profileVM.Photo);

            if (result == null || string.IsNullOrEmpty(result.Uri?.ToString()))
            {
                ModelState.AddModelError("", "Photo upload failed.");
                return View(profileVM);
            }

            var profile = new CandidateProfile
            {
                Name = profileVM.Name,
                Age = profileVM.Age,
                MaritalStatus = profileVM.MaritalStatus,
                Gender = profileVM.Gender,
                Description = profileVM.Description,
                AppUserId = curUserId,
                Photo = result.Uri.ToString(),
                Url = profileVM.Url,
                Address = new Address
                {
                    Country = profileVM.Address?.Country,
                    City = profileVM.Address?.City,
                    State = profileVM.Address?.State,
                    ZipPostalCode = profileVM.Address?.ZipPostalCode,
                }
            };

            await _profileRepository.AddAsync(profile);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // or handle accordingly
            }

            var profile = await _profileRepository.GetProfileByUserId(userId);
            if (profile == null)
            {
                return RedirectToAction("Create", "Dashboard"); // Redirect to create profile if not existing
            }
            var viewModel = new DashboardViewModel
            {
                CandidateProfile = profile
            };
            return View(viewModel); // Pass the profile to the view
        }

        public async Task<IActionResult> Detail(int id)
        {
            var profile = await _profileRepository.GetProfileByIdAsNoTracking(id);
            {
                if (profile == null)
                {
                    return NotFound();
                }
                var viewModel = new DashboardViewModel
                {
                    CandidateProfile = profile
                };
                return View(viewModel);
            }

        }


        public async Task<IActionResult> Edit(int id)
        {
            var profile = await _profileRepository.GetProfileByIdAsNoTracking(id);
            if (profile == null)
            {
                return NotFound();
            }

            var profileVM = new EditPrflleViewModel
            {
                Id = profile.Id,
                Name = profile.Name,
                Age = profile.Age,
                Gender = profile.Gender,
                MaritalStatus = profile.MaritalStatus,
                Description = profile.Description,
                AddressId = profile.AddressId,
                Address = profile.Address,
                Url = profile.Photo
            };

            return View(profileVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPrflleViewModel profileVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View(profileVM);
            }

            var existingProfile = await _profileRepository.GetProfileByIdAsNoTracking(id);
            if (existingProfile != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(existingProfile.Photo))
                    {
                        await _photoService.DeletePhotoAsync(existingProfile.Photo);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Failed to delete the photo: {ex.Message}");
                    return View(profileVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(profileVM.Photo);
                var profile = new CandidateProfile
                {
                    Id = id,
                    Name = profileVM.Name,
                    Age = profileVM.Age,
                    Gender = profileVM.Gender,
                    MaritalStatus = profileVM.MaritalStatus,
                    Description = profileVM.Description,
                    AddressId = profileVM.AddressId,
                    Address = profileVM.Address,
                    Photo = photoResult.Url.ToString(),
                    AppUserId = existingProfile.AppUserId
                };

                await _profileRepository.UpdateProfileAsync(profile);
                return RedirectToAction("Index");
            }
            else
            {
                return View(profileVM);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _profileRepository.GetProfileByIdAsNoTracking(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);


        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profile = await _profileRepository.GetProfileByIdAsNoTracking(id);
            if (profile == null)
            {
                return NotFound();
            }
            await _profileRepository.DeleteAsync(profile);
            return RedirectToAction("Index", "Dashboard");
        }

    }
}
