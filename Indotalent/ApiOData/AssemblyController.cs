using AutoMapper;

using Indotalent.Applications.Products;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace Indotalent.ApiOData
{
    public class AssemblyController : BaseODataController<Assembly, AssemblyDto>
    {
        private readonly AssemblyService _assemblyService;

        public AssemblyController(AssemblyService service, IMapper mapper) : base(service, mapper)
        {
            _assemblyService = service;
        }

        [HttpPost]
        public async Task<IActionResult> BuildAssembly(ODataActionParameters actionParameters)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (actionParameters["assemblyId"] is not int assemblyId)
                {
                    return BadRequest("AssemblyId is required.");
                }

                if (actionParameters["warehouseId"] is not int warehouseId)
                {
                    return BadRequest("WarehouseId is required.");
                }

                if (actionParameters["quantity"] is not int quantity)
                {
                    quantity = 1;
                }

                await _assemblyService.CreateAssemblyAsync(assemblyId, warehouseId, quantity);

                return Ok();
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
