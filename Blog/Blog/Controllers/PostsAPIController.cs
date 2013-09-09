using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using Blog.Models;
using Newtonsoft.Json;

namespace Blog.Controllers
{
    public class PostsAPIController : ApiController
    {
        private UsersContext db = new UsersContext();
        
        // GET api/PostsAPI
        public IEnumerable<PostModel> GetPostModels()
        {
            var p = (from ps in db.Posts
                    select ps).ToList();
            var pq = p.AsEnumerable();
            var pj = JsonConvert.SerializeObject(pq);
            return db.Posts.AsEnumerable();
        }

        // GET api/PostsAPI/5
        public PostModel GetPostModel(int id)
        {
            PostModel postmodel = db.Posts.Find(id);
            if (postmodel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return postmodel;
        }

        // PUT api/PostsAPI/5
        public HttpResponseMessage PutPostModel(int id, PostModel postmodel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != postmodel.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(postmodel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/PostsAPI
        public HttpResponseMessage PostPostModel(PostModel postmodel)
        {
            postmodel.Date = DateTimeOffset.Now;
            postmodel.Author = db.UserProfiles.Where(x => x.UserName == User.Identity.Name).First();
            if (ModelState.IsValid)
            {
                db.Posts.Add(postmodel);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, postmodel);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = postmodel.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PostsAPI/5
        public HttpResponseMessage DeletePostModel(int id)
        {
            PostModel postmodel = db.Posts.Find(id);
            if (postmodel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Posts.Remove(postmodel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, postmodel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}