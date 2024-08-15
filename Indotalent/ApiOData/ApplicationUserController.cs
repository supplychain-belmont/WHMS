using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.ApplicationUsers;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class ApplicationUserController : ODataController
    {
        private readonly ApplicationUserService _applicationUserService;
        private readonly IMapper _mapper;

        public ApplicationUserController(ApplicationUserService applicationUserService, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<ApplicationUserDto> Get()
        {
            return _applicationUserService
                .GetAll()
                .Include(x => x.SelectedCompany)
                .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<ApplicationUserDto>> Get([FromRoute] string key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _applicationUserService.GetByIdAsync(key,
                    x => x.SelectedCompany)
                .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(applicationUser);
        }

        public async Task<ActionResult<ApplicationUserDto>> Post([FromBody] ApplicationUserDto userProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = _mapper.Map<ApplicationUser>(userProfileDto);
            await _applicationUserService.AddAsync(applicationUser);
            return Created();
        }

        public async Task<ActionResult<ApplicationUserDto>> Put([FromRoute] string key,
            [FromBody] ApplicationUserDto applicationUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentApplicationUser = await _applicationUserService.GetByIdAsync(key);
            if (currentApplicationUser == null)
            {
                return NotFound();
            }

            _mapper.Map(applicationUserDto, currentApplicationUser);
            await _applicationUserService.UpdateAsync(currentApplicationUser);
            return NoContent();
        }

        public async Task<ActionResult<ApplicationUserDto>> Patch([FromRoute] string key,
            [FromBody] Delta<ApplicationUserDto> applicationUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentApplicationUser = await _applicationUserService.GetByIdAsync(key);
            if (currentApplicationUser == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ApplicationUserDto>(currentApplicationUser);
            applicationUserDto.Patch(dto);

            var entity = _mapper.Map(dto, currentApplicationUser);

            await _applicationUserService.UpdateAsync(entity);
            return Updated(_mapper.Map<ApplicationUserDto>(entity));
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
