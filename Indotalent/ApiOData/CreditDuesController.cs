using Indotalent.Applications.Payment;
using Indotalent.DTOs;
using Indotalent.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class CreditDuesController : ODataController
    {
        private readonly CreditDuesService _creditDuesService;

        public CreditDuesController(CreditDuesService creditDuesService)
        {
            _creditDuesService = creditDuesService;
        }

        [EnableQuery]
        public IQueryable<CreditDuesDto> Get()
        {
            return _creditDuesService
                .GetAll()
                .Select(rec => new CreditDuesDto
                {
                    Id = rec.Id,
                    CreditId = rec.CreditId,
                    FileImage = rec.FileImage,
                    InitialDate = rec.InitialDate,
                    FinalDate = rec.FinalDate,
                    CustomerId = rec.CustomerId,
                    DueValue = rec.DueValue,
                    DueNumber = rec.DueNumber,
                    DueLapse = rec.DueLapse,
                    InitialPaymentPercentage = rec.InitialPaymentPercentage,
                    PaymentStatus = rec.PaymentStatus
                });
        }

        [EnableQuery]
        public SingleResult<CreditDuesDto> Get([FromODataUri] int key)
        {
            var result = _creditDuesService
                .GetAll()
                .Where(n => n.Id == key)
                .Select(rec => new CreditDuesDto
                {
                    Id = rec.Id,
                    CreditId = rec.CreditId,
                    FileImage = rec.FileImage,
                    InitialDate = rec.InitialDate,
                    FinalDate = rec.FinalDate,
                    CustomerId = rec.CustomerId,
                    DueValue = rec.DueValue,
                    DueNumber = rec.DueNumber,
                    DueLapse = rec.DueLapse,
                    InitialPaymentPercentage = rec.InitialPaymentPercentage,
                    PaymentStatus = rec.PaymentStatus
                });

            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Post([FromBody] CreditDuesDto creditDuesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditDues = new CreditDues
            {
                CreditId = creditDuesDto.CreditId,
                FileImage = creditDuesDto.FileImage,
                InitialDate = creditDuesDto.InitialDate,
                FinalDate = creditDuesDto.FinalDate,
                CustomerId = creditDuesDto.CustomerId,
                DueValue = creditDuesDto.DueValue,
                DueNumber = creditDuesDto.DueNumber,
                DueLapse = creditDuesDto.DueLapse,
                InitialPaymentPercentage = creditDuesDto.InitialPaymentPercentage,
                PaymentStatus = creditDuesDto.PaymentStatus
            };

            await _creditDuesService.AddAsync(creditDues);
            return Created(creditDues);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] CreditDuesDto creditDuesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditDues = await _creditDuesService.GetByIdAsync(key);
            if (creditDues == null)
            {
                return NotFound();
            }

            creditDues.CreditId = creditDuesDto.CreditId;
            creditDues.FileImage = creditDuesDto.FileImage;
            creditDues.InitialDate = creditDuesDto.InitialDate;
            creditDues.FinalDate = creditDuesDto.FinalDate;
            creditDues.CustomerId = creditDuesDto.CustomerId;
            creditDues.DueValue = creditDuesDto.DueValue;
            creditDues.DueNumber = creditDuesDto.DueNumber;
            creditDues.DueLapse = creditDuesDto.DueLapse;
            creditDues.InitialPaymentPercentage = creditDuesDto.InitialPaymentPercentage;
            creditDues.PaymentStatus = creditDuesDto.PaymentStatus;

            await _creditDuesService.UpdateAsync(creditDues);
            return Updated(creditDues);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var creditDues = await _creditDuesService.GetByIdAsync(key);
            if (creditDues == null)
            {
                return NotFound();
            }

            await _creditDuesService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
