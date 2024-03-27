using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using api_relation.Data;
using api_relation.Dtos;
using api_relation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_relation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendController : ControllerBase
    {
        private readonly DataDbContext _context;

        public FriendController(DataDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Friend>>> GetOneFriend(int id)
        {
            try
            {
                var friends = await _context.friends
                .Include(friends => friends.Work)
                .Include(friend => friend.Cars) 
                .Include(friends => friends.Things)
                .FirstOrDefaultAsync(friends => friends.Id == id); 
                
                if(friends == null)
                {
                    return StatusCode(404, "Friends not found for provided ID");
                }

                return Ok(friends);
            }
            catch (System.Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
    
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Friend>>> GetAllFriend()
        {
            try
            {
                var friends = await _context.friends
                    .Include(friends => friends.Work)
                    .Include(friend => friend.Cars) 
                    .Include(friends => friends.Things)
                    .ToListAsync(); 

                    return Ok(friends);

            }
            catch (System.Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500, "Internal server error");               
            }
  
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Friend>>> PostFriend(FriendDTo friendDTo)
        {
            try
            {
                var friendItem = new Friend
                {
                    Name = friendDTo.Name
                };

                var work = new Work { NameWork = friendDTo.Work.NameWork, Friend = friendItem};
                friendItem.Work = work;

                var cars = friendDTo.Car.Select(w => new Cars { CarName = w.CarName, Friend = friendItem }).ToList();
                friendItem.Cars = cars;

                var things = friendDTo.Things.Select(f => new Things { ThingsName = f.ThingsName, Friends = new List<Friend> { friendItem } }).ToList();
                friendItem.Things = things;


                _context.friends.Add(friendItem);

                await _context.SaveChangesAsync();

                return Ok(await _context.friends
                        .Include(c => c.Work)
                        .Include(c => c.Cars)
                        .Include(c => c.Things)
                        .ToListAsync());
            }
            catch (System.Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500, "Internal server error");    
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Friend>>> PutFriend(FriendDTo friendDto, int id)
        {
            try
            {
                var friendEntity = await _context.friends
                    .Include(f => f.Work)
                    .Include(f => f.Cars) 
                    .Include(f => f.Things)
                    .FirstOrDefaultAsync(f => f.Id == id); 

                if (friendEntity == null)
                {
                    return NotFound("Friend not found for provided ID");
                }

                friendEntity.Name = friendDto.Name;
       

                await _context.SaveChangesAsync();
    

                return Ok(friendEntity);
            }
            catch (System.Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Friend>>> DeleteFriend(int id)
        {
            try
            {
                var friendEntity = await _context.friends
                    .Include(f => f.Work)
                    .Include(f => f.Cars) 
                    .Include(f => f.Things)
                    .FirstOrDefaultAsync(f => f.Id == id); 
                if (friendEntity == null)
                {
                    return NotFound("Friend not found for provided ID");
                }

                _context.friends.Remove(friendEntity);
                await _context.SaveChangesAsync();


                return Ok(await _context.friends
                    .Include(c => c.Work)
                    .Include(c => c.Cars)
                    .Include(c => c.Things)
                    .ToListAsync());

            }
            catch (System.Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500, "Internal server error");
            }

        }



    }
}