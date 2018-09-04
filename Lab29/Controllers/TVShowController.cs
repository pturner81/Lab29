using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Lab29.Models;

namespace Lab29.Controllers
{
    public class TVShowController : ApiController
    {
      public List<Show> GetListOfShows()
        {//http://localhost:port/api/TVShow/GetListOfShows
            TVEntities ShowsORM = new TVEntities();

            return ShowsORM.Shows.ToList();
        }
        public List<Show> GetShowsByCategory(string category)
        {//http://localhost:port/api/TVShow/GetShowsByCategory?category=HBO
            TVEntities ShowsORM = new TVEntities();

            return ShowsORM.Shows.Where(x => x.Category.ToLower() == category.ToLower()).ToList();
        }
        public Show GetRandomShow()
        {//http://localhost:port/api/TVShow/GetRandomShow
            TVEntities ShowsORM = new TVEntities();

            List<Show> shows = ShowsORM.Shows.ToList();
            Random n = new Random();
            int num = n.Next(0, shows.Count());

            return shows[num];
        }
        public Show GetRandomShowByCategory(string category)
        {//http://localhost:port/api/TVShow/GetRandomShowByCategory?category=HBO
            TVEntities ShowsORM = new TVEntities();

            List<Show> showsByCat = ShowsORM.Shows.Where(x => x.Category.ToLower() == category.ToLower()).ToList();
            Random n = new Random();
            int num = n.Next(0, showsByCat.Count());

            return showsByCat[num];
        }
        public List<Show> GetRandomShowsByQuantity(int quantity)
        {//http://localhost:port/api/TVShow/GetRandomShowsByQuantity?quantity=5
            TVEntities ShowsORM = new TVEntities();
            List<Show> shows = ShowsORM.Shows.ToList();

            List<Show> randomShows = new List<Show>();
            for (int i=0; i< quantity; i++)
            {
                Random n = new Random();
                int num = n.Next(0, shows.Count());
                randomShows.Add(shows[num]);
                Thread.Sleep(500);
            }

            return randomShows;
        }
        public List<string> GetShowCategories()
        {//http://localhost:port/api/TVShow/GetShowCategories
            TVEntities ShowsORM = new TVEntities();

            return ShowsORM.Shows.Where(x => x.Category != null).Select(x => x.Category).Distinct().ToList();
        }
        public string GetShowCategory(string ShowTitle)
        {//http://localhost:port/api/TVShow/GetShowCategory?ShowTitle=Shameless
            TVEntities ShowsORM = new TVEntities();

            Show s = ShowsORM.Shows.Find(ShowTitle);
            return s.Category;
        }
        [HttpGet]
        public List<Show> SearchShowTitles(string Search)
        {//http://localhost:port/api/TVShow/SearchShowTitles?Search=The
            TVEntities ShowsORM = new TVEntities();

            List<Show> shows = ShowsORM.Shows.ToList();
            List<Show> searchResults = new List<Show>();
            for (int i = 0; i < shows.Count; i++)
            {
                if (shows[i].Title.Contains(Search))
                {
                    searchResults.Add(shows[i]);
                }
            }
            return searchResults;
        }

    }
}