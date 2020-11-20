using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizFlow.Dto.Round;
using QuizFlow.Models;
using QuizFlow.Services.RoundService;

namespace QuizFlow.Controllers {

  [Authorize]
  [ApiController]
  [Route("rounds")]
  public class RoundController : ControllerBase {

    private readonly IRoundService _service;

    public RoundController(IRoundService service) {
      _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> getAllRounds() {
      ServiceResponse<List<RoundDtoGet>> res = await _service.getAllRounds();
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getRoundById(int id) {
      ServiceResponse<RoundDtoGet> res = await _service.getRoundById(id);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [Route("user")]
    [HttpGet]
    public async Task<IActionResult> getUserRounds() {
      ServiceResponse<List<RoundDtoGet>> res = await _service.getUserRounds();
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> addRound(RoundDtoAdd round) {
      ServiceResponse<RoundDtoGet> res = await _service.addRound(round);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> editRound(RoundDtoEdit round) {
      ServiceResponse<RoundDtoGet> res = await _service.editRound(round);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> deleteRound(int id) {
      ServiceResponse<string> res = await _service.deleteRound(id);
      if (res.data == null) {
        return NotFound(res);
      }
      return Ok(res);
    }
  }
}