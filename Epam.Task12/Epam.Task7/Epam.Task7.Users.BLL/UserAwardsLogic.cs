﻿using System.Collections.Generic;
using System.Linq;
using Epam.Task7.Users.BLL.Interface;
using Epam.Task7.Users.DAL.Interface;
using Epam.Task7.Users.Entities;

namespace Epam.Task7.Users.BLL
{
    public class UserAwardsLogic : IUserAwardsLogic
    {
        private const string AllUsersAwardsCacheKey = "GetAllUsersAwards";

        private readonly IUserAwardsDao userAwardsDao;

        private readonly IAwardsDao awardsDao;
       
        public UserAwardsLogic(IUserAwardsDao userAwardsDao, IAwardsDao awardsDao)
        {
            this.userAwardsDao = userAwardsDao;
            this.awardsDao = awardsDao;
        }

        public void AddUserAward(UsersAwards userAward)
        {
            if (userAward != null)
            {
                this.userAwardsDao.AddUserAward(userAward);
            }
        }

        public void DelUserAward(int idUser, int idAward)
        {
            this.userAwardsDao.DelUserAward(idUser, idAward);
        }

        public IEnumerable<UsersAwards> GetAllUserAward()
        {
            return this.userAwardsDao.GetAllUserAward();
        }

        public IEnumerable<string> GetAwardsOfUser(User user)
        {
            IEnumerable<int> allIdAwards;
            IEnumerable<string> namesOfUserAwards;
            allIdAwards = from f in this.GetAllUserAward() where f.IdUser == user.Id select f.IdAward;
            namesOfUserAwards = from t in this.awardsDao.GetAll() where allIdAwards.Contains(t.Id) select t.Title;
            return namesOfUserAwards;
        }

        public IEnumerable<string> GetPhotoAwardsOfUser(User user)
        {
            IEnumerable<int> allIdAwards;
            IEnumerable<string> namesOfUserAwards;
            allIdAwards = from f in this.GetAllUserAward() where f.IdUser == user.Id select f.IdAward;
            namesOfUserAwards = from r in this.awardsDao.GetAll() where allIdAwards.Contains(r.Id) select r.PhotoLink;
            return namesOfUserAwards;
        }

        public void UpdUserAward(UsersAwards userAward)
        {
            this.userAwardsDao.UpdUserAward(userAward);
        }
    }
}
