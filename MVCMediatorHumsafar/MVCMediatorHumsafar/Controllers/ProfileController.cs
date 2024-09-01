using Microsoft.AspNetCore.Mvc;
using MVC_Humsafar_Mubarak.Data;
using MVC_Humsafar_Mubarak.Interface;
using MVC_Humsafar_Mubarak.Models;
using MVC_Humsafar_Mubarak.ViewModel;

namespace MVC_Humsafar_Mubarak.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IProfileRepository _profileRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IProfileRepository profileRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {

            _profileRepository = profileRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

                // Check if the current user is an admin
                if (User.IsInRole(UserRoles.Admin))
                {
                    // Admin logic: Retrieve all profiles and display them
                    var profiles = await _profileRepository.GetAllProfileAsync();
                    return View(profiles);
                }

                // Regular user logic: Retrieve the user's profile
                if (curUserId == null)
                {
                    throw new NullReferenceException("User ID is null.");
                }

                var user = await _profileRepository.GetProfileByUserId(curUserId);
                if (user == null)
                {
                    // If no profile exists, redirect to Create
                    return RedirectToAction("Create", "Dashboard");
                }

                // Show profiles of the opposite gender
                var oppositeGenderProfiles = await _profileRepository.GetProfilesByOppositeGender(user.Gender);
                return View(oppositeGenderProfiles);
            }
            catch (NullReferenceException ex)
            {
                // Handle specific null reference exceptions
                // Log the exception (optional)
                // Redirect to an error page or return an appropriate view/message
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                // Log the exception (optional)
                // Redirect to a generic error page or return an appropriate view/message
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }






        //public async Task<IActionResult> Index()
        //{
        //    var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
        //    var user = await _profileRepository.GetProfileByUserId(curUserId);
        //    if (user == null)
        //    {
        //        return RedirectToAction("Create");
        //    }
        //    IEnumerable<CandidateProfile> profiles;
        //    if (User.IsInRole(UserRoles.Admin))
        //    {
        //        profiles = await _profileRepository.GetAllProfileAsync();
        //    }
        //    else
        //    {
        //        // Show only profiles of the opposite gender
        //        var oppositeGenderProfiles = await _profileRepository.GetProfilesByOppositeGender(user.Gender);
        //        return View(oppositeGenderProfiles);
        //    }

        //    return View(profiles);

        //}



        public async Task<IActionResult> Detail(int id)
        {
            var profile = await _profileRepository.GetProfileByIdAsNoTracking(id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }



        //public async Task<IActionResult> Create()
        //{
        //    var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

        //    // Check if the user already has a profile
        //    var user = await _profileRepository.GetProfileByUserId(curUserId);
        //    if (user != null)
        //    {
        //        return RedirectToAction("Index"); // Redirect to Index if profile exists
        //    }

        //    // Initialize a new CandidateProfile for the view
        //    var createProfileViewModel = new CandidateProfile { AppUserId = curUserId };
        //    return View(createProfileViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CreateProfileViewModel profileVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Invalid form submission.");
        //        return View(profileVM);
        //    }

        //    var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

        //    // Check again if the user already has a profile (in case of concurrency)
        //    var user = await _profileRepository.GetProfileByUserId(curUserId);
        //    if (user != null)
        //    {
        //        return RedirectToAction("Index"); // Redirect if profile exists
        //    }

        //    try
        //    {
        //        // Photo upload logic
        //        if (profileVM.Photo == null || profileVM.Photo.Length == 0)
        //        {
        //            ModelState.AddModelError("", "Please select a photo to upload.");
        //            return View(profileVM);
        //        }

        //        var result = await _photoService.AddPhotoAsync(profileVM.Photo);

        //        if (result == null || string.IsNullOrEmpty(result.Uri?.ToString()))
        //        {
        //            ModelState.AddModelError("", "Photo upload failed.");
        //            return View(profileVM);
        //        }

        //        // Create a new CandidateProfile
        //        var profile = new CandidateProfile
        //        {
        //            Name = profileVM.Name,
        //            Age = profileVM.Age,
        //            MaritalStatus = profileVM.MaritalStatus,
        //            Gender = profileVM.Gender,
        //            Description = profileVM.Description,
        //            AppUserId = curUserId,
        //            Photo = result.Uri.ToString(),  // Corrected 'result.U' to 'result.Uri'
        //            Url = profileVM.Url,
        //            Address = new Address
        //            {
        //                Country = profileVM.Address?.Country,
        //                City = profileVM.Address?.City,
        //                State = profileVM.Address?.State,
        //                ZipPostalCode = profileVM.Address?.ZipPostalCode,
        //            }
        //        };

        //        // Add the new profile to the database
        //        await _profileRepository.AddAsync(profile);

        //        // Redirect to the Profile Index after successful creation
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", $"An error occurred: {ex.Message}");
        //        return View(profileVM);
        //    }
        //}





        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<CandidateProfile> profiles = await _profileRepository.GetAllAsync();
        //    return View(profiles);
        //}

        //public IActionResult Create()
        //{
        //    var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
        //    var createClubViewModel = new CandidateProfile { AppUserId = curUserId };
        //    return View(createClubViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CreateProfileViewModel profileVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Invalid form submission.");
        //        return View(profileVM);
        //    }

        //    try
        //    {

        //        if (profileVM.Photo == null || profileVM.Photo.Length == 0)
        //        {
        //            ModelState.AddModelError("", "Please select a photo to upload.");
        //            return View(profileVM);
        //        }

        //        var result = await _photoService.AddPhotoAsync(profileVM.Photo);

        //        if (result == null || string.IsNullOrEmpty(result.Uri?.ToString()))
        //        {
        //            ModelState.AddModelError("", "Photo upload failed.");
        //            return View(profileVM);
        //        }
        //        var profile = new CandidateProfile
        //        {
        //            Name = profileVM.Name,
        //            Age = profileVM.Age,
        //            MaritalStatus = profileVM.MaritalStatus,
        //            Gender = profileVM.Gender,
        //            Description = profileVM.Description,
        //            AppUserId = profileVM.AppUserId,
        //            Photo = result.Uri.ToString(),
        //            Url = profileVM.Url,
        //            Address = new Address
        //            {
        //                Country = profileVM.Address?.Country,
        //                City = profileVM.Address?.City,
        //                State = profileVM.Address?.State,
        //                ZipPostalCode = profileVM.Address?.ZipPostalCode,
        //            }
        //        };

        //        if (profile.Address == null || string.IsNullOrWhiteSpace(profile.Address.Country))
        //        {
        //            ModelState.AddModelError("", "Address is incomplete.");
        //            return View(profileVM);
        //        }

        //        var addResult = await _profileRepository.AddAsync(profile);

        //        if (!addResult)
        //        {
        //            ModelState.AddModelError("", "An error occurred while saving the profile.");
        //            return View(profileVM);
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", $"An error occurred: {ex.Message}");
        //        return View(profileVM);
        //    }
        //}


        //public async Task<IActionResult> Detail(int id)
        //{
        //    var profile = await _profileRepository.GetByIdAsync(id);
        //    return View(profile);
        //}


        //public async Task<IActionResult> Edit(int id)
        //{
        //    var profile = await _profileRepository.GetByIdAsync(id);
        //    if (profile == null) return View("Error");

        //    var profileVM = new EditPrflleViewModel
        //    {
        //        Name = profile.Name,
        //        Age = profile.Age,
        //        Gender = profile.Gender,
        //        MaritalStatus = profile.MaritalStatus,
        //        Description = profile.Description,
        //        Url = profile.Photo,
        //        AddressId = profile.AddressId,
        //        Address = profile.Address
        //    };
        //    return View(profileVM);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, EditPrflleViewModel profileVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Failed to edit profile");
        //        return View("Edit", profileVM);
        //    }
        //    var user = await _profileRepository.GetByIdAsyncNoTracking(id);

        //    if (user != null)
        //    {
        //        try
        //        {
        //            await _photoService.DeletePhotoAsync(user.Photo);
        //        }
        //        catch (Exception)
        //        {
        //            ModelState.AddModelError("", "Failed to delete the photo");
        //            return View(profileVM);
        //        }

        //        var photoResult = await _photoService.AddPhotoAsync(profileVM.Photo);
        //        var profile = new CandidateProfile
        //        {
        //            Id = id,
        //            Name = profileVM.Name,
        //            Age = profileVM.Age,
        //            Gender = profileVM.Gender,
        //            MaritalStatus = profileVM.MaritalStatus,
        //            Description = profileVM.Description,
        //            AddressId = profileVM.AddressId,
        //            Address = profileVM.Address,
        //            Photo = photoResult.Url.ToString(),
        //            Url = profileVM.Url

        //        };
        //        _profileRepository.UpdateAsync(profile);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(profileVM);
        //    }

        //}


        //public async Task<IActionResult> Delete(int id)
        //{
        //    var profile = await _profileRepository.GetByIdAsync(id);
        //    if (profile == null) return View("Error");

        //    return View(profile);
        //}



        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteProfile(int id)
        //{
        //    var profile = await _profileRepository.GetByIdAsync(id);
        //    if (profile == null) return View("Error");

        //    _profileRepository.DeleteAsync(profile);
        //    return RedirectToAction("Index");
        //}
    }
}
