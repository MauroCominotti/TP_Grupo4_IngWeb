using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RafaelaColabora.Data;
using RafaelaColabora.Models;

namespace RafaelaColabora.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.User).Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/GetPosts
        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            var applicationDbContext = _context.Posts.Include(p => p.User).Include(p => p.Category);
            return StatusCode(200, await applicationDbContext.OrderByDescending(p => p.CreatedAt).ToListAsync());
        }

        // GET: Posts/:cadena
        [HttpPost()]
        public async Task<ActionResult> Index(string cadena)
        {
            var applicationDbContext = _context.Posts.Include(p => p.User).Include(p => p.Category);

            if ((cadena == "") || (cadena == null))
            {
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                return View(await applicationDbContext.Where(p => p.Description.Contains(cadena)).ToListAsync());
            }
        }

        [HttpGet]
        // GET: Posts/Search/:cadena
        public async Task<ActionResult> Search(string cadena)
        {
            var applicationDbContext = _context.Posts.Include(p => p.User).Include(p => p.Category);

            if ((cadena == "") || (cadena == null))
            {
                return StatusCode(200, await applicationDbContext.OrderByDescending(p => p.CreatedAt).ToListAsync());
            }
            else
            {
                return StatusCode(200, await applicationDbContext.Where(p => p.Description.Contains(cadena)).OrderByDescending(p => p.CreatedAt).ToListAsync());
            }
        }

        [HttpGet]
        // GET: Posts/VerifyPostLike/:postId/?userId
        public async Task<ActionResult> VerifyPostLike([FromQuery] int postId, string userId)
        {
            //var post = await _context.Posts
            //    .Include(p => p.User)
            //    .Include(p => p.Likes).ThenInclude(l => l.UserId)
            //    .FirstOrDefaultAsync(m => m.Id == postId && m.Likes.FirstOrDefault(l => l.UserId == userId) != null);

            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(m => m.Id == postId && m.Likes.FirstOrDefault(l => l.UserId == userId) != null);

            if (post == null)
            {
                return NotFound();
            }

            return StatusCode(200, post);

        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Basic,Moderator,Admin,SuperAdmin")]
        public IActionResult Create()
        {
            ViewData["FKUserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName");
            ViewData["FKCategoryId"] = new SelectList(_context.Category, "Id", "Description");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Basic,Moderator,Admin,SuperAdmin")]
        public async Task<IActionResult> Create([Bind("Id,UserId,CategoryId,State,Description,Links,Phone,AlternativePhone,AlternativeEmail,Photo,CreatedAt")] PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                Post postToSend = new Post();
                IFormFile file = post.Photo;
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    postToSend.Photo = dataStream.ToArray();
                }
                postToSend.UserId = post.UserId;
                postToSend.CategoryId = post.CategoryId;
                postToSend.Description = post.Description;
                postToSend.Links = post.Links;
                postToSend.Phone = post.Phone;
                postToSend.AlternativePhone = post.AlternativePhone;
                postToSend.AlternativeEmail = post.AlternativeEmail;
                _context.Add(postToSend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/CreatePost
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Basic,Moderator,Admin,SuperAdmin")]
        public async Task<ActionResult> CreatePost([FromBody] PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                var selectedCategory = await _context.Category.FindAsync(post.CategoryId);
                if (selectedCategory == null)
                {
                    return BadRequest("Associated category not found.");
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return StatusCode(200, post);
            }
            return BadRequest();
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Basic,Moderator,Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Basic,Moderator,Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CategoryId,State,Description,Links,Phone,AlternativePhone,AlternativeEmail,Photo,CreatedAt")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Basic,Moderator,Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Basic,Moderator,Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
