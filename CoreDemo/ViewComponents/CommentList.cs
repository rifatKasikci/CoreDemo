using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreDemo.ViewComponents
{
    public class CommentList:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var commentValues = new List<UserComment>
            {
                new UserComment
                {
                    Id=1,
                    UserName="Rıfat"
                },
                new UserComment { 
                    Id=2,
                    UserName="Murat"
                },
                new UserComment
                {
                    Id=3,
                    UserName="Adnan"
                }
            };
            return View(commentValues);
        }
    }
}
