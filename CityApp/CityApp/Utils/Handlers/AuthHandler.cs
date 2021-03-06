﻿using CityApp.Models.DB;
using CityApp.Models.DTO.User;
using CityApp.Utils;
using CityApp.Utils.Attribute;
using CityApp.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CityApp.Utils.Handlers
{
    
    public class AuthHandler
    {
        private readonly CityAppEntities db;

        public AuthHandler()
        {
            db = new CityAppEntities();
            db.Database.CommandTimeout = ConstantHelper.DB_TIMEOUT;
        }


        //-> Login
        public async Task<UserViewDTO> Login(UserCredentialDTO crendential)
        {
            //string password = EncryptString(crendential.password);
            string password = CryptingHelper.EncryptString(crendential.password);
            var user = await db.tblUsers.FirstOrDefaultAsync(x => x.deleted == null && x.userName == crendential.userName && x.password == password);
            //var user = await db.tblUsers.FirstOrDefaultAsync(x => x.deleted == null && x.userName == crendential.userName);

            if (user == null)
                return null;

            Guid token = Guid.NewGuid();
            user.session = CryptingHelper.EncryptString(token.ToString());
            await db.SaveChangesAsync();

            var userView = MappingHelper.MapDBClassToDTO<tblUser, UserViewDTO>(user);
            userView.session = token.ToString(); //***//
            return userView;
        }

        //-> IsValidSession
        public bool IsValidSession(UserViewDTO userDTO)
        {
            //var session = EncryptString(userDTO.session);
            var session = CryptingHelper.EncryptString(userDTO.session);
            var user = db.tblUsers.FirstOrDefault(x => x.deleted == null && x.id == userDTO.id && x.session == session );
            if (user == null)
                return false;
            return true;
        }



        /*
        //-> encrypt string
        private string EncryptString(string str)
        {
            CryptingHelper _crypt = new CryptingHelper();
            string plainText = str;
            string iv = "Xsoft";// CryptLib.GenerateRandomIV(16); //16 bytes = 128 bits
            string key = CryptingHelper.getHashSha256("@XSoft201701", 31); //32 bytes = 256 bits

            return _crypt.encrypt(plainText, key, iv);

        }*/
    }

}