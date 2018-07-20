using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoSecret.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;



namespace DojoSecret.Controllers
{
    public class HomeController : Controller
    {

        private MyContext _context;
        public HomeController(MyContext context)
        {


            _context = context;
            _context.SaveChanges();
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("Registerd")]
        public IActionResult SignIn()
        {
            Console.WriteLine("Got inside registerd");


            return View("SignIn");
        }


        [HttpPost("Home/create")]

        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                user.Confirm_Password = Hasher.HashPassword(user, user.Confirm_Password);

                user.Created_At = DateTime.Now;
                user.Updated_At = DateTime.Now;

                _context.Add(user);
                _context.SaveChanges();
                ViewBag.user = user;

                HttpContext.Session.SetString("Email", user.Email);
                return RedirectToAction("loadSecretPage");
            }
            else
            {
                return View("Index");
            }


        }

        [HttpGet("Home/back")]
        public IActionResult loadSecretPage()
        {
            string userEmail = HttpContext.Session.GetString("Email");

            User loggedUser = _context.users.Include(usr => usr.Likes).Include(usr => usr.Posts).FirstOrDefault(usr => usr.Email == userEmail);

            List<Posts> allPosts = _context.posts.OrderByDescending(p => p.Created_At).ToList();
            List<User> allUsers = _context.users.ToList();
            ViewBag.user = loggedUser;
            ViewBag.allPosts = allPosts;
            ViewBag.allUsers = allUsers;
            
            return View("Secrets");
        }

         [HttpGet("Home/secDelete/{id}")]
        public IActionResult secDelete(int id)
        {

            Posts post = _context.posts.FirstOrDefault(p => p.PostsId == id);
            string userEmail = HttpContext.Session.GetString("Email");

            _context.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("viewMostPopular");

        }


        [HttpGet("Home/secLikePost/{id}")]
        public IActionResult secLikePost(int id)
        {
            Posts post = _context.posts.FirstOrDefault(p => p.PostsId == id);
            string userEmail = HttpContext.Session.GetString("Email");

            User loggedUser = _context.users.Include(usr => usr.Likes).Include(usr => usr.Posts).FirstOrDefault(usr => usr.Email == userEmail);

            Likes like = new Likes();

            like.User = loggedUser;

            like.Posts = post;

            post.likeCount++;

            post.Likes.Add(like);


            _context.SaveChanges();

            return RedirectToAction("viewMostPopular");
        }

        [HttpGet("Home/viewMostPopular")]
        public IActionResult viewMostPopular()
        {
            string userEmail = HttpContext.Session.GetString("Email");

            User loggedUser = _context.users.Include(usr => usr.Likes).Include(usr => usr.Posts).FirstOrDefault(usr => usr.Email == userEmail);

            List<Posts> allPosts = _context.posts.OrderByDescending(o=>o.likeCount).ToList();
            


            ViewBag.user = loggedUser;
            ViewBag.allPosts = allPosts;


            return View("viewMostPopular");
        }















        [HttpGet("Home/Delete/{id}")]
        public IActionResult Delete(int id)
        {

            Posts post = _context.posts.FirstOrDefault(p => p.PostsId == id);
            string userEmail = HttpContext.Session.GetString("Email");

            _context.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("loadSecretPage");

        }



        [HttpGet("Home/likePost/{id}")]
        public IActionResult likePost(int id)
        {
            Posts post = _context.posts.FirstOrDefault(p => p.PostsId == id);
            string userEmail = HttpContext.Session.GetString("Email");

            User loggedUser = _context.users.Include(usr => usr.Likes).Include(usr => usr.Posts).FirstOrDefault(usr => usr.Email == userEmail);

            Likes like = new Likes();

            like.User = loggedUser;

            like.Posts = post;

            post.likeCount++;

            post.Likes.Add(like);


            _context.SaveChanges();

            return RedirectToAction("loadSecretPage");
        }


        [HttpPost("Home/login")]

        public IActionResult LogingMethod(string Email, string Password)
        {

            List<Posts> Posts = _context.posts.Include(wd => wd.Likes).ToList();

            User logUser = _context.users.SingleOrDefault(usr => usr.Email == Email);




            PasswordHasher<User> Hasher = new PasswordHasher<User>();

            if (logUser != null && Password != null)
            {

                if (0 != Hasher.VerifyHashedPassword(logUser, logUser.Password, Password))
                {

                    ViewBag.logedUser = logUser;
                    HttpContext.Session.SetString("Email", logUser.Email);

                    return RedirectToAction("loadSecretPage");

                }
                else
                {

                    ViewBag.err = "Password or Username is not valid";
                    return View("SignIn");

                }


            }
            else
            {

                ViewBag.err = "Email or Password can't be empty";
                return View("SignIn");
            }


        }

        [HttpPost("Home/addSecret")]
        public IActionResult addSecret(string Post_Content)
        {
            string userEmail = HttpContext.Session.GetString("Email");

            User loggedUser = _context.users.Include(usr => usr.Likes).Include(usr => usr.Posts).FirstOrDefault(usr => usr.Email == userEmail);

            Posts p = new Posts();
            p.Post_Content = Post_Content;
            p.Created_At = DateTime.Now;
            loggedUser.Posts.Add(p);

            _context.SaveChanges();


            return RedirectToAction("loadSecretPage");
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


    }
}
