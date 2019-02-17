using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Helper;
using Newtonsoft.Json;
using SocketTest.Models;

namespace SocketTest
{
    public class Services
    {

        public static void ChooseMethode(Socket handler, string content)
        {
            var model = JsonConvert.DeserializeObject<ObjectModel>(content);
            switch (model.Id)
            {
                case 1:
                    SavePostmanData(handler, content);
                    break;
                case 2:
                    GetRequest(model.Result);
                    break;
            }
        }

        private static void GetRequest(object result)
        {
            using (Post_DbEntities db = new Post_DbEntities())
            {
                int id = (int)result;
                var request = db.UserRequests.Find(id);
                if (request != null)
                {
                    var nears = AsyncSocket._Postman
                .Select(u => new
                {
                    distance = Math.Pow(Convert.ToDouble(u.PostmanModel.Latitude.Replace('.', '/')) -
                    Convert.ToDouble(request.OriginLatitude.Replace('.', '/')), 2) +
                    Math.Pow(Convert.ToDouble(u.PostmanModel.Longitiude.Replace('.', '/')) -
                    Convert.ToDouble(request.OriginLongitiude.Replace('.', '/')), 2),
                    id = u.PostmanModel.Token
                }).OrderBy(u => u.distance).ToList();
                }
               
            }
        }

        //private void InsertLocation(object result)
        //{
        //    var model = result as PostmanLocationViewModel;
        //    if (model != null)
        //    {
        //        var postmanId = GetPostmanId(model.Token);
        //        if (postmanId != 0)
        //        {
        //            using (Post_DbEntities db = new Post_DbEntities())
        //            {
        //                var loc = db.PostmanLocations.FirstOrDefault(u => u.PostmanId == postmanId);
        //                if (loc != null)
        //                {
        //                    loc.Latitude = model.Latitude;
        //                    loc.Longitiude = model.Longitiude;
        //                    db.Entry(loc).State = EntityState.Modified;
        //                }
        //                else
        //                {
        //                    db.PostmanLocations.Add(new PostmanLocations()
        //                    {
        //                        Latitude = model.Latitude,
        //                        Longitiude = model.Longitiude,
        //                        PostmanId = postmanId
        //                    });
        //                }
        //                db.SaveChanges();
        //            }
        //        }

        //    }
        //}

        //private int GetPostmanId(string token)
        //{
        //    using (Post_DbEntities db = new Post_DbEntities())
        //    {
        //        var model = db.PostOfficeSessions.FirstOrDefault(u => u.Token == token);
        //        return model?.Id ?? 0;
        //    }
        //}

        public static void SavePostmanData(Socket handler, string content)
        {
            var model = JsonConvert.DeserializeObject<PostmanModel>(content);
            AsyncSocket._Postman.Add(new PostmanLocationViewModel()
            {
                PostmanModel = model,
                Socket = handler
            });
        }

        public static Postmen  GetPostman(string token)
        {
            using (Post_DbEntities db = new Post_DbEntities())
            {
                var model = db.PostmanSessions.FirstOrDefault(u => u.Token == token);
                return model?.Postmen;
            }
        }
    }
}
