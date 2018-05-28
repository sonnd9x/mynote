using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNotes.Entity;
using PagedList;

namespace MyNotes.Dao
{
    public class CommonDao
    {
        NoteDbContext db = null;

        public CommonDao()
        {
            db = new NoteDbContext();
        }

        public tbl_Notes GetNote(Guid id)
        {
            return db.tbl_Notes.Find(id);
        }

        public bool AddNote(tbl_Notes model)
        {
            try
            {
                model.CreateDate = DateTime.Now;
                db.tbl_Notes.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditNote(tbl_Notes model)
        {
            try
            {
                var res = GetNote(model.ID);
                if (res == null)
                    return false;
                res.Name = model.Name;
                res.Content = model.Content;
                res.Public = model.Public;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<tbl_Notes> ListAllPaging(string search, int page, int pageSize, bool? admin = false)
        {
            IQueryable<tbl_Notes> res = db.tbl_Notes;
            if (admin == false)
            {
                res = res.Where(x => x.Public == true);
            }
            if (!string.IsNullOrEmpty(search))
            {
                res = res.Where(x => x.Name.Contains(search)
                || x.Content.Contains(search));
            }
            return res.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public bool CheckUser(string username, string pass)
        {
            try
            {
                var res = db.tbl_User.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(pass));
                if (res == null)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void CreateFirstUser()
        {
            var res = db.tbl_User.FirstOrDefault(x => x.Username.Equals("demo"));
            if (res == null)
            {
                var user = new tbl_User
                {
                    ID = Guid.NewGuid(),
                    Username = "demo",
                    Password = "demo"
                };
                db.tbl_User.Add(user);
                db.SaveChanges();
            }
        }

        public void DeleteNote(Guid id)
        {
            try
            {
                var res = GetNote(id);
                if(res != null)
                {
                    db.tbl_Notes.Remove(res);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}