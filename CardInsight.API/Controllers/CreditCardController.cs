using CardInsight.API.DTOs;
using CardInsight.API.Models;
using CardInsight.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardInsight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCard creditCardRepository;

        public CreditCardController(ICreditCard creditCardRepository)
        {
            this.creditCardRepository = creditCardRepository;
        }
        [HttpPost("addCC")]
        public async Task<IActionResult> AddCreditCard(CreditCardDTO cardDTO)
        {
            var cc = new CreditCard
            {
                Name = cardDTO.Name,
                ApplyLink = cardDTO.ApplyLink,
                Category = cardDTO.Category,
                //CreatedAt = cardDTO.CreatedAt,
                Features = cardDTO.Features,
                ImageUrl = cardDTO.ImageUrl
            };
            try
            {
                await creditCardRepository.AddCreditCardAsync(cc);
                return Ok("Successfully Added!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCreditCard()
        {
            try
            {
                var CCs = await creditCardRepository.GetAllCreditCards();
                return Ok(CCs.ToList());    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getbySearchorCategory")]
        public async Task<IActionResult> GetCreditCard(string? search , string? category)
        {
            try
            {
                var cc = await creditCardRepository.GetCreditCard(search, category);
                return Ok(cc);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateCC/{id}")]
        public async Task<IActionResult> UpdateCreditCard(int id, CreditCardDTO creditCardDto)
        {
            var cc = await creditCardRepository.GetCreditCardByID(id);
            if (cc is null)
                return NotFound("No CreditCard found with given Id.");
            cc.Name = creditCardDto.Name;
            cc.Features = creditCardDto.Features;
            cc.ApplyLink = creditCardDto.ApplyLink;
            cc.Category = creditCardDto.Category;  
            cc.ImageUrl = creditCardDto.ImageUrl;
            try
            {
                await creditCardRepository.UpdateCreditCardAsync(cc);
                return Ok("Successfully Updated!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("deleteCC/{id}")]
        public async Task<IActionResult> DeleteCreditCard(int id)
        {
            try
            {
                await creditCardRepository.DeleteCreditCardAsync(id);
                return Ok("Successfully Deleted!");
            }
            catch(Exception ex){
            return BadRequest(ex.Message);
            }
        }
    }
}
