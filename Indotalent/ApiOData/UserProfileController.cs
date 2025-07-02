using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.ApplicationUsers;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class UserProfileController : ODataController
    {
        private readonly ApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public UserProfileController(ApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<UserProfileDto> Get()
        {
            var result = _applicationUserService
                .GetAll()
                .Include(x => x.SelectedCompany)
                .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider);

            return result;
        }

        public async Task<ActionResult<UserProfileDto>> Get([FromRoute] string key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfile = await _applicationUserService.GetByIdAsync(key,
                    x => x.SelectedCompany)
                .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(userProfile);
        }

        public async Task<ActionResult<UserProfileDto>> Post([FromBody] UserProfileDto userProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfile = _mapper.Map<ApplicationUser>(userProfileDto);
            await _applicationUserService.AddAsync(userProfile);
            return Created();
        }

        public async Task<ActionResult<UserProfileDto>> Put([FromRoute] string key,
            [FromBody] UserProfileDto userProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currenUserProfile = await _applicationUserService.GetByIdAsync(key);
            if (currenUserProfile == null)
            {
                return NotFound();
            }

            _mapper.Map(userProfileDto, currenUserProfile);
            await _applicationUserService.UpdateAsync(currenUserProfile);
            return NoContent();
        }

        public async Task<ActionResult<UserProfileDto>> Patch([FromRoute] string key,
            [FromBody] Delta<UserProfileDto> userProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUserProfile = await _applicationUserService.GetByIdAsync(key);
            if (currentUserProfile == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<UserProfileDto>(currentUserProfile);
            userProfileDto.Patch(dto);

            var entity = _mapper.Map(dto, currentUserProfile);

            await _applicationUserService.UpdateAsync(entity);
            return Updated(_mapper.Map<UserProfileDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] string key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _applicationUserService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
