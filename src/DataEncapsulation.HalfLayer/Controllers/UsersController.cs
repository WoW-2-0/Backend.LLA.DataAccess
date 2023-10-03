using AutoMapper;
using DataEncapsulation.HalfLayer.Models.Dtos;
using DataEncapsulation.HalfLayer.Models.Entities;
using DataEncapsulation.HalfLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataEncapsulation.HalfLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetUsers()
    {
        var value = await _userService.GetAsync();
        var result = _mapper.Map<IEnumerable<UserDto>>(value);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById(Guid id)
    {
        var value = await _userService.GetByIdAsync(id);
        var result = _mapper.Map<UserDto>(value);
        return Ok(result);
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateUser([FromBody] UserDto user)
    {
        var value = await _userService.CreateAsync(_mapper.Map<User>(user));
        var result = _mapper.Map<UserDto>(value);
        return CreatedAtAction(nameof(GetById),
            new
            {
                id = result.Id
            },
            result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser([FromBody] UserDto user)
    {
        await _userService.UpdateAsync(_mapper.Map<User>(user));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}