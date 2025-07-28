using Indotalent.Applications.Payment;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class PaymentController : ODataController
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [EnableQuery]
        public IQueryable<PaymentDto> Get()
        {
            return _paymentService
                .GetAll()
                .Select(rec => new PaymentDto { Id = rec.RowGuid, PaymentName = rec.PaymentName });
        }

        [EnableQuery]
        public SingleResult<PaymentDto> Get([FromODataUri] Guid key)
        {
            var result = _paymentService
                .GetAll()
                .Where(n => n.RowGuid == key)
                .Select(rec => new PaymentDto { Id = rec.RowGuid, PaymentName = rec.PaymentName });

            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Post([FromBody] PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = new Payment(paymentDto.PaymentName!, paymentDto.Id);

            await _paymentService.AddAsync(payment);
            return Created(payment);
        }

        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = await _paymentService.GetByIdAsync(key);
            if (payment == null)
            {
                return NotFound();
            }

            payment.PaymentName = paymentDto.PaymentName!;

            await _paymentService.UpdateAsync(payment);
            return Updated(payment);
        }

        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var payment = await _paymentService.GetByIdAsync(key);
            if (payment == null)
            {
                return NotFound();
            }

            await _paymentService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
