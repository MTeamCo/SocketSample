using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using SocketTest.Models;

namespace SocketTest
{
    public class Services
    {
        public void ChooseMethode(ObjectModel model)
        {
            switch (model.Id)
            {
                case 1:
                    InsertLocation(model.Result);
                    break;
            }
        }

        private void InsertLocation(object result)
        {
            var model = result as PostmanLocationViewModel;
            if (model != null)
            {
                var postmanId = GetPostmanId(model.Token);
                if (postmanId != 0)
                {
                    using (Post_DbEntities db = new Post_DbEntities())
                    {
                        var loc = db.PostmanLocations.FirstOrDefault(u => u.PostmanId == postmanId);
                        if (loc != null)
                        {
                            loc.Latitude = model.Latitude;
                            loc.Longitiude = model.Longitiude;
                            db.Entry(loc).State = EntityState.Modified;
                        }
                        else
                        {
                            db.PostmanLocations.Add(new PostmanLocations()
                            {
                                Latitude = model.Latitude,
                                Longitiude = model.Longitiude,
                                PostmanId = postmanId
                            });
                        }
                        db.SaveChanges();
                    }
                }

            }
        }

        private int GetPostmanId(string token)
        {
            using (Post_DbEntities db = new Post_DbEntities())
            {
                var model = db.PostOfficeSessions.FirstOrDefault(u => u.Token == token);
                return model?.Id ?? 0;
            }
        }
    }
}
