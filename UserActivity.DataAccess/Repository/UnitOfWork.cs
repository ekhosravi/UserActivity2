﻿using UserActivity.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivity.DataAccess;
using UserActivity.DataAccess.Repository; 
using UserActivity.Models;

namespace UserActivity.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Status = new StatusRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public IStatusRepository Status {  get; private set; }

        public IApplicationUserRepository ApplicationUser {  get; private set; }
        public IUserLoginRepository UserLogins {  get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
