﻿using Epam.TaskFinal.Blog.App_Code.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using WebMatrix.Data;

namespace Epam.TaskFinal.Blog.App_Code
{
    public class Tag
    {
        private static WebPageRenderingBase Page
        {
            get
            {
                return WebPageContext.Current.Page;
            }
        }

        public static string Mode
        {
            get
            {
                if (Page.UrlData.Any())
                {
                    return Page.UrlData[0].ToLower();
                }
                return string.Empty;
            }
        }

        public static string FriendlyName
        {
            get
            {
                if (Mode != "new")
                {
                    return Page.UrlData[1];
                }
                return string.Empty;
            }
        }
       
        public static dynamic Current
        {
            get
            {
                
                    var result = TagRepository.Get(FriendlyName);
                    return result ?? CreateTagObject();
                
                
            }
        } 

        private static dynamic CreateTagObject()
        {
            dynamic obj = new ExpandoObject();
            obj.Id = 0;
            obj.Name = string.Empty;
            obj.UrlFriendlyName = string.Empty;

            return obj;
        }
    }

   
}