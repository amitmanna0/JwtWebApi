using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Data;
using WebAPIProject.Infrastructre;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{

    /// <summary>
    /// Only Authorized Users can View Details
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserCredentialsController : ControllerBase
    { 
        private readonly WebAPIProjectContext _context;
        

        public UserCredentialsController(WebAPIProjectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Allow both Admin and User
        /// </summary>
        // GET: api/UserCredentials
        [HttpGet("GetAll"), Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<UserCredential>>> GetUserCredential()
        {

            return await _context.UserCredential.ToListAsync();

        }

        /// <summary>
        /// Allow only Admin 
        /// </summary>
        // GET: api/UserCredentials/5
        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserCredential>> GetUserCredential(int id)
        {

                var userCredential = await _context.UserCredential.FindAsync(id);

                if (userCredential == null)
                {
                    throw new Exception("User doesn't exist.");
                }

                return userCredential;
        }

        /// <summary>
        /// Allow only Admin 
        /// </summary>
        // PUT: api/UserCredentials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles ="Admin")]
        public async Task<IActionResult> PutUserCredential(int id, UserCredential userCredential)
        {
            if (id != userCredential.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCredential).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCredentialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Allow only Admin 
        /// </summary>
        // POST: api/UserCredentials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserCredential>> PostUserCredential(UserCredential userCredential)
        {
            _context.UserCredential.Add(userCredential);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCredential", new { id = userCredential.Id }, userCredential);
        }

        /// <summary>
        /// Allow only Admin 
        /// </summary>
        // DELETE: api/UserCredentials/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserCredential(int id)
        {
            try
            {
                var userCredential = await _context.UserCredential.FindAsync(id);

                _context.UserCredential.Remove(userCredential);
                await _context.SaveChangesAsync();

                return Ok("User Deleted Successfully");
            }
            catch(Exception e)
            {
                new Error(e);
                return BadRequest("User doesn't exist.");
            }
        }

        private bool UserCredentialExists(int id)
        {
            return _context.UserCredential.Any(e => e.Id == id);
        }
    }
}
